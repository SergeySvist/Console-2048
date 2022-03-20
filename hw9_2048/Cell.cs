namespace Game2048;

struct Point
{
    public int X;
    public int Y;
    
    public Point(int x=0, int y=0)
    {
        X = x;
        Y = y;
    }
}

class Cell
{
    public ConsoleColor Color { get; set; }
    public int Number { get; set; }

    public Point FirstPoint { get; set; }
    public bool IsSummed { get; set; }

    private static int h;
    private static int w;
    public static int H { get { return h; } }
    public static int W { get { return w; } }

    private char sym;

    public Cell(Point FirstPoint, int number = 2)
    {
        Number = number;
        sym = '█';
        Color = ConsoleColor.Yellow;
        IsSummed = false;
        this.FirstPoint = FirstPoint;
        h = 3;
        w = 5;
    }

    public Cell Copy(Cell b)
    {
        return new Cell(b.FirstPoint, b.Number);
    }
    public void Print()
    {
        Console.CursorVisible = false;
        Console.BackgroundColor = Color;
        Console.ForegroundColor = Color;
        for (int i = 0; i < H; i++)
        {
            for(int j = 0; j < W; j++)
            {
                Console.SetCursorPosition(FirstPoint.X+j, FirstPoint.Y+i);
                Console.WriteLine(sym);
            }
        }
        Console.ForegroundColor = ConsoleColor.Black;
        Console.SetCursorPosition((FirstPoint.X + W / 2)-1, FirstPoint.Y + H / 2);
        Console.WriteLine(Number);
        Console.ResetColor();
        Console.SetCursorPosition(FirstPoint.X + 10, FirstPoint.Y + 10);

    }

    public void Clear()
    {
        sym = ' ';
        Color = ConsoleColor.Black;
        Print();
        sym = '█';
        Color = ConsoleColor.Yellow;
    }
}

