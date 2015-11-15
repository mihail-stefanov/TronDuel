namespace TronDuel.GraphicalObjects.MovingObjects
{
    using Interfaces;
    using System;
    using System.Diagnostics;
    using TronDuel.MovingObjects.GraphicalObjects;
    using Enumerations;
    using TronDuel.Utilities;

    public class MovingEnemy : GraphicalObject, IMovable
    {
        private SpaceShip shipToTrace;
        private double speedX = 0.1;
        private double speedY = 0.04;

        public MovingEnemy(byte startingPositionX, byte startingPositionY, ConsoleColor color, SpaceShip shipToTrace)
            : base(startingPositionX, startingPositionY, color)
        {
            this.HealthPoints = 10;
            this.Sprite = '*';
            this.shipToTrace = shipToTrace;
        }

        public sbyte HealthPoints { get; set; }

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

        private void EraseSpriteFromLastPosition()
        {
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.Write(' ');
        }

        public void Move()
        {
            EraseSpriteFromLastPosition(); // TODO: Refactor

            if ((byte)shipToTrace.Xposition < (byte)this.Xposition)
            {
                this.Xposition -= speedX;
            }

            if ((byte)shipToTrace.Xposition > (byte)this.Xposition)
            {
                this.Xposition += speedX;
            }

            if ((byte)shipToTrace.Yposition < (byte)this.Yposition)
            {
                this.Yposition -= speedY;
            }

            if ((byte)shipToTrace.Yposition > (byte)this.Yposition)
            {
                this.Yposition += speedY;
            }
        }
    }
}
