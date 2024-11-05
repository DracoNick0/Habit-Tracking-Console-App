using System;
using Habit_Tracking_Console_App.Backend.Objects;

namespace Task_Tracking_Console_App.Backend.Objects
{
    class TaskObject
    {
        private string name;
        private int difficulty;
        private string description;
        private int completions;
        private RecurrenceEnum recurrence;
        private int occurrence;  
        private DateOnly dueDate;

        public TaskObject()
        {
            this.name = string.Empty;
            this.difficulty = 0;
            this.description = string.Empty;
            this.completions = 0;
            this.recurrence = RecurrenceEnum.none;
            this.occurrence = 1;
            this.dueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
        }

        public TaskObject(string name, int difficulty, string description, RecurrenceEnum recurrence, int occurrence, DateOnly dueDate)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.description = description;
            this.recurrence = recurrence;
            this.occurrence = occurrence;
            this.completions = 0;
            this.dueDate = dueDate;
        }

        public void Edit(string? name = null, int? difficulty = null, string? description = null, RecurrenceEnum? recurrence = null, int? occurrence = null, DateOnly? dueDate = null)
        {
            this.name = name ?? this.name;
            this.difficulty = difficulty ?? this.difficulty;
            this.description = description ?? this.description;
            this.recurrence = recurrence ?? this.recurrence;
            this.occurrence = occurrence ?? this.occurrence;
            this.dueDate = dueDate ?? this.dueDate;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int Difficulty
        {
            get { return this.difficulty; }
            set { this.difficulty = value; }
        }

        public int Completions
        {
            get { return this.completions; }
            set { this.completions = value; }
        }

        public RecurrenceEnum Recurrence
        {
            get { return this.recurrence; }
            set { this.recurrence = value; }
        }

        public int Occurrence
        {
            get { return this.occurrence; }
            set { this.occurrence = value; }
        }

        public DateOnly DueDate
        {
            get { return this.dueDate; }
            set { this.dueDate = value; }
        }
    }
}
