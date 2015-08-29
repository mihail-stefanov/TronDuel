namespace TronDuel
{
    using System;

    public class SpaceShip
    {
        private const char ShipCharRight = '►';
        private const char ShipCharLeft = '◄';
        private const char ShipCharUp = '▲';
        private const char ShipCharDown = '▼';

        private Direction direction;
        private char currentChar;
        private byte xposition;
        private byte yposition;

        public SpaceShip(byte startingPositionX, byte startingPositionY, Direction direction)
        {
            this.Direction = Direction.Right;
            this.CurrentChar = ShipCharRight;
            this.Xposition = startingPositionX;
            this.Yposition = startingPositionY;
        }

        public Direction Direction 
        {
            get
            {
                return this.direction;
            }
            set
            {
                switch (value)
                {
                    case Direction.Right:
                        this.CurrentChar = ShipCharRight;
                        break;
                    case Direction.Left:
                        this.CurrentChar = ShipCharLeft;
                        break;
                    case Direction.Up:
                        this.CurrentChar = ShipCharUp;
                        break;
                    case Direction.Down:
                        this.CurrentChar = ShipCharDown;
                        break;
                    default:
                        break;
                }

                this.direction = value;
            }
        }

        public char CurrentChar
        {
            get
            {
                return this.currentChar;
            }

            private set
            {
                this.currentChar = value;
            }
        }

        public byte Xposition 
        {
            get
            {
                return this.xposition;
            }
            set
            {
                if (value > 0 && value < Console.BufferWidth - 1)
                {
                    this.xposition = value;
                }
            }
        }
        public byte Yposition 
        {
            get
            {
                return this.yposition;
            }
            set
            {
                if (value > 0 && value < Console.BufferHeight - 1)
                {
                    this.yposition = value;
                }
            }
        }


        public void MoveShip(Direction direction)
        {
            this.Direction = direction;

            Console.SetCursorPosition(this.Xposition, this.Yposition);
            Console.Write(' ');

            switch (direction)
            {
                case Direction.Right:
                    Xposition++;
                    break;
                case Direction.Left:
                    Xposition--;
                    break;
                case Direction.Up:
                    Yposition--;
                    break;
                case Direction.Down:
                    Yposition++;
                    break;
                default:
                    break;
            }

            Console.SetCursorPosition(this.Xposition, this.Yposition);
            Console.Write(this.CurrentChar);
        }
    }
}
