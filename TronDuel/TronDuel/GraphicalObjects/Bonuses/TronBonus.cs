﻿namespace TronDuel.GraphicalObjects.Bonuses
{
    using System;

    public class TronBonus : GraphicalObject
    {
        public TronBonus(byte startingPositionX, byte startingPositionY, ConsoleColor color)
            : base(startingPositionX, startingPositionY, color)
        {
            this.Sprite = 'T';
        }
    }
}
