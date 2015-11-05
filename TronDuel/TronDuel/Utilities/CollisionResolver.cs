namespace TronDuel.Utilities
{
    using System;
    using Enumerations;
    using TronDuel.GraphicalObjects;
    using TronDuel.MovingObjects.GraphicalObjects;

    public class CollisionResolver
    {
        private SoundEffectContainer soundEffects;

        public CollisionResolver(SoundEffectContainer soundEffects)
        {
            this.soundEffects = soundEffects;
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
            }

            //ResolvePlayerEnemyCollision(graphicalObjects); // TODO: Implement
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
            //throw new System.NotImplementedException(); // TODO: Implement
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
                double projectileX = graphicalObjects.Projectiles[i].Xposition;
                double projectileY = graphicalObjects.Projectiles[i].Yposition;

                if (projectileX > Console.BufferWidth - 2 ||
                    projectileX < 1 ||
                    projectileY > Console.BufferHeight - 2 ||
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

                    // TODO: Refactor and improve accuracy

                    if (projectileX == (byte)graphicalObjects.SpaceShipPlayerOne.Xposition &&
                        projectileY == (byte)graphicalObjects.SpaceShipPlayerOne.Yposition)
                    {
                        graphicalObjects.SpaceShipPlayerOne.ChangeHealth(Projectile.Damage);
                        soundEffects.PlayHit();
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

                    // TODO: Refactor and improve accuracy

                    for (int j = 0; j < graphicalObjects.Enemies.Count; j++)
                    {
                        byte enemyX = (byte) graphicalObjects.Enemies[j].Xposition;
                        byte enemyY = (byte) graphicalObjects.Enemies[j].Yposition;

                        if (projectileX == enemyX &&
                            projectileY == enemyY)
                        {
                            graphicalObjects.Enemies[j].ReduceHealth(Projectile.Damage);
                            graphicalObjects.Projectiles.Remove(graphicalObjects.Projectiles[i]); // TODO: Fix system out of range exception here
                            soundEffects.PlayHit();
                            if (graphicalObjects.Enemies[j].HealthPoints == 0)
                            {
                                graphicalObjects.Enemies.Remove(graphicalObjects.Enemies[j]);
                            }
                        }
                    }
                }
            }
        }

        private void ResolvePlayerEnemyCollision(GraphicalObjectContainer graphicalObjects)
        {
            //throw new System.NotImplementedException(); // TODO: Implement
        }
    }
}
