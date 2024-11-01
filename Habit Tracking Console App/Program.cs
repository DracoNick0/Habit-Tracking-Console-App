using Habit_Tracking_Console_App.Backend;

CommandHandler commandHandler = new CommandHandler();
string? userInput = string.Empty;
Console.WriteLine(" Welcome back!");

do
{
    Console.WriteLine(" <Enter \"help\" to print a list of commands.>");
    Console.Write(" > ");

    // Wait for user input
    userInput = Console.ReadLine();

    // Execute the users input
} while (commandHandler.ExecuteCommand(userInput));