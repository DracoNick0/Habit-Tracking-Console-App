using Habit_Tracking_Console_App.Commander;
using Habit_Tracking_Console_App.PrintHelpers;
using Habit_Tracking_Console_App.Storage;
using Habit_Tracking_Console_App.Threads;

// Start threads.
ConsoleResizeMonitor monitor = new ConsoleResizeMonitor();
TimeDependentVarUpdater updater = new TimeDependentVarUpdater();

DynamicStorageManager dynamicStorageManager = new DynamicStorageManager();

CLIHelper.Msg("Hello World!");

// Run the main application.
CommandHandler commandHandler = new CommandHandler(dynamicStorageManager);
commandHandler.Run();

// Terminate process.
CLIHelper.Msg("See you again!");

