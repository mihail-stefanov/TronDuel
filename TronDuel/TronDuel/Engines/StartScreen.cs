namespace TronDuel.Engines
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using TronDuel.Interfaces;

    public class StartScreen : IEngine
    {
        private Stopwatch messageBlinkTimer = new Stopwatch();
        private bool enterPressed = false;

        private bool messageDisplayed = false;
        private string message = "PRESS ENTER TO START";

        private string instructions = "To play the game use the arrow keys and the space bar.";

        private List<string> title;

        public StartScreen()
        {
            this.title = this.ObtainTitle();
        }

        public void Run()
        {
            this.messageBlinkTimer.Start();

            this.DisplayScreenTitle();

            this.DisplayInstructions();

            this.DisplayMessageUntilEnterIsPressed();
        }

        private List<string> ObtainTitle()
        {
            StreamReader reader = new StreamReader("title.txt");

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

            foreach (var line in this.title)
            {
                Console.SetCursorPosition(7, Console.CursorTop + 1);
                Console.Write(line);
            }
        }

        private void DisplayInstructions()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(8, 29);
            Console.WriteLine(this.instructions);
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
                        Console.SetCursorPosition(25, 25);
                        Console.Write(this.message);
                    }
                    else
                    {
                        Console.SetCursorPosition(25, 25);
                        Console.Write(new string(' ', this.message.Length));
                    }

                    this.messageDisplayed = !this.messageDisplayed;

                    this.messageBlinkTimer.Restart();
                }

                this.ReadAndProcessCommands();

                if (this.enterPressed)
                {
                    Console.Clear();
                    break;
                }
            }
        }

        private void ReadAndProcessCommands()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                // Start game
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    this.enterPressed = true;
                }
            }
        }
    }
}
