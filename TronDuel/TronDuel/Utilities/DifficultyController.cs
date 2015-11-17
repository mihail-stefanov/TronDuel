namespace TronDuel.Utilities
{
    using TronDuel.GraphicalObjects;
    using TronDuel.GraphicalObjects.Enemies;
    using TronDuel.Utilities.Containers;

    public class DifficultyController
    {
        private GraphicalObjectContainer graphicalObjects;
        private ObjectGenerator objectGenerator;
        private ScoreContainer scoreContainer;

        private int easyScore = 10;
        private int mediumScore = 30;
        private int hardScore = 60;

        public DifficultyController(
            GraphicalObjectContainer graphicalObjects, 
            ObjectGenerator objectGenerator, 
            ScoreContainer scoreContainer)
        {
            this.graphicalObjects = graphicalObjects;
            this.objectGenerator = objectGenerator;
            this.scoreContainer = scoreContainer;
        }

        public void UpdateDifficulty()
        {
            if (this.scoreContainer.Score >= this.easyScore && this.scoreContainer.Score < this.mediumScore)
            {
                this.objectGenerator.MovingEnemyNumberLimit = 7;
                this.objectGenerator.StationaryEnemyNumberLimit = 4;
                this.graphicalObjects.SpaceShipPlayerOne.SpeedX = 0.55;
                this.graphicalObjects.SpaceShipPlayerOne.SpeedY = 0.35;
            }
            else if (this.scoreContainer.Score >= this.mediumScore && this.scoreContainer.Score < this.hardScore)
            {
                this.objectGenerator.MovingEnemyNumberLimit = 10;
                this.objectGenerator.StationaryEnemyNumberLimit = 5;
                this.graphicalObjects.SpaceShipPlayerOne.SpeedX = 0.57;
                this.graphicalObjects.SpaceShipPlayerOne.SpeedY = 0.37;

                for (int i = 0; i < this.graphicalObjects.Enemies.Count; i++)
                {
                    if (this.graphicalObjects.Enemies[i].GetType() == typeof(MovingEnemy))
                    {
                        ((MovingEnemy)this.graphicalObjects.Enemies[i]).Xspeed = 0.11;
                        ((MovingEnemy)this.graphicalObjects.Enemies[i]).Yspeed = 0.05;
                    }
                }
            }
            else if (this.scoreContainer.Score > this.hardScore)
            {
                this.objectGenerator.MovingEnemyNumberLimit = 20;
                this.objectGenerator.StationaryEnemyNumberLimit = 7;
                this.graphicalObjects.SpaceShipPlayerOne.SpeedX = 0.6;
                this.graphicalObjects.SpaceShipPlayerOne.SpeedY = 0.4;

                for (int i = 0; i < this.graphicalObjects.Enemies.Count; i++)
                {
                    if (this.graphicalObjects.Enemies[i].GetType() == typeof(MovingEnemy))
                    {
                        ((MovingEnemy)this.graphicalObjects.Enemies[i]).Xspeed = 0.12;
                        ((MovingEnemy)this.graphicalObjects.Enemies[i]).Yspeed = 0.06;
                    }
                }
            }
        }
    }
}
