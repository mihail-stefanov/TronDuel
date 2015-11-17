namespace TronDuel.GraphicalObjects.Enemies
{
    using System;
    using System.Diagnostics;
    using TronDuel.Enumerations;
    using TronDuel.MovingObjects.GraphicalObjects;
    using TronDuel.Utilities.Containers;

    public class StationaryEnemy : GraphicalObject
    {
        private Stopwatch shotDelayStopwatch = new Stopwatch();
        private int shotDelayTime = 2000;

        public StationaryEnemy(byte startingPositionX, byte startingPositionY, ConsoleColor color)
            : base(startingPositionX, startingPositionY, color)
        {
            this.HealthPoints = 100;
            this.Sprite = '+';
            this.shotDelayStopwatch.Start();
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

        public void FireWeapon(GraphicalObjectContainer graphicalObjects, SoundEffectContainer soundEffects)
        {
            if (this.shotDelayStopwatch.ElapsedMilliseconds > this.shotDelayTime)
            {
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Up, 
                        ProjectileType.Enemy, 
                        '▲'));
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Down, 
                        ProjectileType.Enemy, 
                        '▼'));
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Left, 
                        ProjectileType.Enemy, 
                        '◄'));
                graphicalObjects.Projectiles.Add(
                    new Projectile(
                        this.Xposition,
                        this.Yposition,
                        ConsoleColor.Red,
                        Direction.Right, 
                        ProjectileType.Enemy, 
                        '►'));

                soundEffects.PlayEnemyShot();
                this.shotDelayStopwatch.Restart();
            }
        }

        private void EraseSpriteFromLastPosition()
        {
            Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
            Console.Write(' ');
        }
    }
}
