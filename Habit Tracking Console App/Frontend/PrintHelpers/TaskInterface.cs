using Habit_Tracking_Console_App.Backend.Logic;
using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend.PrintHelpers;
using System.Globalization;
using Task_Tracking_Console_App.Backend.Objects;

namespace Task_Tracking_Console_App.Frontend.PrintHelpers
{
    /// <summary>
    /// Contains functions that prompt for interactions with tasks.
    /// </summary>
    class TaskInterface
    {
        public TaskInterface() { }

        /// <summary>
        /// Displays all tasks.
        /// </summary>
        /// <param name="displayIsGood">Determines if the isGood variable is displayed.</param>
        /// <param name="displayDescription">Determines if the description variable is displayed.</param>
        /// <param name="displayImportance">Determines if the importance variable is displayed.</param>
        /// <param name="displayCompletion">Determines if the completion variable is displayed.</param>
        public void DisplayAllTasks(List<TaskObject> tasks, bool displayIsGood = true, bool displayImportance = true, bool displayCompletion = true, bool displayDescription = true, bool displayTimeLeft = true)
        {
            if (tasks.Count > 0)
            {
                string isGood = "", importance = "", completion = " ", timeLeft = "";
                foreach (TaskObject task in tasks)
                {
                    CLIHelper.MsgForWindow("+", "+", "+", '-');
                    if (displayCompletion)
                    {
                        if (task.Completions >= task.Occurrence)
                        {
                            completion = "x";
                        }
                        else
                        {
                            completion = $"{task.Completions}/{task.Occurrence}";
                        }
                    }

                    if (displayIsGood)
                    {
                        if (task.IsGood)
                        {
                            isGood = "Positive";
                        }
                        else
                        {
                            isGood = "Negative";
                        }
                    }

                    if (displayImportance)
                    {
                        importance = task.Importance.ToString();
                    }

                    if (displayTimeLeft)
                    {
                        switch (task.Recurrence)
                        {
                            case RecurrenceEnum.none:
                                // timeLeft = $"{TimeHelper.TimeTillNextDay().Hours.ToString()} hours left";
                                break;
                            case RecurrenceEnum.daily:
                                timeLeft = $"{TimeHelper.TimeTillNextDay().Hours.ToString()} hours left";
                                break;
                            case RecurrenceEnum.weekly:
                                timeLeft = $"{TimeHelper.TimeTillNextWeek().Days.ToString()} days left";
                                break;
                            case RecurrenceEnum.monthly:
                                timeLeft = $"{TimeHelper.TimeTillNextMonth().Days.ToString()} days left";
                                break;
                            case RecurrenceEnum.yearly:
                                timeLeft = $"{TimeHelper.TimeTillNextYear().Days.ToString()} days left";
                                break;
                        }
                    }

                    //CLIHelper.MsgForWindow($"| Task: [{completion}] Task: {task.Name} ", "...|", "|");
                    //CLIHelper.MsgForWindow($"| Importance: {importance} ", "...|", $"({isGood}) |");

                    //CLIHelper.MsgForWindow($"| [{completion}] {task.Name} ", "..|", $"({importance} : {isGood}) |");

                    CLIHelper.MsgForWindow($"| [{completion}] {task.Name} ", "..|", $"|");
                    CLIHelper.MsgForWindow($"| {timeLeft}", "..|", $"({importance} : {isGood}) |");

                    if (displayDescription)
                    {
                        CLIHelper.MsgForWindow($"| Desc: {task.Description} ", "..|", "|");
                    }
                }
            }
            else
            {
                CLIHelper.MsgForWindow("+", "+", "+", '-');
                CLIHelper.MsgForWindow($"| Empty", "...|", "|");
            }

            CLIHelper.MsgForWindow("+", "+", "+", '-');
        }

        /// <summary>
        /// Prompts the user for the name of a task, then returns the corresponding task.
        /// </summary>
        /// <returns>The task object corresponding to the name provided.</returns>
        public string PromptForTask()
        {
            return CLIHelper.PromptForNotEmptyInput("Enter the task name: ");
        }

        /// <summary>
        /// Prints prompts to allow the user to edit the task object.
        /// </summary>
        /// <param name="name">Tasks name.</param>
        /// <param name="importance">Tasks importance.</param>
        /// <param name="isGood">Tasks isGood.</param>
        /// <param name="description">Tasks description</param>
        public void PromptForTaskCorrection(ref string name, ref int importance, ref bool isGood, ref string description, ref string recurrenceAsString, ref int occurrence)
        {
            string? userInput;

            while (true)
            {
                List<string> prompt = new List<string>();
                prompt.Add("(If a detail is incorrect, type it's name to change the property, otherwise press enter.)");
                prompt.Add("Task details:");
                prompt.Add($"- Name: {name}");
                prompt.Add($"- Recurrence: {recurrenceAsString}");
                prompt.Add($"- Occurrence: {occurrence}");
                prompt.Add($"- Importance: {importance}");
                prompt.Add($"- IsGood: {isGood}");
                prompt.Add($"- Desc: {description}");

                userInput = CLIHelper.PromptForNotNullInput(prompt.ToArray()).ToLower();

                switch (userInput)
                {
                    case "name":
                        name = PromptForName();
                        break;
                    case "desc":
                        description = PromptForDescription();
                        break;
                    case "isgood":
                        isGood = PromptForIsGood();
                        break;
                    case "importance":
                        importance = PromptForImportance();
                        break;
                    case "recurrence":
                        recurrenceAsString = PromptForRecurrence();
                        break;
                    case "occurrence":
                        occurrence = PromptForOccurrence();
                        break;
                    case "":
                        return;
                    default:
                        CLIHelper.Error("Input was not valid, try again!");
                        break;
                }
            }
        }

        public string PromptForDueDate()
        {
            return CLIHelper.PromptForNotEmptyInput("Please enter task start/due date (mm/dd/yyyy).");
        }

        /// <summary>
        /// Prompts the user for the recurrence of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public string PromptForRecurrence()
        {
            while (true)
            {
                string intervalPrompt = "Enter recurrence; none, daily, weekly, monthly, or yearly: ";
                string userInput;
                switch (userInput = CLIHelper.PromptForNotEmptyInput(intervalPrompt))
                {
                    case "none":
                    case "daily":
                    case "weekly":
                    case "monthly":
                    case "yearly":
                        return userInput;
                    default:
                        CLIHelper.Clear();
                        CLIHelper.Error("Input was not valid, try again!");
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts the user for the occurrence of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public int PromptForOccurrence()
        {
            int occurence = -1;

            string occurencePrompt = "Enter the amount of times you wish to do this task within the time interval: ";
            while (!((occurence = CLIHelper.PromptForIntInput(occurencePrompt)) > 0))
            {
                CLIHelper.Clear();
                CLIHelper.Error("Input was not valid, try again!");
            }

            return occurence;
        }

        /// <summary>
        /// Prompts the user for the name of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public string PromptForName()
        {
            string namePrompt = "Enter the name of the task: ";
            return CLIHelper.PromptForNotEmptyInput(namePrompt);
        }

        /// <summary>
        /// Prompts the user for the description of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public string PromptForDescription()
        {
            string descriptionPrompt = "Enter the description of the task: ";
            return CLIHelper.PromptForNotEmptyInput(descriptionPrompt);
        }

        /// <summary>
        /// Prompts the user for the polarity of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public bool PromptForIsGood()
        {
            string isGoodPrompt = "Is this a good task, enter true or false: ";
            return CLIHelper.PromptForTrueFalseInput(isGoodPrompt);
        }

        /// <summary>
        /// Prompts the user for the importance of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public int PromptForImportance()
        {
            int importance;

            string importancePrompt = "If 1 is trivial and 5 is of utmost importance, enter the digit that represents the tasks importance: ";
            while (!((importance = CLIHelper.PromptForIntInput(importancePrompt)) >= 1 && importance <= 5))
            {
                CLIHelper.Clear();
                CLIHelper.Error("Input was not valid, try again!");
            }

            return importance;
        }
    }
}
