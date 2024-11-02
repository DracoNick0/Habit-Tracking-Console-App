namespace Habit_Tracking_Console_App.Backend
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
            foreach (Action action in previousActions)
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
