namespace TronDuel
{
    using System;
    using TronDuel.Engine;
    using TronDuel.Interfaces;

    public class TronDuelMain
    {
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

                IEngine gameEngine = new GameEngine();
                gameEngine.Run();

                IEngine gameOverScreeen = new GameOverScreen();
                gameOverScreeen.Run();
            }
        }
    }
}
