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
        private double speed = 0.1;

        public MovingEnemy(byte startingPositionX, byte startingPositionY, ConsoleColor color, SpaceShip shipToTrace)
            : base(startingPositionX, startingPositionY, color)
        {
            this.HealthPoints = 10;
            this.Sprite = '*';
            this.shipToTrace = shipToTrace;
        }

        public sbyte HealthPoints { get; set; }

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

            if (shipToTrace.Xposition < this.Xposition)
            {
                this.Xposition -= speed;
            }

            if (shipToTrace.Xposition > this.Xposition)
            {
                this.Xposition += speed;
            }

            if (shipToTrace.Yposition < this.Yposition)
            {
                this.Yposition -= speed;
            }

            if (shipToTrace.Yposition > this.Yposition)
            {
                this.Yposition += speed;
            }
        }
    }
}
