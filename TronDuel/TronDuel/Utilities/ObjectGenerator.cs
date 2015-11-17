namespace TronDuel.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects;
    using TronDuel.GraphicalObjects.Bonuses;
    using TronDuel.GraphicalObjects.Enemies;
    using TronDuel.Utilities.Containers;

    public class ObjectGenerator
    {
        private const int BonusGenerationInterval = 5000;

        private int numberOfEnemiesToGenerate = 2;

        private int oldNumberOfMovingEnemies = 1;
        private int oldNumberOfStationaryEnemies = 1;

        private byte movingEnemyNumberLimit = 5;
        private byte stationaryEnemyNumberLimit = 2;

        private Bonus currentBonusToGenerate;

        public ObjectGenerator()
        {
            this.currentBonusToGenerate = Bonus.Ammo;
            this.BonusGeneratorTimer = new Stopwatch();
            this.BonusGeneratorTimer.Start();
        }

        public Stopwatch BonusGeneratorTimer { get; set; }

        public byte MovingEnemyNumberLimit
        {
            get
            {
                return this.movingEnemyNumberLimit;
            }

            set
            {
                this.movingEnemyNumberLimit = value;
            }
        }

        public byte StationaryEnemyNumberLimit
        {
            get
            {
                return this.stationaryEnemyNumberLimit;
            }

            set
            {
                this.stationaryEnemyNumberLimit = value;
            }
        }

        public void GenerateBonus(GraphicalObjectContainer graphicalObjects)
        {
            if (this.BonusGeneratorTimer.ElapsedMilliseconds > BonusGenerationInterval)
            {
                Random randomGenerator = new Random();

                bool generationOnAnEmptySpaceSuccessful = false;

                byte potentialXposition = 0;
                byte potentialYposition = 0;
                
                while (!generationOnAnEmptySpaceSuccessful)
                {
                    generationOnAnEmptySpaceSuccessful = true;

                    potentialXposition = (byte)randomGenerator.Next(1, Console.BufferWidth - 2);
                    potentialYposition = (byte)randomGenerator.Next(1, Console.BufferHeight - 2);

                    List<GraphicalObject> currentGraphicalObjects = graphicalObjects.GetAll();

                    for (int i = 0; i < currentGraphicalObjects.Count; i++)
                    {
                        if (currentGraphicalObjects[i].Xposition == potentialXposition &&
                            currentGraphicalObjects[i].Yposition == potentialYposition)
                        {
                            generationOnAnEmptySpaceSuccessful = false;
                            break;
                        }
                    }
                }

                switch (this.currentBonusToGenerate)
                {
                    case Bonus.Heart:
                        graphicalObjects.Bonuses.Add(new HealthBonus(potentialXposition, potentialYposition, ConsoleColor.Red, 20));
                        this.currentBonusToGenerate = Bonus.Shield;
                        break;
                    case Bonus.Shield:
                        graphicalObjects.Bonuses.Add(new ShieldBonus(potentialXposition, potentialYposition, ConsoleColor.Yellow, 10));
                        this.currentBonusToGenerate = Bonus.Ammo;
                        break;
                    case Bonus.Ammo:
                        graphicalObjects.Bonuses.Add(new AmmoBonus(potentialXposition, potentialYposition, ConsoleColor.White, 20));
                        this.currentBonusToGenerate = Bonus.Tron;
                        break;
                    case Bonus.Tron:
                        graphicalObjects.Bonuses.Add(new TronBonus(potentialXposition, potentialYposition, ConsoleColor.Cyan));
                        this.currentBonusToGenerate = Bonus.Heart;
                        break;
                    default:
                        break;
                }

                this.BonusGeneratorTimer.Restart();
            }
        }

        public void GenerateEnemy(GraphicalObjectContainer graphicalObjects)
        {
            int numberOfMovingEnemies = 0;
            int numberOfStationaryEnemies = 0;

            for (int i = 0; i < graphicalObjects.Enemies.Count; i++)
            {
                if (graphicalObjects.Enemies[i].GetType() == typeof(MovingEnemy))
                {
                    numberOfMovingEnemies++;
                }
                else if (graphicalObjects.Enemies[i].GetType() == typeof(StationaryEnemy))
                {
                    numberOfStationaryEnemies++;
                }
            }

            if (numberOfMovingEnemies <= this.movingEnemyNumberLimit - this.numberOfEnemiesToGenerate)
            {
                if (numberOfMovingEnemies != this.oldNumberOfMovingEnemies)
                {
                    for (int i = 0; i < this.numberOfEnemiesToGenerate; i++)
                    {
                        byte potentialXposition;
                        byte potentialYposition;

                        FindEmptySpace(graphicalObjects, out potentialXposition, out potentialYposition);

                        graphicalObjects.Enemies.Add(new MovingEnemy(potentialXposition, potentialYposition, ConsoleColor.Gray, graphicalObjects.SpaceShipPlayerOne));
                    }

                    this.oldNumberOfMovingEnemies = numberOfMovingEnemies + this.numberOfEnemiesToGenerate;
                }
            }

            if (numberOfStationaryEnemies <= this.stationaryEnemyNumberLimit - this.numberOfEnemiesToGenerate)
            {
                if (numberOfStationaryEnemies != this.oldNumberOfStationaryEnemies)
                {
                    for (int i = 0; i < this.numberOfEnemiesToGenerate; i++)
                    {
                        byte potentialXposition;
                        byte potentialYposition;

                        FindEmptySpace(graphicalObjects, out potentialXposition, out potentialYposition);

                        graphicalObjects.Enemies.Add(new StationaryEnemy(potentialXposition, potentialYposition, ConsoleColor.Magenta));
                    }

                    this.oldNumberOfStationaryEnemies = numberOfStationaryEnemies + this.numberOfEnemiesToGenerate;
                }
            }
        }

        private static void FindEmptySpace(GraphicalObjectContainer graphicalObjects, out byte potentialXposition, out byte potentialYposition)
        {
            Random randomGenerator = new Random();

            bool generationOnAnEmptySpaceSuccessful = false;

            potentialXposition = 0;
            potentialYposition = 0;

            while (!generationOnAnEmptySpaceSuccessful)
            {
                generationOnAnEmptySpaceSuccessful = true;

                potentialXposition = (byte)randomGenerator.Next(1, Console.BufferWidth - 2);
                potentialYposition = (byte)randomGenerator.Next(1, Console.BufferHeight - 2);

                List<GraphicalObject> currentGraphicalObjects = graphicalObjects.GetAll();

                for (int i = 0; i < currentGraphicalObjects.Count; i++)
                {
                    if (currentGraphicalObjects[i].Xposition == potentialXposition &&
                        currentGraphicalObjects[i].Yposition == potentialYposition)
                    {
                        generationOnAnEmptySpaceSuccessful = false;
                        break;
                    }
                }
            }
        }
    }
}
