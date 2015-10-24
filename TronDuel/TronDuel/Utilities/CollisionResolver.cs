namespace TronDuel.Utilities
{
    using TronDuel.GraphicalObjects;

    public class CollisionResolver
    {
        public void Resolve(GraphicalObjectContainer graphicalObjects, SoundEffectContainer soundEffects)
        {
            if (graphicalObjects.Hearts.Count > 0)
            {
                ResolvePlayerHeartCollisions(graphicalObjects, soundEffects);
            }

            if (graphicalObjects.Shields.Count > 0)
            {
                ResolvePlayerShieldCollisions(graphicalObjects);
            }

            //ResolvePlayerProjectileCollisions(graphicalObjects); // TODO: Implement

            //ResolvePlayerEnemyCollision(graphicalObjects); // TODO: Implement
        }

        private void ResolvePlayerHeartCollisions(GraphicalObjectContainer graphicalObjects, SoundEffectContainer soundEffects)
        {
            int spaceShipX = (int) graphicalObjects.SpaceShipPlayerOne.Xposition;
            int spaceShipY = (int)graphicalObjects.SpaceShipPlayerOne.Yposition;
            int heartX = (int)graphicalObjects.Hearts[0].Xposition;
            int heartY = (int)graphicalObjects.Hearts[0].Yposition;

            if (spaceShipX == heartX && spaceShipY == heartY)
            {
                graphicalObjects.SpaceShipPlayerOne.ChangeHealth(graphicalObjects.Hearts[0].BonusPoints);
                soundEffects.PlayPowerUp();
                graphicalObjects.Hearts.Remove(graphicalObjects.Hearts[0]);
            }
        }

        private void ResolvePlayerShieldCollisions(GraphicalObjectContainer graphicalObjects)
        {
            //throw new System.NotImplementedException(); // TODO: Implement
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
