﻿namespace TronDuel.Engine
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
            gameOverText = this.ObtainGameOverText();
            this.scoreContainer = scoreContainer;
        }

        private List<string> ObtainGameOverText()
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

        public void Run()
        {
            messageBlinkTimer.Start();

            Console.Clear();
            Console.SetCursorPosition(0, 10);

            DisplayGameOverText();

            DisplayScore();

            DisplayRetartGameMessageUntilEnterIsPressed();

            Console.Clear();
        }

        private void DisplayGameOverText()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var line in gameOverText)
            {
                Console.SetCursorPosition(16, Console.CursorTop + 1);
                Console.Write(line);
            }
        }

        private void DisplayScore()
        {
            Console.SetCursorPosition(29, 17);
            Console.Write("YOUR SC0RE: {0}", scoreContainer.Score); // TODO: Fix repetition
        }

        private void DisplayRetartGameMessageUntilEnterIsPressed()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            while (true)
            {
                if (messageBlinkTimer.ElapsedMilliseconds > 1000)
                {
                    if (!messageDisplayed)
                    {
                        Console.SetCursorPosition(20, 20);
                        Console.Write(message);
                    }
                    else
                    {
                        Console.SetCursorPosition(20, 20);
                        Console.Write(new string(' ', message.Length));
                    }

                    messageDisplayed = !messageDisplayed;

                    messageBlinkTimer.Restart();
                }

                ReadAndProcessCommands();

                if (enterPressed)
                {
                    scoreContainer.Score = 0;
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
                    enterPressed = true; // TODO: Refactor repeating code
                }
            }
        }
    }
}