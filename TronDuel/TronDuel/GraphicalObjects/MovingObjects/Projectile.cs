namespace TronDuel.MovingObjects.GraphicalObjects
{
    using System;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects;
    using TronDuel.Interfaces;

    public class Projectile : GraphicalObject, IMovable
    {
        public const sbyte Damage = -10;
        private Direction direction;
        private ProjectileType type;

        private double speed = 1;

        public Projectile(
            double startingPositionX,
            double startingPositionY,
            ConsoleColor color,
            Direction direction, ProjectileType type, char sprite)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Direction = direction;
            this.Speed = speed;
            this.type = type;
            this.Sprite = sprite;
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

        public ProjectileType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
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
            EraseSpriteFromLastPosition();  // TODO: To refactor

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
            if (this.Xposition > 0 && this.Xposition < Console.BufferWidth &&
                this.Yposition > 0 && this.Yposition < Console.BufferHeight)
            {
                Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
                Console.Write(' ');
            }
        }

    }
}
