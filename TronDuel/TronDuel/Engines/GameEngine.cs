namespace TronDuel.Engines
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects;
    using TronDuel.Interfaces;
    using TronDuel.Utilities;
    using TronDuel.Utilities.Containers;

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
            this.soundEffects.PlayStart();

            CollisionResolver collisionResolver = new CollisionResolver(this.soundEffects, this.scoreContainer);
            DifficultyController difficultyController =
                new DifficultyController(
                    this.graphicalObjects,
                    this.objectGenerator,
                    this.scoreContainer);

            while (true)
            {
                Thread.Sleep(this.threadSleepTime);

                this.ReadAndProcessCommands(this.graphicalObjects);

                this.UpdateObjects();

                collisionResolver.Resolve(this.graphicalObjects);

                this.DrawObjects();

                this.scoreContainer.DrawScore();

                difficultyController.UpdateDifficulty();

                // Go to game over screen condition
                if (this.graphicalObjects.SpaceShipPlayerOne.HealthPoints == 0)
                {
                    this.soundEffects.PlayExplosion();
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
                    this.graphicalObjects.SpaceShipPlayerOne.ChangeDirection(Direction.Right, this.graphicalObjects);
                }

                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    this.graphicalObjects.SpaceShipPlayerOne.ChangeDirection(Direction.Left, this.graphicalObjects);
                }

                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    this.graphicalObjects.SpaceShipPlayerOne.ChangeDirection(Direction.Up, this.graphicalObjects);
                }

                if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    this.graphicalObjects.SpaceShipPlayerOne.ChangeDirection(Direction.Down, this.graphicalObjects);
                }

                // Firing weapons
                if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    this.graphicalObjects.SpaceShipPlayerOne.FireCurrentWeapon(this.graphicalObjects, this.soundEffects);
                }
            }
        }

        private void UpdateObjects()
        {
            this.graphicalObjects.SpaceShipPlayerOne.Move();

            this.graphicalObjects.SpaceShipPlayerOne.DecreaseShieldTime(this.threadSleepTime);

            foreach (var projectile in this.graphicalObjects.Projectiles)
            {
                projectile.Move();
            }

            foreach (var movingEnemy in this.graphicalObjects.MovingEnemies)
            {
                movingEnemy.Move();
            }

            foreach (var stationaryEnemy in this.graphicalObjects.StationaryEnemies)
            {
                stationaryEnemy.FireWeapon(this.graphicalObjects, this.soundEffects);
            }

            for (int i = 0; i < this.graphicalObjects.TronDotsContainers.Count; i++)
            {
                if (!this.graphicalObjects.TronDotsContainers[i].IsCapacityReached())
                {
                    this.graphicalObjects.TronDotsContainers[i].AddDot();
                }
                else if (this.graphicalObjects.TronDotsContainers[i].Dots.Count == 0)
                {
                    // Makes sure all dots are removed from the screen before removing the container
                    this.graphicalObjects.TronDotsContainers.Remove(this.graphicalObjects.TronDotsContainers[i]);
                    break;
                }

                for (int j = 0; j < this.graphicalObjects.TronDotsContainers[i].Dots.Count; j++)
                {
                    if (this.graphicalObjects.TronDotsContainers[i].Dots[j].IsLifespanOver())
                    {
                        this.graphicalObjects.TronDotsContainers[i].Dots[j].EraseDot();
                        this.graphicalObjects.TronDotsContainers[i].RemoveExpiredDot();
                    }
                }
            }

            this.objectGenerator.GeneratePowerup(this.graphicalObjects);
            this.objectGenerator.GenerateEnemy(this.graphicalObjects);
        }

        private void DrawObjects()
        {
            List<GraphicalObject> currentObjects = this.graphicalObjects.GetAll();

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
