namespace TronDuel.Engine
{
    using System;
    using System.Threading;
    using TronDuel.GraphicalObjects;
    using TronDuel.GraphicalObjects.Enumerations;
    using TronDuel.Interfaces;
    using TronDuel.Utilities;

    class GameEngine : IEngine
    {
        public void Run()
        {
            GraphicalObjectContainer graphicalObjects = new GraphicalObjectContainer(1);

            SoundEffectContainer soundEffects = new SoundEffectContainer();

            CollisionResolver collisionResolver = new CollisionResolver();

            while (true)
            {
                Thread.Sleep(20);

                ReadAndProcessCommands(graphicalObjects);

                UpdateObjects(graphicalObjects);

                collisionResolver.Resolve(graphicalObjects, soundEffects);

                DrawObjects(graphicalObjects);
            }
        }

        private void ReadAndProcessCommands(GraphicalObjectContainer graphicalObjects)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Right;
                }
                else if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Left;
                }
                else if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Up;
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Down;
                }
            }
        }

        private void UpdateObjects(GraphicalObjectContainer graphicalObjects)
        {
            graphicalObjects.SpaceShipPlayerOne.MoveShip(graphicalObjects.SpaceShipPlayerOne.Direction);
        }

        private void DrawObjects(GraphicalObjectContainer graphicalObjects)
        {
            graphicalObjects.SpaceShipPlayerOne.Draw();

            foreach (var heart in graphicalObjects.Hearts)
            {
                heart.Draw();
            }

            foreach (var shield in graphicalObjects.Shields)
            {
                shield.Draw();
            }
        }
    }
}
