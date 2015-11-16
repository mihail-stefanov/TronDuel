namespace TronDuel.Utilities
{
    using System;
    using Enumerations;
    using TronDuel.GraphicalObjects;
    using TronDuel.MovingObjects.GraphicalObjects;

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
            ResolvePlayerWallCollision(graphicalObjects);

            if (graphicalObjects.Hearts.Count > 0)
            {
                ResolvePlayerHeartCollisions(graphicalObjects);
            }

            if (graphicalObjects.Shields.Count > 0)
            {
                ResolvePlayerShieldCollisions(graphicalObjects);
            }

            if (graphicalObjects.Ammo.Count > 0)
            {
                ResolvePlayerAmmoCollisions(graphicalObjects);
            }

            if (graphicalObjects.TronBonuses.Count > 0)
            {
                ResolvePlayerTronBonusCollisions(graphicalObjects);
            }

            if (graphicalObjects.Projectiles.Count > 0)
            {
                ResolveProjectileWallCollisions(graphicalObjects);

                ResolvePlayerProjectileCollisions(graphicalObjects);
            }

            if (graphicalObjects.TronDotsContainers.Count > 0)
            {
                ResolveEnemyDotCollisions(graphicalObjects);
                ResolvePlayerDotCollisions(graphicalObjects);
            }

            if (graphicalObjects.MovingEnemies.Count > 0)
            {
                ResolveMovingEnemyProjectileCollisions(graphicalObjects); // TODO: Fix repetition through polymorphism
                ResolveMovingEnemyEnemyCollisions(graphicalObjects);
                ResolvePlayerMovingEnemyCollision(graphicalObjects);
            }

            if (graphicalObjects.StationaryEnemies.Count > 0)
            {
                ResolveStationaryEnemyProjectileCollisions(graphicalObjects);
                ResolvePlayerStationaryEnemyCollision(graphicalObjects);
            }


        }

        private void ResolvePlayerWallCollision(GraphicalObjectContainer graphicalObjects)
        {
            double spaceShipX = graphicalObjects.SpaceShipPlayerOne.Xposition;
            double spaceShipY = graphicalObjects.SpaceShipPlayerOne.Yposition;

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

        private void ResolvePlayerTronBonusCollisions(GraphicalObjectContainer graphicalObjects)
        {
            int spaceShipX = (int)graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.TronBonuses.Count; i++)
            {
                int tronBonusX = (int)graphicalObjects.TronBonuses[i].Xposition;
                int tronBonusY = (int)graphicalObjects.TronBonuses[i].Yposition;

                if (spaceShipX == tronBonusX && spaceShipY == tronBonusY)
                {
                    // Making all other containers reach their capacity so that dots from the newest containver
                    // do not overlap the dots from the previous container
                    for (int j = 0; j < graphicalObjects.TronDotsContainers.Count; j++)
                    {
                        graphicalObjects.TronDotsContainers[j].ReachCapacity();
                    }
                    graphicalObjects.TronDotsContainers.Add(new TronDotsContainer(graphicalObjects.SpaceShipPlayerOne));
                    soundEffects.PlayTronBonus();
                    graphicalObjects.TronBonuses.Remove(graphicalObjects.TronBonuses[i]);
                }
            }
        }

        private void ResolvePlayerAmmoCollisions(GraphicalObjectContainer graphicalObjects)
        {
            int spaceShipX = (int)graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Ammo.Count; i++)
            {
                int ammoX = (int)graphicalObjects.Ammo[i].Xposition;
                int ammoY = (int)graphicalObjects.Ammo[i].Yposition;

                if (spaceShipX == ammoX && spaceShipY == ammoY)
                {
                    graphicalObjects.SpaceShipPlayerOne.IncreaseShotsAvailable(graphicalObjects.Ammo[i].BonusPoints);
                    soundEffects.PlayAmmoLoad();
                    graphicalObjects.SpaceShipPlayerOne.PrintStatus(); // TODO: To refactor
                    graphicalObjects.Ammo.Remove(graphicalObjects.Ammo[i]);
                }
            }
        }

        private void ResolvePlayerShieldCollisions(GraphicalObjectContainer graphicalObjects)
        {
            int spaceShipX = (int)graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Shields.Count; i++)
            {
                int shieldX = (int)graphicalObjects.Shields[i].Xposition;
                int shieldY = (int)graphicalObjects.Shields[i].Yposition;

                if (spaceShipX == shieldX && spaceShipY == shieldY)
                {
                    graphicalObjects.SpaceShipPlayerOne.IncreaseShieldTimeAvailable(graphicalObjects.Shields[i].TimeInvincibleInSeconds);
                    soundEffects.PlayShieldPowerUp();
                    graphicalObjects.SpaceShipPlayerOne.PrintStatus(); // TODO: To refactor
                    graphicalObjects.Shields.Remove(graphicalObjects.Shields[i]);
                }
            }
        }

        private void ResolvePlayerHeartCollisions(GraphicalObjectContainer graphicalObjects)
        {
            int spaceShipX = (int)graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Hearts.Count; i++)
            {
                int heartX = (int)graphicalObjects.Hearts[i].Xposition;
                int heartY = (int)graphicalObjects.Hearts[i].Yposition;

                if (spaceShipX == heartX && spaceShipY == heartY)
                {
                    graphicalObjects.SpaceShipPlayerOne.ChangeHealth(graphicalObjects.Hearts[i].BonusPoints);
                    soundEffects.PlayPowerUp();
                    graphicalObjects.Hearts.Remove(graphicalObjects.Hearts[i]);
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

                    // TODO: Refactor and improve accuracy

                    if ((projectileX == (byte)graphicalObjects.SpaceShipPlayerOne.Xposition &&
                        projectileY == (byte)graphicalObjects.SpaceShipPlayerOne.Yposition) ||
                        (projectileFutureX == (byte)graphicalObjects.SpaceShipPlayerOne.Xposition &&
                        projectileFutureY == (byte)graphicalObjects.SpaceShipPlayerOne.Yposition))
                    {
                        if (graphicalObjects.SpaceShipPlayerOne.ShieldTimeAvailable == 0)
                        {
                            graphicalObjects.SpaceShipPlayerOne.ChangeHealth(Projectile.Damage); // TODO: Consider changing the method to DecreaseHealth
                            soundEffects.PlayHit();
                        }
                        else
                        {
                            soundEffects.PlayDullHit();
                        }

                        graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]);
                    }
                }
            }
        }

        private void ResolveMovingEnemyProjectileCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.Projectiles.Count; i++)
            {
                if (graphicalObjects.Projectiles[i].Type == ProjectileType.Player)
                {
                    byte projectileX = (byte)graphicalObjects.Projectiles[i].Xposition;
                    byte projectileY = (byte)graphicalObjects.Projectiles[i].Yposition;
                    byte projectileFutureX = graphicalObjects.Projectiles[i].XfuturePosition;
                    byte projectileFutureY = graphicalObjects.Projectiles[i].YfuturePosition;

                    // TODO: Refactor and improve accuracy

                    for (int j = 0; j < graphicalObjects.MovingEnemies.Count; j++)
                    {
                        byte enemyX = (byte)graphicalObjects.MovingEnemies[j].Xposition;
                        byte enemyY = (byte)graphicalObjects.MovingEnemies[j].Yposition;

                        if ((projectileX == enemyX && projectileY == enemyY) ||
                            (projectileFutureX == enemyX && projectileFutureY == enemyY))
                        {
                            graphicalObjects.MovingEnemies[j].ReduceHealth(Projectile.Damage);
                            graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]); // TODO: Fix argument out of range exception here
                            soundEffects.PlayHit();
                            if (graphicalObjects.MovingEnemies[j].HealthPoints == 0)
                            {
                                graphicalObjects.MovingEnemies.Remove(graphicalObjects.MovingEnemies[j]);
                                scoreContainer.Score += movingEnemyDestructionScore;
                            }
                        }
                    }
                }
            }
        }

        private void ResolveStationaryEnemyProjectileCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.Projectiles.Count; i++)
            {
                if (graphicalObjects.Projectiles[i].Type == ProjectileType.Player)
                {
                    byte projectileX = (byte)graphicalObjects.Projectiles[i].Xposition;
                    byte projectileY = (byte)graphicalObjects.Projectiles[i].Yposition;
                    byte projectileFutureX = graphicalObjects.Projectiles[i].XfuturePosition;
                    byte projectileFutureY = graphicalObjects.Projectiles[i].YfuturePosition;

                    // TODO: Refactor and improve accuracy

                    for (int j = 0; j < graphicalObjects.StationaryEnemies.Count; j++)
                    {
                        byte enemyX = (byte)graphicalObjects.StationaryEnemies[j].Xposition;
                        byte enemyY = (byte)graphicalObjects.StationaryEnemies[j].Yposition;

                        if ((projectileX == enemyX && projectileY == enemyY) ||
                            (projectileFutureX == enemyX && projectileFutureY == enemyY))
                        {
                            graphicalObjects.StationaryEnemies[j].ReduceHealth(Projectile.Damage);
                            graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]);
                            soundEffects.PlayHit();
                            if (graphicalObjects.StationaryEnemies[j].HealthPoints == 0)
                            {
                                graphicalObjects.StationaryEnemies.Remove(graphicalObjects.StationaryEnemies[j]);
                                scoreContainer.Score += stationaryEnemyDestructionScore;
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

                    for (int k = 0; k < graphicalObjects.MovingEnemies.Count; k++)
                    {
                        byte enemyX = (byte)graphicalObjects.MovingEnemies[k].Xposition;
                        byte enemyY = (byte)graphicalObjects.MovingEnemies[k].Yposition;

                        if (dotX == enemyX && dotY == enemyY)
                        {
                            soundEffects.PlayExplosion();
                            graphicalObjects.MovingEnemies.Remove(graphicalObjects.MovingEnemies[k]);
                            scoreContainer.Score++;
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

        private void ResolvePlayerMovingEnemyCollision(GraphicalObjectContainer graphicalObjects)
        {
            int spaceShipX = (int)graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.MovingEnemies.Count; i++)
            {
                int enemyX = (int)graphicalObjects.MovingEnemies[i].Xposition;
                int enemyY = (int)graphicalObjects.MovingEnemies[i].Yposition;

                if (spaceShipX == enemyX && spaceShipY == enemyY)
                {
                    if (graphicalObjects.SpaceShipPlayerOne.ShieldTimeAvailable > 0)
                    {
                        soundEffects.PlayExplosion();
                        graphicalObjects.MovingEnemies.Remove(graphicalObjects.MovingEnemies[i]);
                        scoreContainer.Score++;
                    }
                    else
                    {
                        soundEffects.PlayExplosion();
                        graphicalObjects.MovingEnemies.Remove(graphicalObjects.MovingEnemies[i]);
                        scoreContainer.Score += movingEnemyDestructionScore;
                        graphicalObjects.SpaceShipPlayerOne.ChangeHealth(Projectile.Damage * 2);
                    }
                }
            }
        }

        private void ResolvePlayerStationaryEnemyCollision(GraphicalObjectContainer graphicalObjects)
        {
            int spaceShipX = (int)graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.StationaryEnemies.Count; i++)
            {
                int enemyX = (int)graphicalObjects.StationaryEnemies[i].Xposition;
                int enemyY = (int)graphicalObjects.StationaryEnemies[i].Yposition;

                if (spaceShipX == enemyX && spaceShipY == enemyY)
                {
                    if (graphicalObjects.SpaceShipPlayerOne.ShieldTimeAvailable > 0)
                    {
                        soundEffects.PlayExplosion();
                        graphicalObjects.StationaryEnemies.Remove(graphicalObjects.StationaryEnemies[i]);
                        scoreContainer.Score++;
                    }
                    else
                    {
                        graphicalObjects.SpaceShipPlayerOne.HealthPoints = 0;
                    }
                }
            }
        }

        private void ResolveMovingEnemyEnemyCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.MovingEnemies.Count; i++)
            {
                int currentEnemyX = (int)graphicalObjects.MovingEnemies[i].Xposition;
                int currentEnemyY = (int)graphicalObjects.MovingEnemies[i].Yposition;

                for (int j = 0; j < graphicalObjects.MovingEnemies.Count; j++)
                {
                    if (graphicalObjects.MovingEnemies[i] != graphicalObjects.MovingEnemies[j])
                    {
                        int otherEnemyX = (int)graphicalObjects.MovingEnemies[j].Xposition;
                        int otherEnemyY = (int)graphicalObjects.MovingEnemies[j].Yposition;

                        if (currentEnemyX == otherEnemyX && currentEnemyY == otherEnemyY)
                        {
                            graphicalObjects.MovingEnemies.Remove(graphicalObjects.MovingEnemies[j]);
                            graphicalObjects.MovingEnemies[i].Color = ConsoleColor.Red;
                            soundEffects.PlayExplosion();
                            graphicalObjects.MovingEnemies[i].HealthPoints += 100;
                        }
                    }
                }
            }
        }
    }
}
