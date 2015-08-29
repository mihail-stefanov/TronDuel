using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TronDuel
{
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
                        MoveShip(spaceShipPlayerOne, Direction.Right);
                    }
                    else if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        MoveShip(spaceShipPlayerOne, Direction.Left);
                    }
                    else if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        MoveShip(spaceShipPlayerOne, Direction.Up);
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        MoveShip(spaceShipPlayerOne, Direction.Down);
                    }
                }
            }
        }

        private static void MoveShip(SpaceShip spaceShipPlayerOne, Direction direction)
        {
            Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);
            Console.Write(' ');

            switch (direction)
            {
                case Direction.Right:
                    spaceShipPlayerOne.Direction = Direction.Right;
                    spaceShipPlayerOne.Xposition++;
                    break;
                case Direction.Left:
                    spaceShipPlayerOne.Direction = Direction.Left;
                    spaceShipPlayerOne.Xposition--;
                    break;
                case Direction.Up:
                    spaceShipPlayerOne.Direction = Direction.Up;
                    spaceShipPlayerOne.Yposition--;
                    break;
                case Direction.Down:
                    spaceShipPlayerOne.Direction = Direction.Down;
                    spaceShipPlayerOne.Yposition++;
                    break;
                default:
                    break;
            }

            Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);
            Console.Write(spaceShipPlayerOne.CurrentChar);
        }
    }
}
