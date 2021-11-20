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

    public void move()
    {
        Position oldHeadPosition = positions.Last();
        int headX = oldHeadPosition.x;
        int headY = oldHeadPosition.y;

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

        Position newHeadPosition = positions.Last();
        Console.SetCursorPosition(newHeadPosition.x, newHeadPosition.y);
        Console.WriteLine("X");
        Thread.Sleep(100);
    }
}
