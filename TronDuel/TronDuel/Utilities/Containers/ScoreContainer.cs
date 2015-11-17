namespace TronDuel.Utilities.Containers
{
    using System;

    public class ScoreContainer
    {
        private int score;

        public ScoreContainer()
        {
            this.Score = 0;
        }

        public int Score
        {
            get 
            { 
                return this.score; 
            }

            set 
            { 
                this.score = value; 
            }
        }

        public void DrawScore()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(55, 0);
            Console.WriteLine("Score: {0}", this.Score);
        }
    }
}
