Console.WriteLine("Welcome back!");


bool notExit = true;
string? userInput = string.Empty;
while (notExit)
{

    userInput = Console.ReadLine();
    if (userInput != null)
    {
        userInput = userInput.ToLower();

        switch (userInput)
        {
            case "help":
                Console.WriteLine("Commands:");
                Console.WriteLine(" - help: displays a list of commands for the user to input");
                break;
            default:
                Console.Error.WriteLine("That command was not valid, try again!");
                break;
        }
    }
}