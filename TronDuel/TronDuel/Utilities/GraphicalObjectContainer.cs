namespace TronDuel.Utilities
{
    using System;
    using System.Collections.Generic;
    using TronDuel.GraphicalObjects;
    using TronDuel.GraphicalObjects.Enumerations;
    using TronDuel.GraphicalObjects.Powerups;

    public class GraphicalObjectContainer
    {
        private SpaceShip spaceShipPlayerOne;
        private SpaceShip spaceShipPlayerTwo;

        private IList<HealthBonus> hearts = new List<HealthBonus>();
        private IList<Shield> shields = new List<Shield>();
        private IList<Projectile> projectiles = new List<Projectile>();

        public GraphicalObjectContainer(int numberOfPlayers)
        {
            if (numberOfPlayers == 2)
            {
                spaceShipPlayerTwo = new SpaceShip(10, 10, ConsoleColor.Yellow, Direction.Left);
            }
            else if (numberOfPlayers != 1)
            {
                throw new ArgumentException("Only 1 or two players are supported!");
            }

            // Initialisation of all objects
            spaceShipPlayerOne = new SpaceShip(10, 10, ConsoleColor.Green, Direction.Right);
            hearts.Add(new HealthBonus(50, 25, ConsoleColor.Red, 100));
            shields.Add(new Shield(40, 5, ConsoleColor.Yellow, 10000));
        }

        public SpaceShip SpaceShipPlayerOne
        {
            get
            {
                return this.spaceShipPlayerOne;
            }
        }

        public SpaceShip SpaceShipPlayerTwo
        {
            get
            {
                return this.spaceShipPlayerTwo;
            }
        }

        public IList<HealthBonus> Hearts
        {
            get
            {
                return this.hearts;
            }
        }

        public IList<Shield> Shields
        {
            get
            {
                return this.shields;
            }
        }

        public IList<Projectile> Projectiles
        {
            get
            {
                return this.projectiles;
            }
        }
    }
}