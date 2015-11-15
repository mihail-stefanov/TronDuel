namespace TronDuel.GraphicalObjects.MovingObjects
{
    using Interfaces;
    using System;
    using System.Diagnostics;
    using TronDuel.MovingObjects.GraphicalObjects;
    using Enumerations;
    using TronDuel.Utilities;

    public class StationaryEnemy : GraphicalObject
    {
        private Stopwatch shotDelayStopwatch = new Stopwatch();
        private int shotDelayTime = 2000;

        public StationaryEnemy(byte startingPositionX, byte startingPositionY, ConsoleColor color)
            : base(startingPositionX, startingPositionY, color)
        {
            this.HealthPoints = 100;
            this.Sprite = '+';
            shotDelayStopwatch.Start();
        }

        public sbyte HealthPoints { get; set; }

        public void ReduceHealth(sbyte healthPoints)
        {
            if (this.HealthPoints - healthPoints <= 0)
            {
                this.HealthPoints = 0;
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
    }
}
