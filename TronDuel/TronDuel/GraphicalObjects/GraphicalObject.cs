namespace TronDuel.GraphicalObjects
{
    using System;

    public class GraphicalObject
    {
        private byte xposition;
        private byte yposition;

        public GraphicalObject(byte startingPositionX, byte startingPositionY, ConsoleColor color)
        {
            this.Xposition = startingPositionX;
            this.Yposition = startingPositionY;
            this.Color = color;

            // Place the object it in its initial position
            Console.ForegroundColor = this.Color;
            Console.SetCursorPosition(this.Xposition, this.Yposition);
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

        public ConsoleColor Color { get; set; }
    }
}
