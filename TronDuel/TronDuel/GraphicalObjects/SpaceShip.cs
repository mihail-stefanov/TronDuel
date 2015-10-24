namespace TronDuel.GraphicalObjects
{
    using System;
    using Enumerations;
    using TronDuel.Interfaces;
    using System.Diagnostics;
    using TronDuel.Utilities;

    public class SpaceShip : GraphicalObject, IMovable
    {
        private const char ShipCharRight = '►';
        private const char ShipCharLeft = '◄';
        private const char ShipCharUp = '▲';
        private const char ShipCharDown = '▼';

        private const double speedX = 0.5;
        private const double speedY = 0.3;

        private Stopwatch shotDelayStopwatch = new Stopwatch();
        private int shotDelayTime = 200;

        private Direction direction;

        public SpaceShip(byte startingPositionX, byte startingPositionY, ConsoleColor color, Direction direction)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Direction = Direction.Right;
            this.Sprite = ShipCharRight;
            this.HealthPoints = 50;
            this.ShotsAvailable = 10;
            this.shotDelayStopwatch.Start();
            PrintStatus();
        }

        public Direction Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                switch (value)
                {
                    // Changing the graphical direction of the ship upon changing the direction property
                    case Direction.Right:
                        this.Sprite = ShipCharRight;
                        break;
                    case Direction.Left:
                        this.Sprite = ShipCharLeft;
                        break;
                    case Direction.Up:
                        this.Sprite = ShipCharUp;
                        break;
                    case Direction.Down:
                        this.Sprite = ShipCharDown;
                        break;
                    default:
                        break;
                }

                this.direction = value;
            }
        }

        public byte HealthPoints { get; set; }

        public byte ShotsAvailable { get; set; }

        public void Move()
        {
            EraseSpriteFromLastPosition();

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
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.Write(' ');
        }

        public override void Draw()
        {
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Sprite);
        }

        public void ChangeHealth(byte healthPoints)
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

            PrintStatus();
        }

        public void DecreaseShotsAvailable()
        {
            if (this.ShotsAvailable > 0)
            {
                this.ShotsAvailable--;
            }
        }

        private void PrintStatus()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = this.Color;
            Console.Write("Player 1 - Health: {0} Shots: {1}   ", this.HealthPoints, this.ShotsAvailable);
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
                        this.Direction));

                soundEffects.PlayShot();
                this.DecreaseShotsAvailable();
                shotDelayStopwatch.Restart();
            }
            PrintStatus();
        }
    }
}