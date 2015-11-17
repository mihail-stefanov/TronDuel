namespace TronDuel.Utilities
{
    using System;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects.Bonuses;
    using TronDuel.GraphicalObjects.Containers;
    using TronDuel.GraphicalObjects.Enemies;
    using TronDuel.MovingObjects.GraphicalObjects;
    using TronDuel.Utilities.Containers;

    public class CollisionResolver
    {
        private SoundEffectContainer soundEffects;
        private ScoreContainer scoreContainer;
        private int stationaryEnemyDestructionScore = 10;
        private int movingEnemyDestructionScore = 1;

        public CollisionResolver(SoundEffectContainer soundEffects, ScoreContainer scoreContainer)
        {
            this.soundEffects = soundEffects;
            this.scoreContainer = scoreContainer;
        }

        public void Resolve(GraphicalObjectContainer graphicalObjects)
        {
            this.ResolvePlayerWallCollision(graphicalObjects);

            if (graphicalObjects.Bonuses.Count > 0)
            {
                this.ResolvePlayerBonusCollision(graphicalObjects);
            }

            if (graphicalObjects.Projectiles.Count > 0)
            {
                this.ResolveProjectileWallCollisions(graphicalObjects);

                this.ResolvePlayerProjectileCollisions(graphicalObjects);
            }

            if (graphicalObjects.TronDotsContainers.Count > 0)
            {
                this.ResolveEnemyDotCollisions(graphicalObjects);
                this.ResolvePlayerDotCollisions(graphicalObjects);
            }

            if (graphicalObjects.Enemies.Count > 0)
            {
                this.ResolveEnemyProjectileCollisions(graphicalObjects);
                this.ResolveEnemyEnemyCollisions(graphicalObjects);
                this.ResolvePlayerEnemyCollision(graphicalObjects);
            }
        }

        private void ResolvePlayerWallCollision(GraphicalObjectContainer graphicalObjects)
        {
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            if (spaceShipX < 1)
            {
                graphicalObjects.SpaceShipPlayerOne.Xposition = 1;
            }

            if (spaceShipX > Console.BufferWidth - 2)
            {
                graphicalObjects.SpaceShipPlayerOne.Xposition = Console.BufferWidth - 2;
            }

            if (spaceShipY < 1)
            {
                graphicalObjects.SpaceShipPlayerOne.Yposition = 1;
            }

            if (spaceShipY > Console.BufferHeight - 2)
            {
                graphicalObjects.SpaceShipPlayerOne.Yposition = Console.BufferHeight - 2;
            }
        }

        private void ResolvePlayerBonusCollision(GraphicalObjectContainer graphicalObjects)
        {
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Bonuses.Count; i++)
            {
                byte bonusX = (byte)graphicalObjects.Bonuses[i].Xposition;
                byte bonusY = (byte)graphicalObjects.Bonuses[i].Yposition;

                if (spaceShipX == bonusX && spaceShipY == bonusY)
                {
                    if (graphicalObjects.Bonuses[i].GetType() == typeof(AmmoBonus))
                    {
                        graphicalObjects.SpaceShipPlayerOne.IncreaseShotsAvailable(((AmmoBonus)graphicalObjects.Bonuses[i]).BonusPoints);
                        this.soundEffects.PlayAmmoLoad();
                        graphicalObjects.SpaceShipPlayerOne.PrintStatus();
                    }
                    else if (graphicalObjects.Bonuses[i].GetType() == typeof(HealthBonus))
                    {
                        graphicalObjects.SpaceShipPlayerOne.ChangeHealth(((HealthBonus)graphicalObjects.Bonuses[i]).BonusPoints);
                        this.soundEffects.PlayPowerUp();
                    }
                    else if (graphicalObjects.Bonuses[i].GetType() == typeof(ShieldBonus))
                    {
                        graphicalObjects.SpaceShipPlayerOne.IncreaseShieldTimeAvailable(((ShieldBonus)graphicalObjects.Bonuses[i]).TimeInvincibleInSeconds);
                        this.soundEffects.PlayShieldPowerUp();
                        graphicalObjects.SpaceShipPlayerOne.PrintStatus();
                    }
                    else if (graphicalObjects.Bonuses[i].GetType() == typeof(TronBonus))
                    {
                        /* Making all other containers reach their capacity so that dots from the newest containver 
                         * do not overlap the dots from the previous container */
                        for (int j = 0; j < graphicalObjects.TronDotsContainers.Count; j++)
                        {
                            graphicalObjects.TronDotsContainers[j].ReachCapacity();
                        }

                        graphicalObjects.TronDotsContainers.Add(new TronDotsContainer(graphicalObjects.SpaceShipPlayerOne));
                        this.soundEffects.PlayTronBonus();
                    }

                    graphicalObjects.Bonuses.Remove(graphicalObjects.Bonuses[i]);
                }
            }
        }

        private void ResolveProjectileWallCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.Projectiles.Count; i++)
            {
                byte projectileX = (byte)graphicalObjects.Projectiles[i].Xposition;
                byte projectileY = (byte)graphicalObjects.Projectiles[i].Yposition;

                if (projectileX > Console.BufferWidth - 1 ||
                    projectileX < 1 ||
                    projectileY > Console.BufferHeight - 1 ||
                    projectileY < 1)
                {
                    graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]);
                }
            }
        }

        private void ResolvePlayerProjectileCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.Projectiles.Count; i++)
            {
                if (graphicalObjects.Projectiles[i].Type == ProjectileType.Enemy)
                {
                    byte projectileX = (byte)graphicalObjects.Projectiles[i].Xposition;
                    byte projectileY = (byte)graphicalObjects.Projectiles[i].Yposition;
                    byte projectileFutureX = graphicalObjects.Projectiles[i].XfuturePosition;
                    byte projectileFutureY = graphicalObjects.Projectiles[i].YfuturePosition;

                    if ((projectileX == (byte)graphicalObjects.SpaceShipPlayerOne.Xposition &&
                        projectileY == (byte)graphicalObjects.SpaceShipPlayerOne.Yposition) ||
                        (projectileFutureX == (byte)graphicalObjects.SpaceShipPlayerOne.Xposition &&
                        projectileFutureY == (byte)graphicalObjects.SpaceShipPlayerOne.Yposition))
                    {
                        if (graphicalObjects.SpaceShipPlayerOne.ShieldTimeAvailable == 0)
                        {
                            graphicalObjects.SpaceShipPlayerOne.ChangeHealth(Projectile.Damage);
                            this.soundEffects.PlayHit();
                        }
                        else
                        {
                            this.soundEffects.PlayDullHit();
                        }

                        graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]);
                    }
                }
            }
        }

        private void ResolveEnemyProjectileCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.Projectiles.Count; i++)
            {
                if (graphicalObjects.Projectiles[i].Type == ProjectileType.Player)
                {
                    byte projectileX = (byte)graphicalObjects.Projectiles[i].Xposition;
                    byte projectileY = (byte)graphicalObjects.Projectiles[i].Yposition;
                    byte projectileFutureX = graphicalObjects.Projectiles[i].XfuturePosition;
                    byte projectileFutureY = graphicalObjects.Projectiles[i].YfuturePosition;

                    for (int j = 0; j < graphicalObjects.Enemies.Count; j++)
                    {
                        byte enemyX = (byte)graphicalObjects.Enemies[j].Xposition;
                        byte enemyY = (byte)graphicalObjects.Enemies[j].Yposition;

                        if ((projectileX == enemyX && projectileY == enemyY) ||
                            (projectileFutureX == enemyX && projectileFutureY == enemyY))
                        {
                            this.soundEffects.PlayHit();

                            if (graphicalObjects.Enemies[j].GetType() == typeof(MovingEnemy))
                            {
                                ((MovingEnemy)graphicalObjects.Enemies[j]).ReduceHealth(Projectile.Damage);

                                if (((MovingEnemy)graphicalObjects.Enemies[j]).HealthPoints == 0)
                                {
                                    graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[j]);
                                    this.scoreContainer.Score += this.movingEnemyDestructionScore;
                                }
                            }
                            else if (graphicalObjects.Enemies[j].GetType() == typeof(StationaryEnemy))
                            {
                                ((StationaryEnemy)graphicalObjects.Enemies[j]).ReduceHealth(Projectile.Damage);

                                if (((StationaryEnemy)graphicalObjects.Enemies[j]).HealthPoints == 0)
                                {
                                    graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[j]);
                                    this.scoreContainer.Score += this.stationaryEnemyDestructionScore;
                                }
                            }

                            if (graphicalObjects.Projectiles.Count - 1 > i)
                            {
                                graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]);
                            }
                        }
                    }
                }
            }
        }

        private void ResolveEnemyDotCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.TronDotsContainers.Count; i++)
            {
                for (int j = 0; j < graphicalObjects.TronDotsContainers[i].Dots.Count; j++)
                {
                    byte dotX = (byte)graphicalObjects.TronDotsContainers[i].Dots[j].Xposition;
                    byte dotY = (byte)graphicalObjects.TronDotsContainers[i].Dots[j].Yposition;

                    for (int k = 0; k < graphicalObjects.Enemies.Count; k++)
                    {
                        // Skipping stationary enemies because they cannot collide with dots
                        if (graphicalObjects.Enemies[k].GetType() == typeof(StationaryEnemy))
                        {
                            continue;
                        }

                        byte enemyX = (byte)graphicalObjects.Enemies[k].Xposition;
                        byte enemyY = (byte)graphicalObjects.Enemies[k].Yposition;

                        if (dotX == enemyX && dotY == enemyY)
                        {
                            this.soundEffects.PlayExplosion();
                            graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[k]);
                            this.scoreContainer.Score++;
                        }
                    }
                }
            }
        }

        private void ResolvePlayerDotCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.TronDotsContainers.Count; i++)
            {
                for (int j = 0; j < graphicalObjects.TronDotsContainers[i].Dots.Count; j++)
                {
                    byte dotX = (byte)graphicalObjects.TronDotsContainers[i].Dots[j].Xposition;
                    byte dotY = (byte)graphicalObjects.TronDotsContainers[i].Dots[j].Yposition;

                    if (dotX == (byte)graphicalObjects.SpaceShipPlayerOne.Xposition &&
                        dotY == (byte)graphicalObjects.SpaceShipPlayerOne.Yposition)
                    {
                        graphicalObjects.SpaceShipPlayerOne.HealthPoints = 0;
                    }
                }
            }
        }

        private void ResolvePlayerEnemyCollision(GraphicalObjectContainer graphicalObjects)
        {
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Enemies.Count; i++)
            {
                byte enemyX = (byte)graphicalObjects.Enemies[i].Xposition;
                byte enemyY = (byte)graphicalObjects.Enemies[i].Yposition;

                if (spaceShipX == enemyX && spaceShipY == enemyY)
                {
                    if (graphicalObjects.SpaceShipPlayerOne.ShieldTimeAvailable > 0)
                    {
                        if (graphicalObjects.Enemies[i].GetType() == typeof(MovingEnemy))
                        {
                            this.scoreContainer.Score++;
                        }
                        else if (graphicalObjects.Enemies[i].GetType() == typeof(StationaryEnemy))
                        {
                            this.scoreContainer.Score += this.stationaryEnemyDestructionScore;
                        }

                        this.soundEffects.PlayExplosion();
                        graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[i]);
                    }
                    else
                    {
                        if (graphicalObjects.Enemies[i].GetType() == typeof(MovingEnemy))
                        {
                            this.soundEffects.PlayExplosion();
                            graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[i]);
                            this.scoreContainer.Score += this.movingEnemyDestructionScore;
                            graphicalObjects.SpaceShipPlayerOne.ChangeHealth(Projectile.Damage * 2);
                        }
                        else if (graphicalObjects.Enemies[i].GetType() == typeof(StationaryEnemy))
                        {
                            graphicalObjects.SpaceShipPlayerOne.HealthPoints = 0;
                        }
                    }
                }
            }
        }

        private void ResolveEnemyEnemyCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.Enemies.Count; i++)
            {
                // Collisions with static enemies is to do nothing
                if (graphicalObjects.Enemies[i].GetType() == typeof(StationaryEnemy))
                {
                    continue;
                }

                byte currentEnemyX = (byte)graphicalObjects.Enemies[i].Xposition;
                byte currentEnemyY = (byte)graphicalObjects.Enemies[i].Yposition;

                for (int j = 0; j < graphicalObjects.Enemies.Count; j++)
                {
                    // Collisions with static enemies is to do nothing
                    if (graphicalObjects.Enemies[j].GetType() == typeof(StationaryEnemy))
                    {
                        continue;
                    }

                    if (graphicalObjects.Enemies[i] != graphicalObjects.Enemies[j])
                    {
                        byte otherEnemyX = (byte)graphicalObjects.Enemies[j].Xposition;
                        byte otherEnemyY = (byte)graphicalObjects.Enemies[j].Yposition;

                        if (currentEnemyX == otherEnemyX && currentEnemyY == otherEnemyY)
                        {
                            graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[j]);
                            graphicalObjects.Enemies[i].Color = ConsoleColor.Red;
                            this.soundEffects.PlayExplosion();
                            ((MovingEnemy)graphicalObjects.Enemies[i]).HealthPoints += 100;
                        }
                    }
                }
            }
        }
    }
}
