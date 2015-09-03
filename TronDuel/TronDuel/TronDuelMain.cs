﻿namespace TronDuel
{
    using System;
    using GraphicalObjects;
    using GraphicalObjects.Enumerations;
    using GraphicalObjects.Powerups;

    public class TronDuelMain
    {
        static void Main()
        {
            // Console window settings
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 70;
            Console.CursorVisible = false;

            // Initialisation of all objects
            SpaceShip spaceShipPlayerOne = new SpaceShip(10, 10, ConsoleColor.Green, Direction.Right);
            HealthBonus heart = new HealthBonus(50, 25, ConsoleColor.Red, 100);
            Shield shield = new Shield(40, 5, ConsoleColor.Yellow, 10000);

            // Looking for keypresses and controlling actions in each frame
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey();

                    if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        spaceShipPlayerOne.MoveShip(Direction.Right);
                    }
                    else if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        spaceShipPlayerOne.MoveShip(Direction.Left);
                    }
                    else if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        spaceShipPlayerOne.MoveShip(Direction.Up);
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        spaceShipPlayerOne.MoveShip(Direction.Down);
                    }
                }
            }
        }
    }
}
