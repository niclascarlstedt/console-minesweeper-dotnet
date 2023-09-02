namespace Minesweeper;

public class InputParser
{
    public static int[] Parse(string? input)
    {
        char Whitespace = ' ';
        int[] empty = new int[0];
        int[] result = new int[2];

        if (input == null || input.Length != 3) {
            return empty;
        }

        string[] parsed = input.Trim().Split(Whitespace);

        int x;
        bool successX = int.TryParse(parsed[0], out x);

        int y;
        bool successY = int.TryParse(parsed[1], out y);

        if (!(successX && successY))
        {
            return empty;
        }

        result[0] = x;
        result[1] = y;
        return result;
    }
}
