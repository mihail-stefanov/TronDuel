namespace TronDuel.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TronDuel.GraphicalObjects;
    using TronDuel.GraphicalObjects.Enumerations;
    using TronDuel.GraphicalObjects.Powerups;

    public class PowerupGenerator
    {
        private const int generationInterval = 10000;

        private Powerup currentPowerUpToGenerate;

        public PowerupGenerator()
        {
            currentPowerUpToGenerate = Powerup.Ammo;
            generatorTimer = new Stopwatch();
            generatorTimer.Start();
        }

        public Stopwatch generatorTimer { get; set; }

        public void GeneratePowerup(GraphicalObjectContainer graphicalObjects)
        {
            if (generatorTimer.ElapsedMilliseconds > generationInterval)
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
                        graphicalObjects.Hearts.Add(new HealthBonus(potentialXposition, potentialYposition, ConsoleColor.Red, 50));
                        currentPowerUpToGenerate = Powerup.Shield;
                        break;
                    case Powerup.Shield:
                        graphicalObjects.Shields.Add(new ShieldBonus(potentialXposition, potentialYposition, ConsoleColor.Yellow, 5000));
                        currentPowerUpToGenerate = Powerup.Ammo;
                        break;
                    case Powerup.Ammo:
                        graphicalObjects.Ammo.Add(new AmmoBonus(potentialXposition, potentialYposition, ConsoleColor.White, 20));
                        currentPowerUpToGenerate = Powerup.Heart;
                        break;
                    default:
                        break;
                }

                generatorTimer.Restart();
            }
        }
    }
}
