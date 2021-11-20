for (int i = 0; i < 30; i++)
{
    if (i < 15)
    {
        Console.SetCursorPosition(i, i);
    }
    else
    {
        Console.SetCursorPosition(i, 30 - i);
    }
    
    Console.Write("+");
    Thread.Sleep(100);
}
