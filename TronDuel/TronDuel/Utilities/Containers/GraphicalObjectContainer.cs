namespace TronDuel.Utilities.Containers
{
    using System;
    using System.Collections.Generic;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects;
    using TronDuel.GraphicalObjects.Bonuses;
    using TronDuel.GraphicalObjects.Containers;
    using TronDuel.GraphicalObjects.Enemies;
    using TronDuel.MovingObjects.GraphicalObjects;

    public class GraphicalObjectContainer
    {
        private SpaceShip spaceShipPlayerOne;
        private SpaceShip spaceShipPlayerTwo;

        private IList<GraphicalObject> bonuses = new List<GraphicalObject>();
        private IList<Projectile> projectiles = new List<Projectile>();
        private IList<MovingEnemy> movingEnemies = new List<MovingEnemy>();
        private IList<StationaryEnemy> stationaryEnemies = new List<StationaryEnemy>();
        private IList<TronDotsContainer> tronDotsContainers = new List<TronDotsContainer>();

        public GraphicalObjectContainer(int numberOfPlayers)
        {
            if (numberOfPlayers == 2)
            {
                // The two player behavior is currently not implemented
                this.spaceShipPlayerTwo = new SpaceShip(10, 10, ConsoleColor.Yellow, Direction.Left);
            }
            else if (numberOfPlayers != 1)
            {
                throw new ArgumentException("Only 1 or two players are supported!");
            }

            // Initialisation of all objects
            this.spaceShipPlayerOne = new SpaceShip(10, 10, ConsoleColor.Green, Direction.Right);
            this.bonuses.Add(new HealthBonus(50, 25, ConsoleColor.Red, 20));
            this.bonuses.Add(new ShieldBonus(40, 5, ConsoleColor.Yellow, 10));
            this.bonuses.Add(new AmmoBonus(35, 15, ConsoleColor.White, 20));
            this.bonuses.Add(new TronBonus(10, 20, ConsoleColor.Cyan));
            this.movingEnemies.Add(new MovingEnemy(65, 25, ConsoleColor.Gray, this.SpaceShipPlayerOne));
            this.stationaryEnemies.Add(new StationaryEnemy(55, 25, ConsoleColor.Magenta));
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

        public IList<GraphicalObject> Bonuses
        {
            get
            {
                return this.bonuses;
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

        public List<GraphicalObject> GetAll()
        {
            List<GraphicalObject> allObjects = new List<GraphicalObject>();

            if (this.spaceShipPlayerTwo != null)
            {
                allObjects.Add(this.spaceShipPlayerTwo);
            }

            foreach (var bonus in this.bonuses)
            {
                allObjects.Add(bonus);
            }

            foreach (var projectile in this.projectiles)
            {
                allObjects.Add(projectile);
            }

            foreach (var movingEnemy in this.movingEnemies)
            {
                allObjects.Add(movingEnemy);
            }

            foreach (var stationaryEnemy in this.stationaryEnemies)
            {
                allObjects.Add(stationaryEnemy);
            }

            foreach (var container in this.tronDotsContainers)
            {
                foreach (var dot in container.Dots)
                {
                    allObjects.Add(dot);
                }
            }

            // The spaceship is put at the end so that it is drawn on top
            allObjects.Add(this.spaceShipPlayerOne);

            return allObjects;
        }
    }
}