using Habit_Tracking_Console_App.Model;

namespace Habit_Tracking_Console_App.View
{
    class HabitInterface
    {
        private HabitManager habitManager;

        public HabitInterface()
        {
            habitManager = new HabitManager();
        }

        public void DisplayAllHabits(bool displayIsGood = true, bool displayDescription = true, bool displayImportance = true, bool displayCompletion = true)
        {

            List<HabitObject> habits = habitManager.getHabits();

            if (habits.Count > 0)
            {
                string isGood = "", importance = "", completion = "";
                foreach (HabitObject habit in habits)
                {
                    CLIHelper.MsgForWindow("+", "+", "+", '-');
                    if (displayCompletion)
                    {
                        if (habit.Completed)
                        {
                            completion = "Complete";
                        }
                        else
                        {
                            completion = "Incomplete";
                        }
                    }

                    if (displayIsGood)
                    {
                        if (habit.IsGood)
                        {
                            isGood = "Positive Habit";
                        }
                        else
                        {
                            isGood = "Negative Habit";
                        }
                    }

                    if (displayImportance)
                    {
                        importance = habit.Imporatance.ToString();
                    }

                    CLIHelper.MsgForWindow($"| Habit: {habit.Name} ", "...|", $"{completion} |");
                    CLIHelper.MsgForWindow($"| Importance: {importance} ", "...|", $"({isGood}) |");

                    if (displayDescription)
                    {
                        CLIHelper.MsgForWindow($"| Desc: {habit.Description} ", "...|", "|");
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

        public void PromptForHabitView()
        {
        }

        public void PromptForHabitCreation()
        {
            string name, description;
            bool isGood;
            int importance;

            CLIHelper.Info("You can make changes to the habit after answering the following prompts.");

            name = PromptForName();
            description = PromptForDescription();
            isGood = PromptForIsGood();
            importance = PromptForImportance();

            HabitObject newHabit = new HabitObject(name, isGood, description, importance);

            PromptForHabitCorrection(newHabit);

            if (CLIHelper.PromptForTrueFalseInput("Save this habit?"))
            {
                habitManager.AddHabit(newHabit);
            }
        }

        public HabitObject PromptForHabitObject()
        {
            string userInput;
            List<HabitObject> habits = habitManager.getHabits();
            List<string> habitNames = habits.Select(habit => habit.Name).ToList();

            while (true)
            {
                DisplayAllHabits();

                userInput = CLIHelper.PromptForNotEmptyInput("Enter the habit name: ");

                if (habitNames.Contains(userInput))
                {
                    return habits.First(habit => habit.Name == userInput);
                }
            }
        }

        public void PromptForHabitDelete()
        {
            habitManager.RemoveHabit(PromptForHabitObject().Name);
        }

        public void PromptForHabitEdit()
        {
            PromptForHabitCorrection(PromptForHabitObject());
        }

        public void SaveHabits()
        {
            habitManager.SaveHabits();
        }

        private bool PromptForHabitCorrection(HabitObject habitObject)
        {
            string? userInput;

            while (true)
            {
                List<string> prompt = new List<string>();
                prompt.Add("<If a detail is incorrect, type it's name to change the property, otherwise press enter.>");
                prompt.Add("Habit details:");
                prompt.Add($"- Name: {habitObject.Name}");
                prompt.Add($"- Desc: {habitObject.Description}");
                prompt.Add($"- IsGood: {habitObject.IsGood}");
                prompt.Add($"- Importance: {habitObject.Imporatance}");

                userInput = CLIHelper.PromptForNotNullInput(prompt.ToArray());
                userInput.ToLower();

                switch (userInput)
                {
                    case "name":
                        habitObject.Name = PromptForName();
                        break;
                    case "desc":
                        habitObject.Description = PromptForDescription();
                        break;
                    case "isgood":
                        habitObject.IsGood = PromptForIsGood();
                        break;
                    case "importance":
                        habitObject.Imporatance = PromptForImportance();
                        break;
                    case "":
                        return true;
                    default:
                        CLIHelper.Error("Input was not valid, try again!");
                        break;
                }
            }
        }

        private string PromptForName()
        {
            string namePrompt = "Enter the name of the habit: ";
            return CLIHelper.PromptForNotEmptyInput(namePrompt);
        }

        private string PromptForDescription()
        {
            string descriptionPrompt = "Enter the description of the habit: ";
            return CLIHelper.PromptForNotEmptyInput(descriptionPrompt);
        }

        private bool PromptForIsGood()
        {
            string isGoodPrompt = "Is this a good habit, enter true or false: ";
            return CLIHelper.PromptForTrueFalseInput(isGoodPrompt);
        }

        private int PromptForImportance()
        {
            int? importance = null;

            string importancePrompt = "If 1 is trivial and 5 is of utmost importance, enter the digit that represents the habits importance: ";
            while ((importance = CLIHelper.PromptForIntInput(importancePrompt)) == null || 1 > importance || importance > 5)
            {
                CLIHelper.Clear();
                CLIHelper.Error("Input was not valid, try again!");
            }

            return (int)importance;
        }
    }
}
