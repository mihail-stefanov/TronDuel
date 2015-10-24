namespace TronDuel.Utilities
{
    using System;
    using TronDuel.GraphicalObjects;

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
                ResolvePlayerHeartCollisions(graphicalObjects, soundEffects);
            }

            if (graphicalObjects.Shields.Count > 0)
            {
                ResolvePlayerShieldCollisions(graphicalObjects);
            }

            if (graphicalObjects.Projectiles.Count > 0)
            {
                ResolveProjectileWallCollisions(graphicalObjects);
            }
            //ResolvePlayerProjectileCollisions(graphicalObjects); // TODO: Implement

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

        private void ResolvePlayerHeartCollisions(GraphicalObjectContainer graphicalObjects, SoundEffectContainer soundEffects)
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

        private void ResolvePlayerShieldCollisions(GraphicalObjectContainer graphicalObjects)
        {
            //throw new System.NotImplementedException(); // TODO: Implement
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
            //throw new System.NotImplementedException(); // TODO: Implement
        }

        private void ResolvePlayerEnemyCollision(GraphicalObjectContainer graphicalObjects)
        {
            //throw new System.NotImplementedException(); // TODO: Implement
        }
    }
}
