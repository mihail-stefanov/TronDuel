// https://github.com/mihail-stefanov/TronDuel
namespace TronDuel
{
    using System;
    using TronDuel.Engines;
    using TronDuel.Interfaces;
    using TronDuel.Utilities;
    using TronDuel.Utilities.Containers;

    public class TronDuelMain
    {
        private static ScoreContainer scoreContainer = new ScoreContainer();

        public static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 70;
            Console.CursorVisible = false;

            // Infinite loop allows for multiple game sessions
            while (true)
            {
                IEngine startScreen = new StartScreen();
                startScreen.Run();

                IEngine gameEngine = new GameEngine(scoreContainer);
                gameEngine.Run();

                IEngine gameOverScreeen = new GameOverScreen(scoreContainer);
                gameOverScreeen.Run();
            }
        }
    }
}
