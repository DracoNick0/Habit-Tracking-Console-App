using System.Text.Json;

namespace Habit_Tracking_Console_App.Model.Storage
{
    class StorageManager
    {
        private const string habitStoragePath = @"../../../Model/Storage/Habit_Storage.json";

        public StorageManager() { }

        public void SaveHabits(List<HabitObject> habits)
        {
            string jsonString = JsonSerializer.Serialize(habits);
            File.WriteAllText(habitStoragePath, jsonString);
        }

        public List<HabitObject> RetrieveHabits()
        {
            string jsonString = File.ReadAllText(habitStoragePath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                List<HabitObject>? habits = JsonSerializer.Deserialize<List<HabitObject>>(jsonString);
                if (habits != null)
                {
                    return habits;
                }
            }

            return new List<HabitObject>();
        }
    }
}
