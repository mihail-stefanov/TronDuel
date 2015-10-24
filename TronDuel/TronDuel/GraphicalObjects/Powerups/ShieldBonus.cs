namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    public class ShieldBonus : GraphicalObject
    {
        public ShieldBonus(byte startingPositionX, byte startingPositionY, ConsoleColor color, int timeInvincibleInMilliseconds)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = '♦';
            this.TimeInvincibleInMilliseconds = timeInvincibleInMilliseconds;
        }

        public int TimeInvincibleInMilliseconds { get; set; }
    }
}
