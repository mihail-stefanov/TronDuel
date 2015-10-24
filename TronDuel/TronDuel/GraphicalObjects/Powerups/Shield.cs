namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    public class Shield : GraphicalObject
    {
        private const char sprite = '♦';

        public Shield(byte startingPositionX, byte startingPositionY, ConsoleColor color, int timeInvincibleInMilliseconds)
            : base(startingPositionX, startingPositionY, color)
        {
            this.TimeInvincibleInMilliseconds = timeInvincibleInMilliseconds;
        }

        public int TimeInvincibleInMilliseconds { get; set; }

        public void Draw()
        {
            Console.ForegroundColor = this.Color;
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.Write(sprite);
        }
    }
}
