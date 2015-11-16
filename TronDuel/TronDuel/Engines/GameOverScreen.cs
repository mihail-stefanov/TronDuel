namespace TronDuel.Engines
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using TronDuel.Interfaces;
    using TronDuel.Utilities;

    public class GameOverScreen : IEngine
    {
        private Stopwatch messageBlinkTimer = new Stopwatch();

        private bool messageDisplayed = false;
        private string message = "PRESS ENTER TO RESTART THE GAME";

        private bool enterPressed = false;
        private List<string> gameOverText;
        private Utilities.ScoreContainer scoreContainer;

        public GameOverScreen(ScoreContainer scoreContainer)
        {
            this.gameOverText = this.ObtainTitle();
            this.scoreContainer = scoreContainer;
        }

        public void Run()
        {
            this.messageBlinkTimer.Start();

            Console.Clear();
            Console.SetCursorPosition(0, 10);

            this.DisplayScreenTitle();

            this.DisplayScore();

            this.DisplayMessageUntilEnterIsPressed();

            Console.Clear();
        }

        private List<string> ObtainTitle()
        {
            StreamReader reader = new StreamReader("gameOverText.txt");

            List<string> lines = new List<string>();

            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    lines.Add(line);
                }
            }

            return lines;
        }

        private void DisplayScreenTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var line in this.gameOverText)
            {
                Console.SetCursorPosition(16, Console.CursorTop + 1);
                Console.Write(line);
            }
        }

        private void DisplayScore()
        {
            Console.SetCursorPosition(29, 17);
            Console.Write("YOUR SC0RE: {0}", this.scoreContainer.Score);
        }

        private void DisplayMessageUntilEnterIsPressed()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            while (true)
            {
                if (this.messageBlinkTimer.ElapsedMilliseconds > 1000)
                {
                    if (!this.messageDisplayed)
                    {
                        Console.SetCursorPosition(20, 20);
                        Console.Write(this.message);
                    }
                    else
                    {
                        Console.SetCursorPosition(20, 20);
                        Console.Write(new string(' ', this.message.Length));
                    }

                    this.messageDisplayed = !this.messageDisplayed;

                    this.messageBlinkTimer.Restart();
                }

                this.ReadAndProcessCommands();

                if (this.enterPressed)
                {
                    this.scoreContainer.Score = 0;
                    break;
                }
            }
        }

        private void ReadAndProcessCommands()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                // Restart game
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    this.enterPressed = true;
                }
            }
        }
    }
}
