namespace TronDuel.GraphicalObjects.MovingObjects
{
    using Interfaces;
    using System;
    using System.Diagnostics;
    using TronDuel.MovingObjects.GraphicalObjects;
    using Enumerations;
    using TronDuel.Utilities;

    public class Enemy : GraphicalObject, IMovable
    {
        private SpaceShip shipToTrace;
        private double speed = 0.1;

        private Stopwatch shotDelayStopwatch = new Stopwatch();
        private int shotDelayTime = 2000;

        public Enemy(byte startingPositionX, byte startingPositionY, ConsoleColor color, SpaceShip shipToTrace)
            : base(startingPositionX, startingPositionY, color)
        {
            this.HealthPoints = 100;
            this.Sprite = '+';
            this.shipToTrace = shipToTrace;
            shotDelayStopwatch.Start();
        }

        public sbyte HealthPoints { get; set; }

        public void ReduceHealth(sbyte healthPoints)
        {
            if (this.HealthPoints - healthPoints <= 0)
            {
                this.HealthPoints = 0;

                // TODO: Remove enemy here
            }
            else
            {
                this.HealthPoints -= healthPoints;
            }
        }

        private void EraseSpriteFromLastPosition()
        {
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.Write(' ');
        }

        public void Move()
        {
            EraseSpriteFromLastPosition(); // TODO: Refactor

            if (shipToTrace.Xposition < this.Xposition && (byte)this.Yposition != (byte) shipToTrace.Yposition)
            {
                this.Xposition -= speed;
            }

            if (shipToTrace.Xposition > this.Xposition && (byte)this.Yposition != (byte) shipToTrace.Yposition)
            {
                this.Xposition += speed;
            }

            if (shipToTrace.Yposition < this.Yposition && (byte)this.Xposition != (byte) shipToTrace.Xposition)
            {
                this.Yposition -= speed;
            }

            if (shipToTrace.Yposition > this.Yposition && (byte)this.Xposition != (byte) shipToTrace.Xposition)
            {
                this.Yposition += speed;
            }
        }

        public void FireWeapon(GraphicalObjectContainer graphicalObjects, SoundEffectContainer soundEffects)
        {
            if (shotDelayStopwatch.ElapsedMilliseconds > shotDelayTime)
            {
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Up, ProjectileType.Enemy, '▲'));
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Down, ProjectileType.Enemy, '▼'));
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Left, ProjectileType.Enemy, '◄'));
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Right, ProjectileType.Enemy, '►'));

                soundEffects.PlayEnemyShot();
                shotDelayStopwatch.Restart();
            }
        }

        public Enumerations.Direction Direction { get; set; }
    }
}
