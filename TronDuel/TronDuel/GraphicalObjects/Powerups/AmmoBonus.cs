namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    public class AmmoBonus : GraphicalObject
    {
        public AmmoBonus(byte startingPositionX, byte startingPositionY, ConsoleColor color, byte bonusPoints)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = '@';
            this.BonusPoints = bonusPoints;
        }

        public byte BonusPoints { get; set; }
    }
}
