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

            IEngine gameEngine = new GameEngine();
            gameEngine.Run();
        }
    }
}
