﻿namespace TronDuel.Utilities
{
    using System;
    using TronDuel.Enumerations;
    using TronDuel.GraphicalObjects.Containers;
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

            if (graphicalObjects.Hearts.Count > 0)
            {
                this.ResolvePlayerHeartCollisions(graphicalObjects);
            }

            if (graphicalObjects.Shields.Count > 0)
            {
                this.ResolvePlayerShieldCollisions(graphicalObjects);
            }

            if (graphicalObjects.Ammo.Count > 0)
            {
                this.ResolvePlayerAmmoCollisions(graphicalObjects);
            }

            if (graphicalObjects.TronBonuses.Count > 0)
            {
                this.ResolvePlayerTronBonusCollisions(graphicalObjects);
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

            if (graphicalObjects.MovingEnemies.Count > 0)
            {
                this.ResolveMovingEnemyProjectileCollisions(graphicalObjects); // TODO: Fix repetition through polymorphism
                this.ResolveMovingEnemyEnemyCollisions(graphicalObjects);
                this.ResolvePlayerMovingEnemyCollision(graphicalObjects);
            }

            if (graphicalObjects.StationaryEnemies.Count > 0)
            {
                this.ResolveStationaryEnemyProjectileCollisions(graphicalObjects);
                this.ResolvePlayerStationaryEnemyCollision(graphicalObjects);
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

        private void ResolvePlayerTronBonusCollisions(GraphicalObjectContainer graphicalObjects)
        {
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.TronBonuses.Count; i++)
            {
                byte tronBonusX = (byte)graphicalObjects.TronBonuses[i].Xposition;
                byte tronBonusY = (byte)graphicalObjects.TronBonuses[i].Yposition;

                if (spaceShipX == tronBonusX && spaceShipY == tronBonusY)
                {
                    /* Making all other containers reach their capacity so that dots from the newest containver 
                     * do not overlap the dots from the previous container */
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
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Ammo.Count; i++)
            {
                byte ammoX = (byte)graphicalObjects.Ammo[i].Xposition;
                byte ammoY = (byte)graphicalObjects.Ammo[i].Yposition;

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
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Shields.Count; i++)
            {
                byte shieldX = (byte)graphicalObjects.Shields[i].Xposition;
                int shieldY = (byte)graphicalObjects.Shields[i].Yposition;

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
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Hearts.Count; i++)
            {
                byte heartX = (byte)graphicalObjects.Hearts[i].Xposition;
                byte heartY = (byte)graphicalObjects.Hearts[i].Yposition;

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
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.MovingEnemies.Count; i++)
            {
                byte enemyX = (byte)graphicalObjects.MovingEnemies[i].Xposition;
                byte enemyY = (byte)graphicalObjects.MovingEnemies[i].Yposition;

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
            byte spaceShipX = (byte)graphicalObjects.SpaceShipPlayerOne.Xposition;
            byte spaceShipY = (byte)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.StationaryEnemies.Count; i++)
            {
                byte enemyX = (byte)graphicalObjects.StationaryEnemies[i].Xposition;
                byte enemyY = (byte)graphicalObjects.StationaryEnemies[i].Yposition;

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
                byte currentEnemyX = (byte)graphicalObjects.MovingEnemies[i].Xposition;
                byte currentEnemyY = (byte)graphicalObjects.MovingEnemies[i].Yposition;

                for (int j = 0; j < graphicalObjects.MovingEnemies.Count; j++)
                {
                    if (graphicalObjects.MovingEnemies[i] != graphicalObjects.MovingEnemies[j])
                    {
                        byte otherEnemyX = (byte)graphicalObjects.MovingEnemies[j].Xposition;
                        byte otherEnemyY = (byte)graphicalObjects.MovingEnemies[j].Yposition;

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
