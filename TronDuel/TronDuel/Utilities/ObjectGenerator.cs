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
        private const int bonusGenerationInterval = 5000;

        private double numberOfEnemiesToGenerate = 2;

        private int oldNumberOfMovingEnemies = 1;
        private int newNumberOfMovingEnemies = 1;

        private int oldNumberOfStationaryEnemies = 1;
        private int newNumberOfStationaryEnemies = 1;

        private byte movingEnemyNumberLimit = 5;
        private byte stationaryEnemyNumberLimit = 2;

        private Bonus currentBonusToGenerate;

        public ObjectGenerator()
        {
            currentBonusToGenerate = Bonus.Ammo;
            bonusGeneratorTimer = new Stopwatch();
            bonusGeneratorTimer.Start();
        }

        public Stopwatch bonusGeneratorTimer { get; set; }

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
            if (bonusGeneratorTimer.ElapsedMilliseconds > bonusGenerationInterval)
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

                switch (currentBonusToGenerate)
                {
                    case Bonus.Heart:
                        graphicalObjects.Bonuses.Add(new HealthBonus(potentialXposition, potentialYposition, ConsoleColor.Red, 20));
                        currentBonusToGenerate = Bonus.Shield;
                        break;
                    case Bonus.Shield:
                        graphicalObjects.Bonuses.Add(new ShieldBonus(potentialXposition, potentialYposition, ConsoleColor.Yellow, 10));
                        currentBonusToGenerate = Bonus.Ammo;
                        break;
                    case Bonus.Ammo:
                        graphicalObjects.Bonuses.Add(new AmmoBonus(potentialXposition, potentialYposition, ConsoleColor.White, 20));
                        currentBonusToGenerate = Bonus.Tron;
                        break;
                    case Bonus.Tron:
                        graphicalObjects.Bonuses.Add(new TronBonus(potentialXposition, potentialYposition, ConsoleColor.Cyan));
                        currentBonusToGenerate = Bonus.Heart;
                        break;
                    default:
                        break;
                }

                bonusGeneratorTimer.Restart();
            }
        }

        public void GenerateEnemy(GraphicalObjectContainer graphicalObjects)
        {
            if (graphicalObjects.MovingEnemies.Count <= movingEnemyNumberLimit - numberOfEnemiesToGenerate)
            {
                newNumberOfMovingEnemies = graphicalObjects.MovingEnemies.Count;

                if (newNumberOfMovingEnemies != oldNumberOfMovingEnemies)
                {
                    for (int i = 0; i < numberOfEnemiesToGenerate; i++)
                    {
                        // TODO: Fix repeating code
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

                            for (int j = 0; j < currentGraphicalObjects.Count; j++)
                            {
                                if (currentGraphicalObjects[j].Xposition == potentialXposition &&
                                    currentGraphicalObjects[j].Yposition == potentialYposition)
                                {
                                    generationOnAnEmptySpaceSuccessful = false;
                                    break;
                                }
                            }
                        }

                        graphicalObjects.MovingEnemies.Add(new MovingEnemy(potentialXposition, potentialYposition, ConsoleColor.Gray, graphicalObjects.SpaceShipPlayerOne));

                        newNumberOfMovingEnemies = graphicalObjects.MovingEnemies.Count;
                        oldNumberOfMovingEnemies = newNumberOfMovingEnemies;
                    }
                }
            }

            if (graphicalObjects.StationaryEnemies.Count <= stationaryEnemyNumberLimit - numberOfEnemiesToGenerate)
            {
                newNumberOfStationaryEnemies = graphicalObjects.StationaryEnemies.Count;

                if (newNumberOfStationaryEnemies != oldNumberOfStationaryEnemies)
                {
                    for (int i = 0; i < numberOfEnemiesToGenerate; i++)
                    {
                        // TODO: Fix repeating code
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

                            for (int j = 0; j < currentGraphicalObjects.Count; j++)
                            {
                                if (currentGraphicalObjects[j].Xposition == potentialXposition &&
                                    currentGraphicalObjects[j].Yposition == potentialYposition)
                                {
                                    generationOnAnEmptySpaceSuccessful = false;
                                    break;
                                }
                            }
                        }

                        graphicalObjects.StationaryEnemies.Add(new StationaryEnemy(potentialXposition, potentialYposition, ConsoleColor.Magenta));

                        newNumberOfStationaryEnemies = graphicalObjects.StationaryEnemies.Count;
                        oldNumberOfStationaryEnemies = newNumberOfStationaryEnemies;
                    }
                }
            }
        }
    }
}
