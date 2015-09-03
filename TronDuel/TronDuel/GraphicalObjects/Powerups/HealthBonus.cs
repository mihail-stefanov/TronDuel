namespace TronDuel.GraphicalObjects.Powerups
{
    using System;

    class HealthBonus : GraphicalObject
    {
        private const char sprite = '♥';

        public HealthBonus(byte startingPositionX, byte startingPositionY, ConsoleColor color, byte bonusPoints)
            : base(startingPositionX, startingPositionY, color)
        {
            this.BonusPoints = bonusPoints;
            Console.ForegroundColor = this.Color;
            Console.SetCursorPosition(this.Xposition, this.Yposition);

            //Print the object
            Console.Write(sprite);
        }

        public byte BonusPoints { get; set; }
    }
}
