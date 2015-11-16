﻿namespace TronDuel.MovingObjects.GraphicalObjects
{
    using System;
    using Enumerations;
    using TronDuel.Interfaces;
    using System.Diagnostics;
    using TronDuel.Utilities;
    using TronDuel.GraphicalObjects;

    public class SpaceShip : GraphicalObject, IMovable
    {
        private const char ShipCharRight = '►';
        private const char ShipCharLeft = '◄';
        private const char ShipCharUp = '▲';
        private const char ShipCharDown = '▼';

        private double speedX = 0.5;
        private double speedY = 0.3;

        private Stopwatch shotDelayStopwatch = new Stopwatch();
        private int shotDelayTime = 200;

        private Stopwatch shieldTimeStopwatch = new Stopwatch();

        private Direction direction;

        public SpaceShip(byte startingPositionX, byte startingPositionY, ConsoleColor color, Direction direction)
            : base(startingPositionX, startingPositionY, color)
        {
            this.direction = Direction.Right;
            this.Sprite = ShipCharRight;
            this.HealthPoints = 100;
            this.ShotsAvailable = 10;
            this.ShieldTimeAvailable = 0;
            this.shotDelayStopwatch.Start();
            PrintStatus();  // TODO: To refactor
        }

        public double XpreviousPosition { get; set; }
        public double YpreviousPosition { get; set; }

        public double SpeedX
        {
            get
            {
                return this.speedX;
            }
            set
            {
                this.speedX = value;
            }
        }
        public double SpeedY
        {
            get
            {
                return this.speedY;
            }
            set
            {
                this.speedY = value;
            }
        }

        public Direction Direction
        {
            get
            {
                return this.direction;
            }
        }

        public void ChangeDirection(Direction direction, GraphicalObjectContainer graphicalObjects)
        {
            bool changeSuccessful = false;

            switch (direction)
            {
                // Changing the graphical direction of the ship upon changing the direction property
                // Making sure the ship cannot move in the opposite direction when the tron bonus is on
                case Direction.Right:
                    if (this.direction != Direction.Left || graphicalObjects.TronDotsContainers.Count == 0)
                    {
                        this.Sprite = ShipCharRight;
                        changeSuccessful = true;
                    }
                    break;
                case Direction.Left:
                    if (this.direction != Direction.Right || graphicalObjects.TronDotsContainers.Count == 0)
                    {
                        this.Sprite = ShipCharLeft;
                        changeSuccessful = true;
                    }
                    break;
                case Direction.Up:
                    if (this.direction != Direction.Down || graphicalObjects.TronDotsContainers.Count == 0)
                    {
                        this.Sprite = ShipCharUp;
                        changeSuccessful = true;
                    }
                    break;
                case Direction.Down:
                    if (this.direction != Direction.Up || graphicalObjects.TronDotsContainers.Count == 0)
                    {
                        this.Sprite = ShipCharDown;
                        changeSuccessful = true;
                    }
                    break;
                default:
                    break;
            }

            if (changeSuccessful)
            {
                this.direction = direction;
            }
        }

        public sbyte HealthPoints { get; set; }

        public byte ShotsAvailable { get; set; }

        public double ShieldTimeAvailable { get; set; }

        public void Move()
        {
            EraseSpriteFromLastPosition();  // TODO: To refactor

            this.XpreviousPosition = Xposition;
            this.YpreviousPosition = Yposition;

            switch (Direction)
            {
                case Direction.Right:
                    Xposition += speedX;
                    break;
                case Direction.Left:
                    Xposition -= speedX;
                    break;
                case Direction.Up:
                    Yposition -= speedY;
                    break;
                case Direction.Down:
                    Yposition += speedY;
                    break;
                default:
                    break;
            }
        }

        private void EraseSpriteFromLastPosition()
        {
            Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
            Console.Write(' ');
        }

        public override void Draw()
        {
            Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Sprite);
        }

        public void ChangeHealth(sbyte healthPoints)
        {
            if (this.HealthPoints + healthPoints > 100)
            {
                this.HealthPoints = 100;
            }
            else if (this.HealthPoints + healthPoints <= 0)
            {
                this.HealthPoints = 0;

                // TODO: Gameover logic call here
            }
            else
            {
                this.HealthPoints += healthPoints;
            }

            PrintStatus();  // TODO: To refactor
        }

        public void IncreaseShotsAvailable(byte ammoShots)
        {
            if (this.ShotsAvailable + ammoShots > 100)
            {
                this.ShotsAvailable = 100;
            }
            else
            {
                this.ShotsAvailable += ammoShots;
            }
        }

        public void DecreaseShotsAvailable()
        {
            if (this.ShotsAvailable > 0)
            {
                this.ShotsAvailable--;
            }
        }

        public void IncreaseShieldTimeAvailable(byte shieldTime)
        {
            this.shieldTimeStopwatch.Start();

            if (this.ShieldTimeAvailable + shieldTime > 100)
            {
                this.ShieldTimeAvailable = 100;
            }
            else
            {
                this.ShieldTimeAvailable += shieldTime;
            }
        }

        public void DecreaseShieldTime(int threadSleepTime)
        {
            if (this.ShieldTimeAvailable > 0)
            {
                this.Color = ConsoleColor.Yellow;

                this.ShieldTimeAvailable -= ((double)threadSleepTime / 1000);
            }
            else
            {
                this.Color = ConsoleColor.Green;
                this.shieldTimeStopwatch.Stop();
                this.shieldTimeStopwatch.Reset();
                this.ShieldTimeAvailable = 0;
            }
            PrintStatus();  // TODO: To refactor
        }

        public void PrintStatus()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Player 1 - Health: {0} Shots: {1} ", this.HealthPoints, this.ShotsAvailable);
            if (this.ShieldTimeAvailable > 0)
            {
                Console.Write("Shield time: {0}  ", (byte)this.ShieldTimeAvailable);
            }
            else
            {
                Console.Write(new string(' ', 16));
            }
        }

        public void FireCurrentWeapon(GraphicalObjectContainer graphicalObjects, SoundEffectContainer soundEffects)
        {
            if (shotDelayStopwatch.ElapsedMilliseconds > shotDelayTime && this.ShotsAvailable > 0)
            {
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Cyan,
                        this.Direction, ProjectileType.Player, '*'));

                soundEffects.PlayShot();
                this.DecreaseShotsAvailable();
                shotDelayStopwatch.Restart();
            }
            PrintStatus(); // TODO: To refactor
        }
    }
}