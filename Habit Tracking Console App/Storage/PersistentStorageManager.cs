﻿using System.Text.Json;
using Habit_Tracking_Console_App.Objects;

namespace Habit_Tracking_Console_App.Storage
{
    class PersistentStorageManager
    {
        private const string habitStoragePath = @"../../../Storage/Habit_Storage.json";

        public PersistentStorageManager() { }

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
