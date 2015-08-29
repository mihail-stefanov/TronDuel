﻿namespace TronDuel
{
    public class SpaceShip
    {
        private const char ShipCharRight = '►';
        private const char ShipCharLeft = '◄';
        private const char ShipCharUp = '▲';
        private const char ShipCharDown = '▼';

        private char currentChar;
        private Direction direction;

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

        public byte Xposition { get; set; }
        public byte Yposition { get; set; }
    }
}
