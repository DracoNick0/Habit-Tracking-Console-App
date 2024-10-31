using Habit_Tracking_Console_App;

CommandHandler commandHandler = new CommandHandler();
string? userInput = string.Empty;
Console.WriteLine("Welcome back!");

do
{
    // Wait for user input
    userInput = Console.ReadLine();

    // Execute the users input
} while (commandHandler.ExecuteCommand(userInput));