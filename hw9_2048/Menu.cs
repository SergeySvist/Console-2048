namespace Game2048;

class Menu
{
    public Point FirstPoint { get; set; }

    private Game g;
    private string[] punkts;
    private string name;
    public Menu()
    {
        name = "Menu";
        FirstPoint = new Point(0, 0);
        g = new Game(FirstPoint);
        punkts = new string[]
        {
            "1. Новая Игра",
            "2. Продолжить",
            "3. Сохранить",
            "4. Загрузить посл. сохранение",
            "5. Выйти",
        };
    }

    public Menu(Point FirstPoint)
    {
        name = "Menu";
        this.FirstPoint = FirstPoint;
        g = new Game(this.FirstPoint);
        punkts = new string[]
        {
            "1. Новая Игра",
            "2. Продолжить",
            "3. Сохранить",
            "4. Загрузить посл. сохранение",
            "5. Выйти",
        };
    }

    private void Print(int i)
    {
        Console.SetCursorPosition(FirstPoint.X, FirstPoint.Y);
        Console.WriteLine(name);
        name = "Menu";

        int h = punkts.Length + 3;
        int max = punkts[0].Length;
        for (int j = 0; j < punkts.Length; j++)
        {
            if (punkts[j].Length > max)
                max = punkts[j].Length;
        }
        int w = max+3;

        for (int g = 0; g < h; g++)
        {
            Console.SetCursorPosition(FirstPoint.X, (FirstPoint.Y + g)+1);
            Console.WriteLine("|");
            for (int j = 0; j < w+1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X + j, FirstPoint.Y+1);
                Console.WriteLine("-");
            }
        }

        for (int g = 1; g < h+1; g++)
        {
            Console.SetCursorPosition(FirstPoint.X+w, (FirstPoint.Y + g) + 1);
            Console.WriteLine("|");
            for (int j = 0; j < w + 1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X + j, FirstPoint.Y+h+1);
                Console.WriteLine("-");
            }
        }

        for (int j = 0; j < punkts.Length; j++)
        {
            if (j == i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            
            Console.SetCursorPosition(FirstPoint.X+2, (FirstPoint.Y + j)+3);
            Console.WriteLine(punkts[j]);
            Console.ResetColor();
        }
    }

    private void Clear()
    {
        Console.SetCursorPosition(FirstPoint.X, FirstPoint.Y);
        Console.WriteLine("    ");
        int h = punkts.Length + 3;
        int max = punkts[0].Length;
        for (int j = 0; j < punkts.Length; j++)
        {
            if (punkts[j].Length > max)
                max = punkts[j].Length;
        }
        int w = max + 3;

        for (int g = 0; g < h; g++)
        {
            Console.SetCursorPosition(FirstPoint.X, (FirstPoint.Y + g) + 1);
            Console.WriteLine(" ");
            for (int j = 0; j < w + 1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X + j, FirstPoint.Y + 1);
                Console.WriteLine(" ");
            }
        }

        for (int g = 1; g < h + 1; g++)
        {
            Console.SetCursorPosition(FirstPoint.X + w, (FirstPoint.Y + g) + 1);
            Console.WriteLine(" ");
            for (int j = 0; j < w + 1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X + j, FirstPoint.Y + h + 1);
                Console.WriteLine(" ");
            }
        }

        for (int j = 0; j < punkts.Length; j++)
        {
            Console.SetCursorPosition(FirstPoint.X + 2, (FirstPoint.Y + j) + 3);

            for (int i=0; i< punkts[j].Length; i++)
            {
                Console.Write(" ");
            }
        }
    }

    public void Save()
    {
        string[,] tmp = new string[(g.cells.GetUpperBound(0) + 1), (g.cells.GetUpperBound(0) + 1)];
        
        for (int i = 0; i < g.cells.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < g.cells.GetUpperBound(1) + 1; j++)
            {

                tmp[i, j] = g.cells[i, j].Number.ToString();
            }
        }

        File.WriteAllText("LastSave.save", string.Concat(tmp.Cast<string>().Select((s, i) => s + ((i + 1) % tmp.GetLength(1) == 0 ? "\n" : " "))));

    }

    public void Load()
    {
        string tmp = File.ReadAllText("LastSave.save");

        int i = 0, j = 0;
        int[,] res = new int[10, 10];
        foreach (var row in tmp.Split('\n'))
        {
            j = 0;
            foreach (var col in row.Trim().Split(' '))
            {
                try
                {
                    res[i, j] = int.Parse(col.Trim());
                    g.cells[i,j].Number = res[i, j];
                }
                catch { }
                j++;
            }
            i++;
        }
        
    }
    private void Choose(int i)
    {
        Clear();
        if (i == 0)
        {
            g = new Game(this.FirstPoint);
            StartGame();
        }
        else if (i == 1)
        {
            StartGame();
        }
        else if (i == 2)
        {
            Save();
        }
        else if (i == 3)
        {
            Load();
        }
        else if (i == 4)
        {
            System.Environment.Exit(0);
        }
    }

    public void StartMenu()
    {
        int i = 0;
        ConsoleKey key = ConsoleKey.S;
        while (key != ConsoleKey.Escape)
        {
            if (key == ConsoleKey.UpArrow)
            {
                i--;
                if (i < 0)
                    i = 4;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                i++;
                if (i > 4)
                    i = 0;
            }
            g.Clear();
            Print(i);
            key = Console.ReadKey().Key;
            Clear();
            if (key == ConsoleKey.Enter)
                Choose(i);
        }
    }

    public void StartGame()
    {
        g.Print();
        
        ConsoleKey key = Console.ReadKey().Key;
        while (key != ConsoleKey.Escape & g.CheckLose() == false)
        {
            if (key == ConsoleKey.UpArrow)
            {
                g.MoveUp(g.cells);
                g.RandomCell();
            }
            else if (key == ConsoleKey.DownArrow)
            {
                g.MoveDown(g.cells);
                g.RandomCell();
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                g.MoveLeft(g.cells);
                g.RandomCell();
            }
            else if (key == ConsoleKey.RightArrow)
            {
                g.MoveRight(g.cells);
                g.RandomCell();
            }

            g.Print();
            key = Console.ReadKey().Key;
        }
        g.Clear();

        if (g.CheckLose() == true)
            name="ТЫ ПРОИГРАЛ";

    }

}

