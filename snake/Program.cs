Snake snake = new Snake();

while (true)
{
    snake.move();
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo cki = Console.ReadKey();
        snake.setVectorByKey(cki.Key);
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
    public int x { get; set; }
    public int y { get; set; }
}

class Snake
{
    public Snake()
    {
        vector = Vector.right;

        position = new Position();
        position.x = 0;
        position.y = 0;
    }

    public Vector vector { get; set; }
    private Position position;

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

    public void move()
    {
        switch (vector)
        {
            case Vector.up:
                position.y -= 1;
                break;

            case Vector.down:
                position.y += 1;
                break;

            case Vector.left:
                position.x -= 1;
                break;

            case Vector.right:
                position.x += 1;
                break;
        }
        Console.SetCursorPosition(position.x, position.y);
        Console.WriteLine("X");
        Thread.Sleep(100);
    }
}
