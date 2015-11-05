namespace TronDuel.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using TronDuel.GraphicalObjects;
    using TronDuel.Enumerations;
    using TronDuel.Interfaces;
    using TronDuel.Utilities;

    public class GameEngine : IEngine
    {
        private GraphicalObjectContainer graphicalObjects = new GraphicalObjectContainer(1);
        private SoundEffectContainer soundEffects = new SoundEffectContainer();
        private ObjectGenerator objectGenerator = new ObjectGenerator();

        public void Run()
        {
            CollisionResolver collisionResolver = new CollisionResolver(soundEffects);

            while (true)
            {
                Thread.Sleep(50);

                ReadAndProcessCommands(graphicalObjects);

                UpdateObjects(graphicalObjects);

                collisionResolver.Resolve(graphicalObjects);

                DrawObjects(graphicalObjects);
            }
        }

        private void ReadAndProcessCommands(GraphicalObjectContainer graphicalObjects)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                // Direction control
                if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Right;
                }

                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Left;
                }

                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Up;
                }

                if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    graphicalObjects.SpaceShipPlayerOne.Direction = Direction.Down;
                }

                // Firing weapons
                if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    graphicalObjects.SpaceShipPlayerOne.FireCurrentWeapon(graphicalObjects, soundEffects);
                }
                
            }
        }

        private void UpdateObjects(GraphicalObjectContainer graphicalObjects)
        {
            graphicalObjects.SpaceShipPlayerOne.Move();

            foreach (var projectile in graphicalObjects.Projectiles)
            {
                projectile.Move();
            }

            foreach (var enemy in graphicalObjects.Enemies)
            {
                enemy.Move();
                enemy.FireWeapon(graphicalObjects, soundEffects);
            }

            objectGenerator.GeneratePowerup(graphicalObjects);
            objectGenerator.GenerateEnemy(graphicalObjects);
        }

        private void DrawObjects(GraphicalObjectContainer graphicalObjects)
        {
            List<GraphicalObject> currentObjects = graphicalObjects.GetAll();

            foreach (var currentObject in currentObjects)
            {
                if (currentObject.Xposition > 0 && currentObject.Xposition < Console.BufferWidth &&
                    currentObject.Yposition > 0 && currentObject.Yposition < Console.BufferHeight)
                {
                    currentObject.Draw();
                }
            }
        }
    }
}
