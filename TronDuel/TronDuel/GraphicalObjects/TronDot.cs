namespace TronDuel.GraphicalObjects
{
    using System;
    using System.Diagnostics;

    public class TronDot : GraphicalObject
    {
        private Stopwatch lifespantimer = new Stopwatch();
        private int lifespan = 3000;
        private bool lifespanOver = false;

        public TronDot(
            byte startingPositionX, 
            byte startingPositionY, 
            ConsoleColor color)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = 'o';
            this.lifespantimer.Start();
        }

        public bool IsLifespanOver()
        {
            if (this.lifespantimer.ElapsedMilliseconds > this.lifespan)
            {
                this.lifespanOver = true;
            }

            return this.lifespanOver;
        }

        public void EraseDot()
        {
            Console.SetCursorPosition((byte)this.Xposition, (byte)this.Yposition);
            Console.Write(' ');
        }
    }
}
