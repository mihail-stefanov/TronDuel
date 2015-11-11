namespace TronDuel.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TronDuel.GraphicalObjects;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects.Powerups;
    using TronDuel.GraphicalObjects.MovingObjects;

    public class ObjectGenerator
    {
        private const int powerupGenerationInterval = 10000;
        private int enemyGenerationInterval = 15000;

        private Powerup currentPowerUpToGenerate;

        public ObjectGenerator()
        {
            currentPowerUpToGenerate = Powerup.Ammo;
            powerupGeneratorTimer = new Stopwatch();
            enemyGeneratorTimer = new Stopwatch();
            powerupGeneratorTimer.Start();
            enemyGeneratorTimer.Start();
        }

        public Stopwatch powerupGeneratorTimer { get; set; }
        public Stopwatch enemyGeneratorTimer { get; set; }

        public void GeneratePowerup(GraphicalObjectContainer graphicalObjects)
        {
            if (powerupGeneratorTimer.ElapsedMilliseconds > powerupGenerationInterval)
            {
                Random randomGenerator = new Random();

                bool generationOnAnEmptySpaceSuccessful = false;

                byte potentialXposition = 0;
                byte potentialYposition = 0;
                // TODO: Create an external Timer!!!
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

                switch (currentPowerUpToGenerate)
                {
                    case Powerup.Heart:
                        graphicalObjects.Hearts.Add(new HealthBonus(potentialXposition, potentialYposition, ConsoleColor.Red, 20));
                        currentPowerUpToGenerate = Powerup.Shield;
                        break;
                    case Powerup.Shield:
                        graphicalObjects.Shields.Add(new ShieldBonus(potentialXposition, potentialYposition, ConsoleColor.Yellow, 10));
                        currentPowerUpToGenerate = Powerup.Ammo;
                        break;
                    case Powerup.Ammo:
                        graphicalObjects.Ammo.Add(new AmmoBonus(potentialXposition, potentialYposition, ConsoleColor.White, 20));
                        currentPowerUpToGenerate = Powerup.Heart;
                        break;
                    default:
                        break;
                }

                powerupGeneratorTimer.Restart();
            }
        }

        public void GenerateEnemy(GraphicalObjectContainer graphicalObjects)
        {
            if (enemyGeneratorTimer.ElapsedMilliseconds > enemyGenerationInterval)
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

                graphicalObjects.Enemies.Add(new Enemy(potentialXposition, potentialYposition, ConsoleColor.Gray, graphicalObjects.SpaceShipPlayerOne));

                // Reducing the interval for enemy generation on each generation
                if (enemyGenerationInterval > 5000)
                {
                    enemyGenerationInterval = (int)(enemyGenerationInterval * 0.9);
                }
                
                enemyGeneratorTimer.Restart();
            }
        }
    }
}
