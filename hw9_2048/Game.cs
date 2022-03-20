namespace Game2048;

class Game
{
    public Point FirstPoint { get; set; }

    public int H { get; set; }
    public int W { get; set; }

    public Cell[,] cells;

    private Random r;

    public Game(Point FirstPoint)
    {
        this.FirstPoint = FirstPoint;
        cells = new Cell[4, 4];
        r = new Random();

        for (int i = 1; i < cells.GetUpperBound(0)+2; i++)
        {
            for (int j = 1; j < cells.GetUpperBound(1)+2; j++)
            {
                cells[i-1, j-1] = new Cell(new Point((FirstPoint.X + (Cell.W + 1) * j) - Cell.W, (FirstPoint.Y + (Cell.H + 1) * i) - Cell.H), 0);
            }
        }

        H = 4 * (Cell.H + 1);
        W = 4 * (Cell.W + 1);
        RandomCell();
        RandomCell();
    }
    public void ResetAllSumma(Cell[,] arr)
    {
        for(int i=0; i< arr.GetUpperBound(0) + 1; i++)
        {
            for(int j = 0; j < arr.GetUpperBound(1) + 1; j++)
            {
                arr[i, j].IsSummed = false;
            }
        }
    }

    public void MoveUp(Cell[,] arr)
    {
        for (int g = 0; g < arr.GetUpperBound(0); g++)
        {
            for (int i = 1; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    if(arr[i, j].IsSummed == false && arr[i - 1, j].IsSummed == false && arr[i, j].Number !=0&& arr[i - 1, j].Number !=0&& arr[i - 1, j].Number == arr[i, j].Number)
                    {
                        arr[i - 1, j].Number += arr[i, j].Number;
                        arr[i, j].Number = 0;
                        arr[i - 1, j].IsSummed = true;
                    }
                    if (arr[i - 1, j].Number == 0 || arr[i, j].Number == 0)
                    {
                        arr[i - 1, j].Number += arr[i, j].Number;
                        arr[i, j].Number = 0;
                    }
                    
                }
            }
        }

        ResetAllSumma(arr);
    }

    public void MoveDown(Cell[,] arr)
    {
        for (int g = 0; g < arr.GetUpperBound(0); g++)
        {
            for (int i = arr.GetUpperBound(0); i > 0; i--)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    if (arr[i-1, j].IsSummed == false && arr[i, j].IsSummed == false && arr[i, j].Number != 0 && arr[i - 1, j].Number != 0 && arr[i - 1, j].Number == arr[i, j].Number)
                    {
                        arr[i, j].Number += arr[i - 1, j].Number;
                        arr[i - 1, j].Number = 0;
                        arr[i, j].IsSummed = true;
                    }
                    if (arr[i - 1, j].Number == 0 || arr[i, j].Number == 0)
                    {
                        arr[i, j].Number += arr[i - 1, j].Number;
                        arr[i - 1, j].Number = 0;
                    }
                }
            }
        }

        ResetAllSumma(arr);
    }

    public void MoveLeft(Cell[,] arr)
    {
        for (int g = 0; g < arr.GetUpperBound(0); g++)
        {
            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 1; j < arr.GetUpperBound(1)+1; j++)
                {
                    if (arr[i, j].IsSummed == false && arr[i, j-1].IsSummed == false && arr[i, j].Number != 0 && arr[i , j-1].Number != 0 && arr[i, j-1].Number == arr[i, j].Number)
                    {
                        arr[i, j - 1].Number += arr[i, j].Number;
                        arr[i, j].Number = 0;
                        arr[i, j - 1].IsSummed = true;
                        arr[i, j].IsSummed = true;
                    }
                    if (arr[i, j - 1].Number == 0 || arr[i, j].Number == 0 )
                    {
                        arr[i, j - 1].Number += arr[i, j].Number;
                        arr[i, j].Number = 0;
                    }
                }
            }
        }

        ResetAllSumma(arr);
    }

    public void MoveRight(Cell[,] arr)
    {
        for (int g = 0; g < arr.GetUpperBound(0); g++)
        {
            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = arr.GetUpperBound(1); j > 0; j--)
                {
                    if (arr[i, j-1].IsSummed == false && arr[i, j].IsSummed == false && arr[i, j].Number != 0 && arr[i, j - 1].Number != 0 && arr[i, j - 1].Number == arr[i, j].Number)
                    {
                        arr[i, j].Number += arr[i, j - 1].Number;
                        arr[i, j - 1].Number = 0;
                        arr[i, j].IsSummed = true;
                        arr[i, j - 1].IsSummed = true;
                    }
                    if (arr[i, j - 1].Number == 0 || arr[i, j].Number == 0)
                    {
                        arr[i, j].Number += arr[i, j - 1].Number;
                        arr[i, j - 1].Number = 0;
                    }
                }
            }
        }

        ResetAllSumma(arr);
    }

    private void CellCpy(Cell[,] a, Cell[,] b)
    {
        for (int i = 0; i < b.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < b.GetUpperBound(1) + 1; j++)
            {
                a[i, j] = new Cell(new Point(b[i, j].FirstPoint.X, b[i, j].FirstPoint.Y), b[i, j].Number);
            }
        }
    }

    private bool CellEquals(Cell[,] a, Cell[,] b)
    {
        bool c = true;
        for (int i = 0; i < b.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < b.GetUpperBound(1) + 1; j++)
            {
                if (a[i,j].Number != b[i,j].Number)
                    c = false;
            }
        }
        return c;
    }

    public bool CheckLose()
    {
        Cell[,] tmp = new Cell[cells.GetUpperBound(0)+1, cells.GetUpperBound(1)+1];
        int i = 0;
        CellCpy(tmp, cells);
        MoveUp(tmp);
        if (CellEquals(tmp,cells))
            i++;
        else
            CellCpy(tmp, cells);

        MoveDown(tmp);
        if (CellEquals(tmp, cells))
            i++;
        else
            CellCpy(tmp, cells);

        MoveLeft(tmp);
        if (CellEquals(tmp, cells))
            i++;
        else
            CellCpy(tmp, cells);

        MoveRight(tmp);
        if (CellEquals(tmp, cells))
            i++;

        if (i == 4)
            return true;
        return false;
    }

    public void RandomCell()
    {
        int tmp1 = r.Next(1, cells.GetUpperBound(0) + 2);
        int tmp2 = r.Next(1, cells.GetUpperBound(1) + 2);
        int i = 0;
        while (cells[tmp1 - 1, tmp2 - 1].Number != 0 )
        {
            if (i > (cells.GetUpperBound(0) * cells.GetUpperBound(1)) * 2)
                break;
            tmp1 = r.Next(1, cells.GetUpperBound(0) + 2);
            tmp2 = r.Next(1, cells.GetUpperBound(1) + 2);
            i++;
        }
        if (i < (cells.GetUpperBound(0) * cells.GetUpperBound(1)) * 2)
            cells[tmp1 - 1, tmp2 - 1].Number = r.Next(0, 100) > 20 ? 2 : 4; 
    }
    
    public void Print()
    {
        Console.CursorVisible = false;
        for (int i = 0; i < H; i++)
        {
            Console.SetCursorPosition(FirstPoint.X,FirstPoint.Y+i);
            Console.WriteLine("|");
            for (int j = 0; j < W+1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X+j, FirstPoint.Y);
                Console.WriteLine("-");
            }
        }

        for (int i = 1; i < H+1; i++)
        {
            Console.SetCursorPosition(FirstPoint.X+W, FirstPoint.Y + i);
            Console.WriteLine("|");
            for (int j = 0; j < W+1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X + j, FirstPoint.Y+H);
                Console.WriteLine("-");
            }
        }

        for (int i = 0; i < cells.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < cells.GetUpperBound(1) + 1; j++)
            {
                if (cells[i, j].Number != 0)
                    cells[i, j].Print();
                else
                    cells[i, j].Clear();
            }
        }
        Console.SetCursorPosition(W + 10, H + 10);
    }

    public void Clear()
    {
        Console.CursorVisible = false;
        for (int i = 0; i < H; i++)
        {
            Console.SetCursorPosition(FirstPoint.X, FirstPoint.Y + i);
            Console.WriteLine(" ");
            for (int j = 0; j < W + 1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X + j, FirstPoint.Y);
                Console.WriteLine(" ");
            }
        }

        for (int i = 1; i < H + 1; i++)
        {
            Console.SetCursorPosition(FirstPoint.X + W, FirstPoint.Y + i);
            Console.WriteLine(" ");
            for (int j = 0; j < W + 1; j++)
            {
                Console.SetCursorPosition(FirstPoint.X + j, FirstPoint.Y + H);
                Console.WriteLine(" ");
            }
        }

        for (int i = 0; i < cells.GetUpperBound(0) + 1; i++)
        {
            for (int j = 0; j < cells.GetUpperBound(1) + 1; j++)
            {
                cells[i, j].Clear();
            }
        }
        Console.SetCursorPosition(W + 10, H + 10);
    }
}
