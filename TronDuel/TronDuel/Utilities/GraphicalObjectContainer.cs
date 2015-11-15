namespace TronDuel.Utilities
{
    using System;
    using System.Collections.Generic;
    using TronDuel.GraphicalObjects;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects.Powerups;
    using TronDuel.MovingObjects.GraphicalObjects;
    using TronDuel.GraphicalObjects.MovingObjects;

    public class GraphicalObjectContainer
    {
        private SpaceShip spaceShipPlayerOne;
        private SpaceShip spaceShipPlayerTwo;

        private IList<HealthBonus> hearts = new List<HealthBonus>();
        private IList<ShieldBonus> shields = new List<ShieldBonus>();
        private IList<AmmoBonus> ammo = new List<AmmoBonus>();
        private IList<TronBonus> tronBonuses = new List<TronBonus>();
        private IList<Projectile> projectiles = new List<Projectile>();
        private IList<MovingEnemy> movingEnemies = new List<MovingEnemy>();
        private IList<StationaryEnemy> stationaryEnemies = new List<StationaryEnemy>();

        private IList<TronDotsContainer> tronDotsContainers = new List<TronDotsContainer>();

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
            hearts.Add(new HealthBonus(50, 25, ConsoleColor.Red, 20));
            shields.Add(new ShieldBonus(40, 5, ConsoleColor.Yellow, 10));
            ammo.Add(new AmmoBonus(35, 15, ConsoleColor.White, 20));
            tronBonuses.Add(new TronBonus(10, 20, ConsoleColor.Cyan));
            movingEnemies.Add(new MovingEnemy(65, 25, ConsoleColor.Gray, this.SpaceShipPlayerOne));
            stationaryEnemies.Add(new StationaryEnemy(55, 25, ConsoleColor.Magenta));
        }

        public List<GraphicalObject> GetAll()
        {
            List<GraphicalObject> allObjects = new List<GraphicalObject>();

            if (spaceShipPlayerTwo != null)
            {
                allObjects.Add(spaceShipPlayerTwo);
            }

            foreach (var heart in hearts)
            {
                allObjects.Add(heart);
            }

            foreach (var shield in shields)
            {
                allObjects.Add(shield);
            }

            foreach (var ammoUnit in ammo)
            {
                allObjects.Add(ammoUnit);
            }

            foreach (var tronBonus in tronBonuses)
            {
                allObjects.Add(tronBonus);
            }

            foreach (var projectile in projectiles)
            {
                allObjects.Add(projectile);
            }

            foreach (var movingEnemy in movingEnemies)
            {
                allObjects.Add(movingEnemy);
            }

            foreach (var stationaryEnemy in stationaryEnemies)
            {
                allObjects.Add(stationaryEnemy);
            }

            foreach (var container in tronDotsContainers)
            {
                foreach (var dot in container.Dots)
                {
                    allObjects.Add(dot);
                }
            }

            // The spaceship is put at the end so that it is drawn on top
            allObjects.Add(spaceShipPlayerOne); 
            
            return allObjects;
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

        public IList<ShieldBonus> Shields
        {
            get
            {
                return this.shields;
            }
        }

        public IList<AmmoBonus> Ammo
        {
            get
            {
                return this.ammo;
            }
        }

        public IList<TronBonus> TronBonuses
        {
            get
            {
                return this.tronBonuses;
            }
        }

        public IList<Projectile> Projectiles
        {
            get
            {
                return this.projectiles;
            }
        }

        public IList<MovingEnemy> MovingEnemies
        {
            get
            {
                return this.movingEnemies;
            }
        }

        public IList<StationaryEnemy> StationaryEnemies
        {
            get
            {
                return this.stationaryEnemies;
            }
        }

        public IList<TronDotsContainer> TronDotsContainers
        {
            get
            {
                return this.tronDotsContainers;
            }
        }
    }
}