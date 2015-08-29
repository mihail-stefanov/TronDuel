namespace TronDuel
{
    using System;

    public class TronDuelMain
    {
        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 70;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;

            SpaceShip spaceShipPlayerOne = new SpaceShip(10, 10, Direction.Right);

            Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);

            Console.Write(spaceShipPlayerOne.CurrentChar);

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
