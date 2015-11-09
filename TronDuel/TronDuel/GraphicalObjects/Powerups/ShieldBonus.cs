namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    public class ShieldBonus : GraphicalObject
    {
        public ShieldBonus(byte startingPositionX, byte startingPositionY, ConsoleColor color, byte timeInvincibleInSeconds)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = '♦';
            this.TimeInvincibleInSeconds = timeInvincibleInSeconds;
        }

        public byte TimeInvincibleInSeconds { get; set; }
    }
}
