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
        private byte xfuturePosition;
        private byte yfuturePosition;

        private double xspeed = 1;
        private double yspeed = 0.5;

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
            this.Xspeed = this.xspeed;
            this.type = type;
            this.Sprite = sprite;
            this.XfuturePosition = (byte)this.Xposition;
            this.YfuturePosition = (byte)this.Yposition;
        }

        public byte XfuturePosition
        {
            get
            {
                return this.xfuturePosition;
            }

            set
            {
                this.xfuturePosition = value;
            }
        }

        public byte YfuturePosition
        {
            get
            {
                return this.yfuturePosition;
            }

            set
            {
                this.yfuturePosition = value;
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

        public void Move()
        {
            this.EraseSpriteFromLastPosition();

            switch (this.Direction)
            {
                case Direction.Right:
                    if (this.Xposition + this.Xspeed > Console.BufferWidth - 1)
                    {
                        this.Xposition = Console.BufferWidth;
                    }
                    else
                    {
                        this.Xposition += this.Xspeed;
                        this.xfuturePosition = (byte)(this.Xposition + 1);
                    }

                    break;
                case Direction.Left:
                    if (this.Xposition - this.Xspeed < 1)
                    {
                        this.Xposition = 0;
                    }
                    else
                    {
                        this.Xposition -= this.Xspeed;
                        this.xfuturePosition = (byte)(this.Xposition - 1);
                    }

                    break;
                case Direction.Up:
                    if (this.Yposition - this.Yspeed < 1)
                    {
                        this.Yposition = 0;
                    }
                    else
                    {
                        this.Yposition -= this.Yspeed;
                        this.yfuturePosition = (byte)(this.Yposition - 1);
                    }

                    break;
                case Direction.Down:
                    if (this.Yposition + this.Yspeed > Console.BufferHeight - 1)
                    {
                        this.Yposition = Console.BufferHeight;
                    }
                    else
                    {
                        this.Yposition += this.Yspeed;
                        this.yfuturePosition = (byte)(this.Yposition + 1);
                    }

                    break;
                default:
                    break;
            }
        }

        private void EraseSpriteFromLastPosition()
        {
            if ((byte)this.Xposition < Console.BufferWidth && (byte)this.Xposition > 0 &&
                (byte)this.Yposition < Console.BufferHeight && (byte)this.Yposition > 0)
            {
                Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
                Console.Write(' ');
            }
        }
    }
}
