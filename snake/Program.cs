﻿namespace Application
{
    class Program
    {
        public static void Main()
        {
            Snake snake = new Snake();
            Apple apple = new Apple();
            apple.draw();
            drawBottobLine();

            while (true)
            {
                snake.move(apple);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey();
                    if ((cki.Key == ConsoleKey.Enter) || (cki.Key == ConsoleKey.Escape))
                    {
                        gameOver();
                    }
                    else
                    {
                        snake.setVectorByKey(cki.Key);
                    }
                }
                snake.drawScore();
            }

        }

        public static void gameOver()
        {
            int maxX = Console.WindowWidth;
            int maxY = Console.WindowHeight;

            Console.Clear();
            Console.SetCursorPosition(maxX / 2 - 4, maxY / 2);
            Console.WriteLine("GAME OVER");

            for (int i = 0; i < maxY / 2 - 3; i++)
            {
                Console.WriteLine();
            }

            System.Environment.Exit(0);
        }

        public static void drawBottobLine()
        {
            int maxX = Console.WindowWidth;
            int maxY = Console.WindowHeight;
            int bottomY = maxY - 2;

            for (int x = 0; x < maxX; x++)
            {
                Console.SetCursorPosition(x, bottomY);
                Console.WriteLine("-");
            }
        }
    }

    class Apple
    {
        public Apple()
        {
            rand = new Random();
            position = new Position(0, 0);
        }
        private Random rand;
        public Position position { get; set; }
        
        public void draw()
        {
            int maxX = Console.WindowWidth;
            int maxY = Console.WindowHeight - 3;

            position.x = rand.Next(maxX);
            position.y = rand.Next(maxY);

            Console.SetCursorPosition(position.x, position.y);
            Console.WriteLine("O");
        }
    }

    enum Vector
    {
        up,
        down,
        left,
        right,
    }

    class Position
    {
        public Position(int xParam, int yParam)
        {
            x = xParam;
            y = yParam;
        }
        public int x { get; set; }
        public int y { get; set; }
    }

    class Snake
    {
        public Snake()
        {
            vector = Vector.right;
            score = 1;

            positions = new List<Position>();
            positions.Add(new Position(0, 0));
        }

        public Vector vector { get; set; }
        private List<Position> positions;
        private int score;

        public void drawScore()
        {
            int maxX = Console.WindowWidth;
            int maxY = Console.WindowHeight;

            Console.SetCursorPosition(maxX - 3, maxY - 2);
            Console.WriteLine(score);
        }

        public void setVectorByKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.RightArrow:
                    vector = Vector.right;
                    break;

                case ConsoleKey.LeftArrow:
                    vector = Vector.left;
                    break;

                case ConsoleKey.UpArrow:
                    vector = Vector.up;
                    break;

                case ConsoleKey.DownArrow:
                    vector = Vector.down;
                    break;
            }
        }

        public void move(Apple apple)
        {
            Position oldHeadPosition = positions.Last();
            int headX = oldHeadPosition.x;
            int headY = oldHeadPosition.y;
            bool hasEaten = false;

            switch (vector)
            {
                case Vector.up:
                    positions.Add(new Position(headX, headY - 1));
                    break;

                case Vector.down:
                    positions.Add(new Position(headX, headY + 1));
                    break;

                case Vector.left:
                    positions.Add(new Position(headX - 1, headY));
                    break;

                case Vector.right:
                    positions.Add(new Position(headX + 1, headY));
                    break;
            }

            // Draw new position of head
            Position newHeadPosition = positions.Last();
            bool correctPosition = checkNewPositionForBorders(newHeadPosition);
            if (!correctPosition)
            {
                Program.gameOver();
            }
            correctPosition = chechForCrossSelfBody(newHeadPosition);
            if (!correctPosition)
            {
                Program.gameOver();
            }
            Console.SetCursorPosition(newHeadPosition.x, newHeadPosition.y);
            Console.WriteLine("X");

            if ((newHeadPosition.x == apple.position.x) && (newHeadPosition.y == apple.position.y))
            {
                hasEaten = true;
                score += 1;
                apple.draw();
            }

            //Clear old position of tail and delete tail
            Position tailPosition = positions[0];
            Console.SetCursorPosition(tailPosition.x, tailPosition.y);
            Console.WriteLine(" ");
            if (!hasEaten)
            {
                positions.RemoveAt(0);
            }
            
            Thread.Sleep(100);
        }

        private bool chechForCrossSelfBody(Position newPosition)
        {
            // If all ok, function returns true
            // But if newPosition cross over snake's body, functin returns false

            foreach(Position p in positions.GetRange(0, positions.Count - 2))
            {
                if ((newPosition.x == p.x) && (newPosition.y == p.y))
                {
                    return false;
                }
            }

            return true;
        }

        private bool checkNewPositionForBorders(Position newPosition)
        {
            // Function returns true if all ok
            // And returns false if new position beyond borders

            int maxX = Console.WindowWidth;
            int maxY = Console.WindowHeight - 2;

            if (newPosition.x >= maxX)
            {
                return false;
            }
            if (newPosition.x < 0)
            {
                return false;
            }
            if (newPosition.y < 0)
            {
                return false;
            }
            if (newPosition.y > maxY - 1)
            {
                return false;
            }

            return true;
        }
    }
}
