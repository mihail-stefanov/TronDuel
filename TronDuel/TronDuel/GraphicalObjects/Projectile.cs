namespace TronDuel.GraphicalObjects
{
    using System;
    using TronDuel.GraphicalObjects.Enumerations;
    using TronDuel.Interfaces;

    public class Projectile : GraphicalObject, IMovable
    {
        private Direction direction;

        private double speed = 2;

        public Projectile(
            double startingPositionX,
            double startingPositionY,
            ConsoleColor color,
            Direction direction)
            : base(startingPositionX, startingPositionY, ConsoleColor.Cyan)
        {
            this.Direction = direction;
            this.Speed = speed;
            this.Sprite = '*';
        }

        public Direction Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        public double Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }

        public void Move()
        {
            EraseSpriteFromLastPosition();

            switch (this.Direction)
            {
                case Direction.Right:
                    this.Xposition += this.Speed;
                    break;
                case Direction.Left:
                    this.Xposition -= this.Speed;
                    break;
                case Direction.Up:
                    this.Yposition -= this.Speed / 2;
                    break;
                case Direction.Down:
                    this.Yposition += this.Speed / 2;
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

    }
}
