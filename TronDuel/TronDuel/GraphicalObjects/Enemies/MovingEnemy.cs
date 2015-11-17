namespace TronDuel.GraphicalObjects.Enemies
{
    using System;
    using Interfaces;
    using TronDuel.MovingObjects.GraphicalObjects;

    public class MovingEnemy : GraphicalObject, IMovable
    {
        private SpaceShip shipToTrace;
        private double xspeed = 0.1;
        private double yspeed = 0.04;

        public MovingEnemy(byte startingPositionX, byte startingPositionY, ConsoleColor color, SpaceShip shipToTrace)
            : base(startingPositionX, startingPositionY, color)
        {
            this.HealthPoints = 10;
            this.Sprite = '*';
            this.shipToTrace = shipToTrace;
        }

        public sbyte HealthPoints { get; set; }

        public double Xspeed
        {
            get
            {
                return this.xspeed;
            }

            set
            {
                this.xspeed = value;
            }
        }

        public double Yspeed
        {
            get
            {
                return this.yspeed;
            }

            set
            {
                this.yspeed = value;
            }
        }

        public void ReduceHealth(sbyte healthPoints)
        {
            if (this.HealthPoints + healthPoints <= 0)
            {
                this.HealthPoints = 0;
            }
            else
            {
                this.HealthPoints -= healthPoints;
            }
        }

        public void Move()
        {
            this.EraseSpriteFromLastPosition();

            if ((byte)this.shipToTrace.Xposition < (byte)this.Xposition)
            {
                this.Xposition -= this.xspeed;
            }

            if ((byte)this.shipToTrace.Xposition > (byte)this.Xposition)
            {
                this.Xposition += this.xspeed;
            }

            if ((byte)this.shipToTrace.Yposition < (byte)this.Yposition)
            {
                this.Yposition -= this.yspeed;
            }

            if ((byte)this.shipToTrace.Yposition > (byte)this.Yposition)
            {
                this.Yposition += this.yspeed;
            }
        }

        private void EraseSpriteFromLastPosition()
        {
            Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
            Console.Write(' ');
        }
    }
}
