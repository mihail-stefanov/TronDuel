namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    class Shield : GraphicalObject
    {
        private const char sprite = '♦';

        public Shield(byte startingPositionX, byte startingPositionY, ConsoleColor color, int timeInvincibleInMilliseconds)
            : base(startingPositionX, startingPositionY, color)
        {
            this.TimeInvincibleInMilliseconds = timeInvincibleInMilliseconds;
            Console.ForegroundColor = this.Color;
            Console.SetCursorPosition(this.Xposition, this.Yposition);

            //Print the object
            Console.Write(sprite);
        }

        public int TimeInvincibleInMilliseconds { get; set; }


    }
}
