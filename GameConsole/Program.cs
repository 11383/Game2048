﻿using System;
using GameLib;

namespace GameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(3, 2, 32);

            while (game.IsPlaying)
            {
                Console.Clear();
                Console.WriteLine($"Score: {game.Score} \tHighscore: {game.Highscore}");

                printArray(game.GameBoard);
                Console.WriteLine($"Press W,S,A,D to move.\nPress C to start new game");

                if (game.CanUndo())
                {
                    Console.WriteLine("Press K to undo");
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        game.MoveTop();
                        break;

                    case ConsoleKey.S:
                        game.MoveBottom();
                        break;

                    case ConsoleKey.A:
                        game.MoveLeft();
                        break;

                    case ConsoleKey.D:
                        game.MoveRight();
                        break;
                    case ConsoleKey.C:
                        game.Restart();
                        break;
                    case ConsoleKey.K:
                        game.Undo();
                        break;
                    case ConsoleKey.Escape:
                        game.End();
                        break;
                }
            }

            Console.Write($"Game over\nScore {game.Score}");
            printArray(game.GameBoard);
        }

        static void printArray<T>(T[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("| {0} |\t", arr[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
