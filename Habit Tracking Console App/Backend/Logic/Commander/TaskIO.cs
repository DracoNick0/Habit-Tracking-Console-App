using Habit_Tracking_Console_App.Backend.Objects;
using Habit_Tracking_Console_App.Frontend;
using Task_Tracking_Console_App.Backend.Objects;

namespace Habit_Tracking_Console_App.Backend.Logic.Commander
{
    /// <summary>
    /// Contains functions that prompt for interactions with tasks.
    /// </summary>
    class TaskIO
    {
        public TaskIO() { }

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
                    IO.MsgForWindow("+", "+", "+", '-');
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

                    IO.MsgForWindow($"| [{completion}] {task.Name} ", "..|", $"|");
                    IO.MsgForWindow($"| {timeLeft}", "..|", $"({importance} : {isGood}) |");

                    if (displayDescription)
                    {
                        IO.MsgForWindow($"| Desc: {task.Description} ", "..|", "|");
                    }
                }
            }
            else
            {
                IO.MsgForWindow("+", "+", "+", '-');
                IO.MsgForWindow($"| Empty", "...|", "|");
            }

            IO.MsgForWindow("+", "+", "+", '-');
        }

        public string PromptAndGetNewTaskName()
        {
            return IO.PromptForNotEmptyInput("Enter the new task name: ");
        }

        public void PromptAndGetTaskCorrection(ref string name, ref int importance, ref bool isGood, ref string description, ref RecurrenceEnum recurrence, ref int occurrence)
        {
            string? userInput;

            while (true)
            {
                List<string> prompt = new List<string>();
                prompt.Add("(If a detail is incorrect, type it's name to change the property, otherwise press enter.)");
                prompt.Add("Task details:");
                prompt.Add($"- Name: {name}");
                prompt.Add($"- Recurrence: {recurrence.ToString()}");
                prompt.Add($"- Occurrence: {occurrence}");
                prompt.Add($"- Importance: {importance}");
                prompt.Add($"- IsGood: {isGood}");
                prompt.Add($"- Desc: {description}");

                userInput = IO.PromptForNotNullInput(prompt.ToArray()).ToLower();

                switch (userInput)
                {
                    case "name":
                        name = PromptAndGetNewTaskName();
                        break;
                    case "desc":
                        description = PromptAndGetDescription();
                        break;
                    case "isgood":
                        isGood = PromptAndGetIsGood();
                        break;
                    case "importance":
                        importance = PromptAndGetImportance();
                        break;
                    case "recurrence":
                        recurrence = PromptAndGetRecurrence();
                        break;
                    case "occurrence":
                        occurrence = PromptAndGetOccurrence();
                        break;
                    case "":
                        return;
                    default:
                        IO.Error("Input was not valid, try again!");
                        break;
                }
            }
        }

        public DateTime PromptAndGetDueDate()
        {
            return InputManager.GetDateInput("Enter task start/due date (mm/dd/yyyy): ");
        }

        public RecurrenceEnum PromptAndGetRecurrence()
        {
            return InputManager.GetRecurrenceInput("Enter recurrence; none, daily, weekly, monthly, or yearly: ");
        }

        public int PromptAndGetOccurrence()
        {
            int occurrence = 0;
            string occurrencePrompt = "Enter the amount of times you wish to do this task within the time interval: ";
            while (!((occurrence = InputManager.GetIntInput(occurrencePrompt)) > 0))
            {
                IO.Clear();
                IO.Error($"\"{occurrence}\" is not > 0, try again!");
            }

            return occurrence;
        }

        /// <summary>
        /// Prompts the user for the name of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public string PromptAndGetExistingTaskName()
        {
            return IO.PromptForNotEmptyInput("Enter the name of the task: ");
        }

        /// <summary>
        /// Prompts the user for the description of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public string PromptAndGetDescription()
        {
            return IO.PromptForNotEmptyInput("Enter the description of the task: ");
        }

        /// <summary>
        /// Prompts the user for the polarity of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public bool PromptAndGetIsGood()
        {
            string isGoodPrompt = "Is this a good task, enter true or false: ";
            return InputManager.GetBoolInput(isGoodPrompt);
        }

        /// <summary>
        /// Prompts the user for the importance of a task.
        /// </summary>
        /// <returns>User input.</returns>
        public int PromptAndGetImportance()
        {
            int importance;
            string importancePrompt = "If 1 is trivial and 5 is of utmost importance, enter the digit that represents the tasks importance: ";
            while (!((importance = InputManager.GetIntInput(importancePrompt)) >= 1 && importance <= 5))
            {
                IO.Clear();
                IO.Error($"\"{importance}\" is not >= 1 && <= 5, try again!");
            }

            return importance;
        }
    }
}
