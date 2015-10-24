namespace TronDuel.GraphicalObjects
{
    using System;

    public abstract class GraphicalObject
    {
        private double xposition;
        private double yposition;

        public GraphicalObject(double startingPositionX, double startingPositionY, ConsoleColor color)
        {
            this.Xposition = startingPositionX;
            this.Yposition = startingPositionY;
            this.Color = color;
        }

        public double Xposition
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
        public double Yposition
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
