using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            Console.WindowHeight = 30;
            Console.WindowWidth = 50;
            Console.BufferHeight = 30;
            Console.BufferWidth = 50;

            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int score = 0;
            int speed = 100;
            int difficultyLevel = 1;
            Random randomNumberGenerator = new Random();
            List<int> snakeXCoordinates = new List<int>();
            List<int> snakeYCoordinates = new List<int>();
            string direction = "right";

            Console.CursorVisible = false;

            do
            {
                if (difficultyLevel == 1)
                {
                    speed = 100;
                }
                else if (difficultyLevel == 2)
                {
                    speed = 75;
                }
                else if (difficultyLevel == 3)
                {
                    speed = 50;
                }

                Console.Clear();
                x1 = randomNumberGenerator.Next(0, Console.WindowWidth);
                y1 = randomNumberGenerator.Next(0, Console.WindowHeight);

                Console.SetCursorPosition(x1, y1);
                Console.Write("X");

                snakeXCoordinates.Add(x2);
                snakeYCoordinates.Add(y2);

                Console.SetCursorPosition(x2, y2);
                Console.Write("o");

                if (x2 == x1 && y2 == y1)
                {
                    score++;
                    x2 = snakeXCoordinates[snakeXCoordinates.Count - 1];
                    y2 = snakeYCoordinates[snakeYCoordinates.Count - 1];
                    Console.SetCursorPosition(x2, y2);
                    Console.Write("o");
                }

                if (snakeXCoordinates.Count > score)
                {
                    Console.SetCursorPosition(snakeXCoordinates[0], snakeYCoordinates[0]);
                    Console.Write(" ");
                    snakeXCoordinates.RemoveAt(0);
                    snakeYCoordinates.RemoveAt(0);
                }

                if (direction == "right")
                {
                    x2++;
                }
                else if (direction == "left")
                {
                    x2--;
                }
                else if (direction == "up")
                {
                    y2--;
                }
                else if (direction == "down")
                {
                    y2++;
                }

                if (x2 < 0)
                {
                    x2 = Console.WindowWidth - 1;
                }

                if (x2 >= Console.WindowWidth)
                {
                    x2 = 0;
                }

                if (y2 < 0)
                {
                    y2 = Console.WindowHeight - 1;
                }



                if (y2 >= Console.WindowHeight)
                {
                    y2 = 0;
                }

                for (int i = 0; i < snakeXCoordinates.Count; i++)
                {
                    if (x2 == snakeXCoordinates[i] && y2 == snakeYCoordinates[i])
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Game Over");
                        Console.WriteLine("Your score is " + score);
                        Console.WriteLine("Press Enter to exit");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

                Console.Title = "Snake Game | Difficulty Level: " + difficultyLevel + " | Score: " + score;

                Thread.Sleep(speed);
            } while (true);
        }
    }
}


