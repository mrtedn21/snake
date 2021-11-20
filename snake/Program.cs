int[] detectPosition(int index)
{
    int[] res = new int[2];

    if (index < 15)
    {
        res[0] = index;
        res[1] = index;
        return res;
    }
    else
    {
        res[0] = index;
        res[1] = 30 - index;
        return res;
    }
}

int[] position = new int[2];

for (int i = 0; i < 30; i++)
{
    position = detectPosition(i);

    Console.SetCursorPosition(position[0], position[1]);
    Console.Write("+");
    Thread.Sleep(100);
}
