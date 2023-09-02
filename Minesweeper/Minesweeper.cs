namespace Minesweeper;

public class Minesweeper
{
    static ConsoleColor SavedForegroundColor;

    static void Main()
    {
        int size = 5;
        var field = new Minefield(size);
        SavedForegroundColor = Console.ForegroundColor;

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);


        //the mine field should look like this now:
        //  01234
        //4|1X1
        //3|11111
        //2|2211X
        //1|XX111
        //0|X31

        // I modified the Layout so that it would look like:
        //y
        //4 1X1
        //3 11111
        //2 2211X
        //1 XX111
        //0 X31
        //  01234 x

        // Game code...

        bool running = true;
        var input = "";

        Console.WriteLine();
        Console.WriteLine(" ===** Welcome to a very simple Console MineSweeper example **===");
        Console.WriteLine();
        Display(field);

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("Enter 'x y' to try uncovering a bomb. (Enter 'q' to quit)");
            Console.Write("> ");

            input = Console.ReadLine();

            if (input == "q")
            {
                running = false;
            }
            else
            {
                Console.WriteLine($"Processing input | {input} | :");

                int[] parsed = InputParser.Parse(input);

                if (parsed.Length == 0) {
                    Console.WriteLine("Incorrect input. Try again.");
                    continue;
                }

                int x = parsed[0];
                int y = parsed[1];

                if (!field.InsideBoard(x,y))
                {
                    Console.WriteLine("Incorrect input. Coordinates outside board. Try again.");
                    continue;
                }

                bool successfulUncover = field.AttemptClearBomb(x,y);
                Display(field);

                if (!successfulUncover)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*** BOOOOOOM!!! ***");
                    Console.WriteLine($"There was a bomb on {x} {y}");
                    Console.ForegroundColor = SavedForegroundColor;
                    Console.WriteLine();
                    running = false;
                }
                else if (field.AllDone())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*** CONGRATULATIONS, you cleared the board. ***");
                    Console.ForegroundColor = SavedForegroundColor;
                    Console.WriteLine();
                    Display(field, true);

                    Console.WriteLine();
                    running = false;
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine("Thank you for playing. Bye!");
        Console.WriteLine();
    }

    static void Display(Minefield mineField, bool showBoard = false)
    {
        var rows = mineField.GetDisplayRows(showBoard);
        Console.WriteLine("y");
        int displayRowIndex = rows.Length-1;
        foreach (var row in rows) {
            Console.Write(displayRowIndex-- + " ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(row);
            Console.ForegroundColor = SavedForegroundColor;
        }
        Console.WriteLine("  01234 x");
    }
}
