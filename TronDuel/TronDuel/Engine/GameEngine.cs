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
        private int threadSleepTime = 30;

        private GraphicalObjectContainer graphicalObjects = new GraphicalObjectContainer(1);
        private SoundEffectContainer soundEffects = new SoundEffectContainer();
        private ObjectGenerator objectGenerator = new ObjectGenerator();
        private ScoreContainer scoreContainer;

        public GameEngine(ScoreContainer scoreContainer)
        {
            this.scoreContainer = scoreContainer;
        }

        public void Run()
        {
            soundEffects.PlayStart();

            CollisionResolver collisionResolver = new CollisionResolver(soundEffects, scoreContainer);

            while (true)
            {
                Thread.Sleep(threadSleepTime);

                ReadAndProcessCommands(graphicalObjects);

                UpdateObjects(graphicalObjects);

                collisionResolver.Resolve(graphicalObjects);

                DrawObjects(graphicalObjects);

                scoreContainer.DrawScore();

                // Go to game over screen
                if (graphicalObjects.SpaceShipPlayerOne.HealthPoints == 0)
                {
                    soundEffects.PlayExplosion();
                    break;
                }
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
            graphicalObjects.SpaceShipPlayerOne.DecreaseShieldTime(threadSleepTime);

            foreach (var projectile in graphicalObjects.Projectiles)
            {
                projectile.Move();
            }

            foreach (var enemy in graphicalObjects.Enemies)
            {
                enemy.Move();
                enemy.FireWeapon(graphicalObjects, soundEffects);
            }

            for (int i = 0; i < graphicalObjects.TronDotsContainers.Count; i++)
            {
                if (!graphicalObjects.TronDotsContainers[i].IsCapacityReached())
                {
                    graphicalObjects.TronDotsContainers[i].AddDot();
                }
                // Makes sure all dots are removed from the screen before removing the container
                else if (graphicalObjects.TronDotsContainers[i].Dots.Count == 0) 
                {
                    graphicalObjects.TronDotsContainers.Remove(graphicalObjects.TronDotsContainers[i]);
                    break;
                }

                for (int j = 0; j < graphicalObjects.TronDotsContainers[i].Dots.Count; j++)
                {
                    if (graphicalObjects.TronDotsContainers[i].Dots[j].IsLifespanOver())
                    {
                        graphicalObjects.TronDotsContainers[i].Dots[j].EraseDot();
                        graphicalObjects.TronDotsContainers[i].RemoveExpiredDot();
                    }
                }
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
