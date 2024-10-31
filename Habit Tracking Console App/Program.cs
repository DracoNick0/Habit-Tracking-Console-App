Console.WriteLine("Welcome back!");


bool wantsToExit = false;
string? userInput = string.Empty;

while (!wantsToExit)
{
    // Wait for user input
    userInput = Console.ReadLine();

    if (userInput != null)
    {
        // Change user input to be digestable by the program
        userInput = userInput.ToLower();

        Console.Clear();
        switch (userInput)
        {
            case "help":
                Console.WriteLine("Commands:");
                Console.WriteLine(" - help: displays a list of commands for the user to input");
                break;
            case "exit":
                Console.WriteLine("See you again!");
                wantsToExit = true;
                break;
            default:
                Console.Error.WriteLine("The command \"" + userInput + "\" is not valid, try again!");
                break;
        }
    }
}