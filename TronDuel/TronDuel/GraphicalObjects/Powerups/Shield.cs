namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    public class Shield : GraphicalObject
    {
        public Shield(byte startingPositionX, byte startingPositionY, ConsoleColor color, int timeInvincibleInMilliseconds)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = '♦';
            this.TimeInvincibleInMilliseconds = timeInvincibleInMilliseconds;
        }

        public int TimeInvincibleInMilliseconds { get; set; }
    }
}
