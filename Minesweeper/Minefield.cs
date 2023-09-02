namespace Minesweeper;

public class Minefield
{
    private bool[,] _bombLocations;

    private Dictionary<string,bool> _visible;

    private int [,] _adjecentCountOverlay;

    private int _size;

    private int _initialCovered;

    public Minefield(int size)
    {
        _size = size;
        _initialCovered = size*size;
        _bombLocations = new bool[size, size];
        _visible = new Dictionary<string, bool>();
        _adjecentCountOverlay = new int[size, size];
    }

    public void SetBomb(int x, int y)
    {
        _bombLocations[x, y] = true;
        _initialCovered--;
        IncreaseAdjecentWeights(x,y);
    }

    internal void IncreaseAdjecentWeights(int x, int y)
    {
        // Increase itself
        PerformSafeIncrease(x,y);

        // Left of current
        PerformSafeIncrease(x-1,y+1);
        PerformSafeIncrease(x-1,y);
        PerformSafeIncrease(x-1,y-1);

        // Above current
        PerformSafeIncrease(x,y-1);

        // Right of current
        PerformSafeIncrease(x+1,y-1);
        PerformSafeIncrease(x+1,y);
        PerformSafeIncrease(x+1,y+1);

        // Below current
        PerformSafeIncrease(x,y+1);
    }

    internal void PerformSafeIncrease(int x, int y)
    {
        if (!InsideBoard(x,y))
        {
            return;
        }

        _adjecentCountOverlay[x,y] = _adjecentCountOverlay[x,y] + 1;
    }

    public bool HasBomb(int x, int y)
    {
        return _bombLocations[x, y];
    }

    public int GetSize()
    {
        return _size;
    }

    public bool IsVisible(int x, int y)
    {
       bool visible = false;
       _visible.TryGetValue($"{x}_{y}", out visible);
       return visible;
    }

    internal bool InsideBoard(int x, int y)
    {
        if (x >= 0 && x < _size && y >= 0 && y < _size) {
            return true;
        }

        return false;
    }

    internal bool MakeVisible(int x, int y)
    {
        if (!InsideBoard(x,y)) {
           return false;
        }
        _visible[$"{x}_{y}"] = true;
        return true;
    }


    private void RecursiveUncoverAdjescent(int x, int y)
    {
        if (!InsideBoard(x,y) || IsVisible(x,y))
        {
            return;
        }

        MakeVisible(x,y);

        int adjescentBombs = _adjecentCountOverlay[x,y];
        if (adjescentBombs == 0)
        {
            // Right of current
            RecursiveUncoverAdjescent(x+1,y+1);
            RecursiveUncoverAdjescent(x+1,y);
            RecursiveUncoverAdjescent(x+1,y-1);

            // Left of current
            RecursiveUncoverAdjescent(x-1,y+1);
            RecursiveUncoverAdjescent(x-1,y);
            RecursiveUncoverAdjescent(x-1,y-1);

            // Above and below current in matrix
            RecursiveUncoverAdjescent(x,y-1);
            RecursiveUncoverAdjescent(x,y+1);
        }
    }

    public bool AttemptClearBomb(int x, int y)
    {
        RecursiveUncoverAdjescent(x,y);

        if (HasBomb(x,y)) {
            return false;
        }
        return true;
    }

    public string[] GetDisplayRows(bool showBoard = false)
    {
        Stack<string> result = new Stack<string>();
        for (int y = _size-1; y > -1; y--) {
            string rowText = "";
            for (int x = 0; x < _size; x++) {
                bool hide = !showBoard && !IsVisible(x,y);
                if (hide)
                {
                    rowText += '?';
                    continue;
                }

                int foundCount = _adjecentCountOverlay[x,y];
                if (HasBomb(x,y))
                {
                    rowText += 'X';
                }
                else if (foundCount == 0)
                {
                    rowText += ' ';
                }
                else
                {
                    rowText += foundCount;
                }
            }
            result.Push(rowText);
        }
        return result.Reverse().ToArray();
    }

    internal bool AllDone()
    {
        var leftToUncover = _initialCovered - _visible.Count;
        Console.WriteLine($"Left to uncover: {leftToUncover}");
        return leftToUncover == 0;
    }
}
