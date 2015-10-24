namespace TronDuel.GraphicalObjects
{
    using System;
    using Enumerations;

    public class SpaceShip : GraphicalObject
    {
        private const char ShipCharRight = '►';
        private const char ShipCharLeft = '◄';
        private const char ShipCharUp = '▲';
        private const char ShipCharDown = '▼';

        private const double speedX = 0.5;
        private const double speedY = 0.3;

        private char currentChar;

        private Direction direction;

        public SpaceShip(byte startingPositionX, byte startingPositionY, ConsoleColor color, Direction direction)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Direction = Direction.Right;
            this.CurrentChar = ShipCharRight;
            this.HealthPoints = 50;

            // Print the object
            Console.Write(this.CurrentChar);

            PrintHealth();
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
                    // Changing the graphical direction of the ship upon changing the direction property
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

        protected char CurrentChar
        {
            get
            {
                return this.currentChar;
            }

            set
            {
                this.currentChar = value;
            }
        }

        public byte HealthPoints { get; set; }

        internal void MoveShip(Direction direction)
        {
            this.Direction = direction;

            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.Write(' ');

            switch (direction)
            {
                case Direction.Right:
                    Xposition += speedX;
                    break;
                case Direction.Left:
                    Xposition -= speedX;
                    break;
                case Direction.Up:
                    Yposition -= speedY;
                    break;
                case Direction.Down:
                    Yposition += speedY;
                    break;
                default:
                    break;
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.ForegroundColor = this.Color;
            Console.Write(this.CurrentChar);

            PrintHealth();
        }

        internal void ChangeHealth(byte healthPoints)
        {
            if (this.HealthPoints + healthPoints > 100)
            {
                this.HealthPoints = 100;
            }
            else if (this.HealthPoints + healthPoints <= 0)
            {
                this.HealthPoints = 0;

                // TODO: Gameover logic call here
            }
            else
            {
                this.HealthPoints += healthPoints;
            }
        }

        private void PrintHealth()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = this.Color;
            Console.Write("Player 1 - Health: {0}", this.HealthPoints);
        }
    }
}
