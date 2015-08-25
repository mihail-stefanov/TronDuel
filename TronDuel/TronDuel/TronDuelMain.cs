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

            SpaceShip spaceShipPlayerOne = new SpaceShip(10, 10);

            Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);
            Console.Write(SpaceShip.ShipCharRight);

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
                    spaceShipPlayerOne.Xposition++;
                    Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);
                    Console.Write(SpaceShip.ShipCharRight);
                    break;
                case Direction.Left:
                    spaceShipPlayerOne.Xposition--;
                    Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);
                    Console.Write(SpaceShip.ShipCharLeft);
                    break;
                case Direction.Up:
                    spaceShipPlayerOne.Yposition--;
                    Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);
                    Console.Write(SpaceShip.ShipCharUp);
                    break;
                case Direction.Down:
                    spaceShipPlayerOne.Yposition++;
                    Console.SetCursorPosition(spaceShipPlayerOne.Xposition, spaceShipPlayerOne.Yposition);
                    Console.Write(SpaceShip.ShipCharDown);
                    break;
                default:
                    break;
            }
        }
    }
}
