using Habit_Tracking_Console_App;

Console.WriteLine("Welcome back!");
string? userInput = string.Empty;

do {
    // Wait for user input
    userInput = Console.ReadLine();

    // Execute the users input
} while (CommandHandler.ExecuteCommand(userInput));