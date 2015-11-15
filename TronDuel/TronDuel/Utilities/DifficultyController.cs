namespace TronDuel.Utilities
{
    using TronDuel.GraphicalObjects;

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
            if (scoreContainer.Score >= easyScore && scoreContainer.Score < mediumScore)
            {
                objectGenerator.MovingEnemyNumberLimit = 7;
                objectGenerator.StationaryEnemyNumberLimit = 4;
                graphicalObjects.SpaceShipPlayerOne.SpeedX = 0.55;
                graphicalObjects.SpaceShipPlayerOne.SpeedY = 0.35;
            }
            else if (scoreContainer.Score >= mediumScore && scoreContainer.Score < hardScore)
            {
                objectGenerator.MovingEnemyNumberLimit = 10;
                objectGenerator.StationaryEnemyNumberLimit = 5;
                graphicalObjects.SpaceShipPlayerOne.SpeedX = 0.57;
                graphicalObjects.SpaceShipPlayerOne.SpeedY = 0.37;

                for (int i = 0; i < graphicalObjects.MovingEnemies.Count; i++)
                {
                    graphicalObjects.MovingEnemies[i].SpeedX = 0.11;
                    graphicalObjects.MovingEnemies[i].SpeedY = 0.05;
                }
            }
            else if (scoreContainer.Score > hardScore)
            {
                objectGenerator.MovingEnemyNumberLimit = 20;
                objectGenerator.StationaryEnemyNumberLimit = 7;
                graphicalObjects.SpaceShipPlayerOne.SpeedX = 0.6;
                graphicalObjects.SpaceShipPlayerOne.SpeedY = 0.4;

                for (int i = 0; i < graphicalObjects.MovingEnemies.Count; i++)
                {
                    graphicalObjects.MovingEnemies[i].SpeedX = 0.12;
                    graphicalObjects.MovingEnemies[i].SpeedY = 0.06;
                }
            }
        }
    }
}
