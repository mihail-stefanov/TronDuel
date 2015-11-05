namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    public class HealthBonus : GraphicalObject
    {
        public HealthBonus(byte startingPositionX, byte startingPositionY, ConsoleColor color, sbyte bonusPoints)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = '♥';
            this.BonusPoints = bonusPoints;
        }

        public sbyte BonusPoints { get; set; }
    }
}
