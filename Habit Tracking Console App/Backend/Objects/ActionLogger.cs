namespace Habit_Tracking_Console_App.Backend.Objects
{
    /// <summary>
    /// Stores and executes stored actions.
    /// </summary>
    class ActionLogger
    {
        private static List<Action> previousActions = new List<Action>();

        /// <summary>
        /// Saves an action to be executed later.
        /// </summary>
        /// <param name="action">Action to be saved.</param>
        public static void AddAction(Action action)
        {
            previousActions.Add(action);
        }

        /// <summary>
        /// Executes all stored actions.
        /// </summary>
        public static void ExecuteStoredActions()
        {
            Console.Clear();
            foreach (var action in previousActions)
            {
                action.Invoke();
            }
        }

        /// <summary>
        /// Removes all stored actions.
        /// </summary>
        public static void ClearStoredActions()
        {
            previousActions.Clear();
        }
    }
}
