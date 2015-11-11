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

            if (graphicalObjects.Projectiles.Count > 0)
            {
                ResolveProjectileWallCollisions(graphicalObjects);

                ResolvePlayerProjectileCollisions(graphicalObjects);
            }

            if (graphicalObjects.Enemies.Count > 0)
            {
                ResolveEnemyProjectileCollisions(graphicalObjects);
                ResolveEnemyEnemyCollisions(graphicalObjects);
            }

            ResolvePlayerEnemyCollision(graphicalObjects);
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
                byte projectileX = (byte) graphicalObjects.Projectiles[i].Xposition;
                byte projectileY = (byte) graphicalObjects.Projectiles[i].Yposition;

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

                    // TODO: Refactor and improve accuracy

                    for (int j = 0; j < graphicalObjects.Enemies.Count; j++)
                    {
                        byte enemyX = (byte) graphicalObjects.Enemies[j].Xposition;
                        byte enemyY = (byte) graphicalObjects.Enemies[j].Yposition;

                        if ((projectileX == enemyX && projectileY == enemyY) ||
                            (projectileFutureX == enemyX && projectileFutureY == enemyY))
                        {
                            graphicalObjects.Enemies[j].ReduceHealth(Projectile.Damage);
                            graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]);
                            soundEffects.PlayHit();
                            if (graphicalObjects.Enemies[j].HealthPoints == 0)
                            {
                                graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[j]);
                                scoreContainer.Score++;
                            }
                        }
                    }
                }
            }
        }

        private void ResolvePlayerEnemyCollision(GraphicalObjectContainer graphicalObjects)
        {
            int spaceShipX = (int)graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;

            for (int i = 0; i < graphicalObjects.Enemies.Count; i++)
            {
                int enemyX = (int)graphicalObjects.Enemies[i].Xposition;
                int enemyY = (int)graphicalObjects.Enemies[i].Yposition;

                if (spaceShipX == enemyX && spaceShipY == enemyY)
                {
                    if (graphicalObjects.SpaceShipPlayerOne.ShieldTimeAvailable > 0)
                    {
                        soundEffects.PlayExplosion();
                        graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[i]);
                        scoreContainer.Score++;
                    }
                    else
                    {
                        graphicalObjects.SpaceShipPlayerOne.HealthPoints = 0;
                    }
                }
            }
        }

        private void ResolveEnemyEnemyCollisions(GraphicalObjectContainer graphicalObjects)
        {
            for (int i = 0; i < graphicalObjects.Enemies.Count; i++)
            {
                int currentEnemyX = (int)graphicalObjects.Enemies[i].Xposition;
                int currentEnemyY = (int)graphicalObjects.Enemies[i].Yposition;

                for (int j = 0; j < graphicalObjects.Enemies.Count; j++)
                {
                    if (graphicalObjects.Enemies[i] != graphicalObjects.Enemies[j])
                    {
                        int otherEnemyX = (int)graphicalObjects.Enemies[j].Xposition;
                        int otherEnemyY = (int)graphicalObjects.Enemies[j].Yposition;

                        if (currentEnemyX == otherEnemyX && currentEnemyY == otherEnemyY)
                        {
                            graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[j]);
                            graphicalObjects.Enemies[i].Color = ConsoleColor.Red;
                            soundEffects.PlayExplosion();
                            graphicalObjects.Enemies[i].HealthPoints += 100;
                        }
                    }
                }
            }
        }
    }
}
