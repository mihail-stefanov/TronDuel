namespace TronDuel.GraphicalObjects
{
    using System;

    public abstract class GraphicalObject
    {
        private double xposition;
        private double yposition;

        private char sprite;

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
                this.xposition = value;
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
                this.yposition = value;
            }
        }

        protected char Sprite
        {
            get
            {
                return this.sprite;
            }

            set
            {
                this.sprite = value;
            }
        }

        public ConsoleColor Color { get; set; }

        public virtual void Draw()
        {
            Console.ForegroundColor = this.Color;
            Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
            Console.Write(this.Sprite);
        }
    }
}
