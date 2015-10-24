namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    public class HealthBonus : GraphicalObject
    {
        private const char sprite = '♥';

        public HealthBonus(byte startingPositionX, byte startingPositionY, ConsoleColor color, byte bonusPoints)
            : base(startingPositionX, startingPositionY, color)
        {
            this.BonusPoints = bonusPoints;
        }

        public void Draw()
        {
            Console.ForegroundColor = this.Color;
            Console.SetCursorPosition((int)this.Xposition, (int)this.Yposition);
            Console.Write(sprite);
        }

        public byte BonusPoints { get; set; }
    }
}
