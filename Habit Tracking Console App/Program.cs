using Habit_Tracking_Console_App.Model;
using Habit_Tracking_Console_App.View;
using Habit_Tracking_Console_App.ViewModel;

// Start window size monitoring.
ConsoleResizeMonitor monitor = new ConsoleResizeMonitor();

CLIHelper.Msg("Hello World!");

// Run the main application.
MainCommandHandler commandHandler = new MainCommandHandler();
commandHandler.Run();

// Terminate process.
CLIHelper.Msg("See you again!");

