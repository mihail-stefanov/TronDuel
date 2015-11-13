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

        // Future positions are to be used to improve collision detections
        private byte xFuturePosition;
        private byte yFuturePosition;

        private double speed = 1;

        public Projectile(
            double startingPositionX,
            double startingPositionY,
            ConsoleColor color,
            Direction direction, 
            ProjectileType type, 
            char sprite)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Direction = direction;
            this.Speed = speed;
            this.type = type;
            this.Sprite = sprite;
            this.XfuturePosition = (byte) this.Xposition;
            this.YfuturePosition = (byte) this.Yposition;
        }

        public byte XfuturePosition
        {
            get
            {
                return this.xFuturePosition;
            }
            set
            {
                this.xFuturePosition = value;
            }
        }

        public byte YfuturePosition
        {
            get
            {
                return this.yFuturePosition;
            }
            set
            {
                this.yFuturePosition = value;
            }
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
                    if (this.Xposition + this.Speed > Console.BufferWidth - 1)
                    {
                        this.Xposition = Console.BufferWidth;
                    }
                    else
                    {
                        this.Xposition += this.Speed;
                        this.xFuturePosition = (byte) (this.Xposition + 1);
                    }
                    break;
                case Direction.Left:
                    if (this.Xposition - this.Speed < 1)
                    {
                        this.Xposition = 0;
                    }
                    else
                    {
                        this.Xposition -= this.Speed;
                        this.xFuturePosition = (byte) (this.Xposition - 1);
                    }
                    break;
                case Direction.Up:
                    if (this.Yposition - this.Speed / 2 < 1)
                    {
                        this.Yposition = 0;
                    }
                    else
                    {
                        this.Yposition -= this.Speed / 2;
                        this.yFuturePosition = (byte) (this.Yposition - 1);
                    }
                    break;
                case Direction.Down:
                    if (this.Yposition + this.Speed / 2 > Console.BufferHeight - 1)
                    {
                        this.Yposition = Console.BufferHeight;
                    }
                    else
                    {
                        this.Yposition += this.Speed / 2;
                        this.yFuturePosition = (byte) (this.Yposition + 1);
                    }
                    break;
                default:
                    break;
            }
        }

        private void EraseSpriteFromLastPosition()
        {
            if ((byte)this.Xposition < Console.BufferWidth && (byte) this.Xposition > 0 && 
                (byte)this.Yposition < Console.BufferHeight && (byte) this.Yposition > 0)
            {
                Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
                Console.Write(' ');
            }
        }

    }
}
