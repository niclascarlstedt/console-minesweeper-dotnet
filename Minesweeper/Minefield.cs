namespace Minesweeper;

class Minefield
{
    private bool[,] _bombLocations = new bool[5, 5];

    public void SetBomb(int x, int y)
    {
        _bombLocations[x, y] = true;
    }
}
