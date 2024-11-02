namespace Habit_Tracking_Console_App.Model
{
    class ActionLogger
    {
        private static List<Action> previousActions = new List<Action>();

        public static void AddAction(Action action)
        {
            previousActions.Add(action);
        }

        public static void ExecuteStoredActions()
        {
            Console.Clear();
            foreach (var action in previousActions)
            {
                action.Invoke();
            }
        }

        public static void ClearStoredActions()
        {
            previousActions.Clear();
        }
    }
}
