namespace Application
{
    class Program
    {
        public static void Main()
        {
            Snake snake = new Snake();
            Apple apple = new Apple();
            apple.draw();

            while (true)
            {
                snake.move(apple);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey();
                    if ((cki.Key == ConsoleKey.Enter) || (cki.Key == ConsoleKey.Escape))
                    {
                        gameOver();
                        return;
                    }
                    else
                    {
                        snake.setVectorByKey(cki.Key);
                    }
                }
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
            int maxY = Console.WindowHeight;

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

            positions = new List<Position>();
            positions.Add(new Position(0, 0));
        }

        public Vector vector { get; set; }
        private List<Position> positions;

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
            Console.SetCursorPosition(newHeadPosition.x, newHeadPosition.y);
            Console.WriteLine("X");

            if ((newHeadPosition.x == apple.position.x) && (newHeadPosition.y == apple.position.y))
            {
                hasEaten = true;
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
    }
}
