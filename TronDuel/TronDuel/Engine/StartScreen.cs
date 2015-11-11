namespace TronDuel.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
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
            title = this.ObtainTitle();
        }

        public void Run()
        {
            messageBlinkTimer.Start();

            DisplayTitle();

            DisplayInstructions();

            DisplayStartGameMessageUntilEnterIsPressed();
        }

        private void DisplayStartGameMessageUntilEnterIsPressed()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                if (messageBlinkTimer.ElapsedMilliseconds > 1000)
                {
                    if (!messageDisplayed)
                    {
                        Console.SetCursorPosition(25, 25);
                        Console.Write(message);
                    }
                    else
                    {
                        Console.SetCursorPosition(25, 25);
                        Console.Write(new string(' ', message.Length));
                    }

                    messageDisplayed = !messageDisplayed;

                    messageBlinkTimer.Restart();

                    ReadAndProcessCommands();

                    if (enterPressed)
                    {
                        Console.Clear();
                        break;
                    }
                }
            }
        }

        private void DisplayInstructions()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(8, 29);
            Console.WriteLine(instructions);
        }

        private void DisplayTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var line in title)
            {
                Console.SetCursorPosition(7, Console.CursorTop + 1);
                Console.Write(line);
            }
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

        private void ReadAndProcessCommands()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                // Start game
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    enterPressed = true;
                }
            }
        }
    }
}
