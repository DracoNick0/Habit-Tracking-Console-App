using Habit_Tracking_Console_App.Objects;

namespace Habit_Tracking_Console_App.Interface
{
    class HabitInterface
    {
        public HabitInterface() { }

        public HabitObject HabitCreator()
        {
            string name, description;
            bool isGood;
            int importance;

            CLIHelper.Info("You can make changes to the habit after answering the following prompts.");

            name = PromptForName();
            description = PromptForDescription();
            isGood = PromptForIsGood();
            importance = PromptForImportance();

            PromptForHabitCorrection(ref name, ref description, ref isGood, ref importance);

            return new HabitObject(name, isGood, description, importance);
        }

        private bool PromptForHabitCorrection(ref string name, ref string description, ref bool isGood, ref int importance)
        {
            string? userInput;

            string prompt = "<If a detail is incorrect, type it's name to change the property, otherwise press enter.>\n";
            prompt += "Habit details:\n";
            prompt += "- Name: " + name + "\n";
            prompt += "- Desc: " + description + "\n";
            prompt += "- IsGood: " + isGood + "\n";
            prompt += "- Importance: " + importance + "\n";

            while (true)
            {
                userInput = CLIHelper.PromptForNotNullInput(prompt);
                userInput.ToLower();

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
                    case "":
                        return true;
                    default:
                        Console.Error.WriteLine("Input was not valid, try again!");
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
                Console.Clear();
                CLIHelper.Error("Input was not valid, try again!");
            }

            return (int)importance;
        }
    }
}
