﻿using Habit_Tracking_Console_App.Backend;

namespace Habit_Tracking_Console_App.Interface
{
    class CommandHandler
    {
        HabitManager habitManager;
        TaskManager taskManager;

        public CommandHandler()
        {
            habitManager = new HabitManager();
            taskManager = new TaskManager();
        }

        public bool ExecuteCommand(string? userInput)
        {
            if (userInput != null)
            {
                // Change user input to be digestable by the program
                userInput = userInput.ToLower();
                string[] inputArgs = userInput.Split(' ');

                Console.Clear();
                switch (inputArgs[0])
                {
                    case "help":
                        HelpCommand();
                        break;
                    case "create":
                        switch (inputArgs[1])
                        {
                            case "habit":
                                habitManager.CreateHabit();
                                break;
                            case "task":
                                taskManager.AddTask();
                                break;
                            default:
                                InvalidArguument(userInput, inputArgs, 1);
                                break;
                        }
                        break;
                    case "exit":
                        ExitCommand();
                        return false;
                    default:
                        InvalidCommand(userInput);
                        break;
                }
            }

            return true;
        }

        private void HelpCommand()
        {
            CLIHelper.Msg("Commands:");
            CLIHelper.Msg("- help: displays a list of commands for the user to input");
            CLIHelper.Msg("- exit: exit the program");
        }

        private void ExitCommand()
        {
            CLIHelper.Msg("See you again!");
        }

        private void InvalidCommand(string command)
        {
            CLIHelper.Error($"The command \"{command}\" is not valid, try again!");
        }

        private void InvalidArguument(string command, string[] inputArgs, int index)
        {
            CLIHelper.Error($"The argument \"{inputArgs[index]}\" in \"{command}\" is not valid, try again!");
        }
    }
}
