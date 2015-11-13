namespace TronDuel.GraphicalObjects
{
    using System;
    using System.Diagnostics;

    public class TronDot : GraphicalObject
    {
        private Stopwatch lifespantimer = new Stopwatch();
        private int lifespan = 5000;
        private bool lifespanOver = false;

        public TronDot(
            byte startingPositionX, 
            byte startingPositionY, 
            ConsoleColor color)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = 'o';
            lifespantimer.Start();
        }

        public bool IsLifespanOver()
        {
            if (this.lifespantimer.ElapsedMilliseconds > lifespan)
            {
                this.lifespanOver = true;
            }

            return this.lifespanOver;
        }

        public void EraseDot()
        {
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.Write(' ');
        }
    }
}
