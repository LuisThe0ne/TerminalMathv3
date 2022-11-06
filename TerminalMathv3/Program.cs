using TerminalMathv3;
using static System.Formats.Asn1.AsnWriter;

Random rd = new Random();
int difficulty = 1;
List<Player> players = new List<Player>();

Console.WriteLine("Hello! \nWelcome to TerminalMath! \nPlease enter a Player count:");

int playerCount = ConvertToIntSafe(Console.ReadLine());

for(int i = 1; i <= playerCount; i++)
{
    Console.WriteLine($"Please enter Player {i - 1} Name:");
    string name = Console.ReadLine();
    Player player = new Player(){
        Id = i - 1,
        Name = name,
        Score = 0
    };
    players.Add(player);
}

difficulty = DropDownMenu(" -) Easy", " -) Medium", " -) Hard");
Console.Clear();
Exercise(difficulty, 0);

void Exercise(int difficulty, int player)
{
    switch (difficulty)
    {
        case 1:
            int random_1 = rd.Next(1, 20);
            int random_2 = rd.Next(1, 20);
            Console.WriteLine($"Calculate: {random_1} + {random_2}");
            int result_easy = random_1 + random_2;

            int input_result = ConvertToIntSafe(Console.ReadLine());

            ConsoleOutput(result_easy, input_result, player);
            break;

        case 2:
            int random_3 = rd.Next(1, 20);
            int random_4 = rd.Next(1, 20);
            Console.WriteLine($"Calculate: {random_3} * {random_4}");
            int result_medium = random_3 * random_4;

            int input_result_medium = ConvertToIntSafe(Console.ReadLine());

            ConsoleOutput(result_medium, input_result_medium, player);
            break;

        case 3:
            int random_5 = rd.Next(1, 200);
            int random_6 = rd.Next(2, 5);

            while ((random_5 % random_6) != 0)
            {
                random_5 = rd.Next(1, 200);
                random_6 = rd.Next(2, 5);
            }

            Console.WriteLine($"Calculate: {random_5} / {random_6}");
            int result_hard = random_5 / random_6;
            int input_result_hard = ConvertToIntSafe(Console.ReadLine());

            ConsoleOutput(result_hard, input_result_hard, player);
            break;
    }
}

void ConsoleOutput(int input_result, int result, int player)
{
    if (input_result == result)
    {
        Console.WriteLine("Correct!");
        Console.Clear();
        players[player].Score = players[player].Score + 1;
        Exercise(difficulty, player);
    }
    else
    {
        if(player != players.Count() - 1)
        {
            Console.Clear();
            Console.WriteLine($"Wrong, Player {player++}, your turn!");
            Exercise(difficulty, player++);
        }
        else
        {
            ViewScoreboard();
        }
    }
}

void ViewScoreboard()
{
    players.Sort((x, y) => y.Score.CompareTo(x.Score));
    Console.WriteLine("The Game is over, Scoreboard:");
    foreach (var player in players)
    {
        Console.WriteLine($"Player {player.Id} ({player.Name}): {player.Score}");
    }
}

int DropDownMenu(params string[] options)
{
    int currentSelection = 0;
    ConsoleKey key;

    Console.CursorVisible = false;

    do
    {
        Console.Clear();
        Console.WriteLine("Choose a difficulty between 1-3!");

        for (int i = 0; i < options.Length; i++)
        {
            Console.SetCursorPosition(i % 1, 1 + i);

            if (i == currentSelection)
                Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.Write(options[i]);

            Console.ResetColor();
        }

        key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                {
                    if (currentSelection >= 1)
                        currentSelection -= 1;
                    break;
                }
            case ConsoleKey.DownArrow:
                {
                    if (currentSelection + 1 < options.Length)
                        currentSelection += 1;
                    break;
                }
        }
    } while (key != ConsoleKey.Enter);

    Console.CursorVisible = true;
    Console.SetCursorPosition(0, 0);

    return currentSelection + 1;
}

int ConvertToIntSafe(string input)
{
    try
    {
        return Convert.ToInt32(input);
    }
    catch(Exception e)
    {
        return 0;
    }
}

void PrettyScoreboard(string[] items, string title)
{
    int maxLength = 0;

    foreach (string item in items)
    {
        if (item.Length > maxLength)
        {
            maxLength = item.Length;
        }
    }


    for (int i = 0; i < lines; i++)
    {
        lines_string = lines_string + "═";
    }

    int highScore = GetHighScore();

    Write_Color("╔" + lines_string + "╗", ConsoleColor.DarkCyan);
    Console.SetCursorPosition(Console.WindowWidth - length, Console.WindowHeight - 4);
    Write_Color($"║ Score: {score}     ║", ConsoleColor.DarkCyan);
    Console.SetCursorPosition(Console.WindowWidth - length, Console.WindowHeight - 3);
    Write_Color($"║ Highscore: {highScore} ║", ConsoleColor.DarkCyan);
    Console.SetCursorPosition(Console.WindowWidth - length, Console.WindowHeight - 2);
    Write_Color("╚" + lines_string + "╝", ConsoleColor.DarkCyan);

    Console.SetCursorPosition(cursor_Left, cursor_Top);
}