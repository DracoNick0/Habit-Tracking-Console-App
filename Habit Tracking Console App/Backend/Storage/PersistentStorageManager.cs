using System.Text.Json;
using Task_Tracking_Console_App.Backend.Objects;

namespace Task_Tracking_Console_App.Backend.Storage
{
    class PersistentStorageManager
    {
        private const string taskStoragePath = @"../../../Backend/Storage/Task_Storage.json";

        public PersistentStorageManager() { }

        public void SaveTasks(List<TaskObject> tasks)
        {
            string jsonString = JsonSerializer.Serialize(tasks);
            File.WriteAllText(taskStoragePath, jsonString);
        }

        public List<TaskObject> RetrieveTasks()
        {
            string jsonString = File.ReadAllText(taskStoragePath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                List<TaskObject>? tasks = JsonSerializer.Deserialize<List<TaskObject>>(jsonString);
                if (tasks != null)
                {
                    return tasks;
                }
            }

            return new List<TaskObject>();
        }
    }
}
