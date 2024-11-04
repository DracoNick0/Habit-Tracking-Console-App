using Habit_Tracking_Console_App.Backend.Objects;

namespace Task_Tracking_Console_App.Backend.Objects
{
    class TaskObject
    {
        private string name;
        private int importance;
        private bool isGood;
        private string description;
        private int completions;
        private RecurrenceEnum recurrence;
        private int occurrence;
        private DateTime dueDate;

        public TaskObject()
        {
            this.name = string.Empty;
            this.importance = 0;
            this.isGood = false;
            this.description = string.Empty;
            this.completions = 0;
            this.recurrence = RecurrenceEnum.none;
            this.occurrence = 1;
            this.dueDate = DateTime.Now.AddDays(1);
        }

        public TaskObject(string name, int importance, bool isGood, string description, RecurrenceEnum recurrence, int occurrence, DateTime dueDate)
        {
            this.name = name;
            this.importance = importance;
            this.isGood = isGood;
            this.description = description;
            this.recurrence = recurrence;
            this.occurrence = occurrence;
            this.completions = 0;
            this.dueDate = dueDate;
        }

        public void Edit(string? name = null, int? importance = null, bool? isGood = null, string? description = null, RecurrenceEnum? recurrence = null, int? occurrence = null, DateTime? dueDate = null)
        {
            this.name = name ?? this.name;
            this.importance = importance ?? this.importance;
            this.isGood = isGood ?? this.isGood;
            this.description = description ?? this.description;
            this.recurrence = recurrence ?? this.recurrence;
            this.occurrence = occurrence ?? this.occurrence;
            this.dueDate = dueDate ?? this.dueDate;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsGood
        {
            get { return isGood; }
            set { isGood = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Importance
        {
            get { return importance; }
            set { importance = value; }
        }

        public int Completions
        {
            get { return completions; }
            set { completions = value; }
        }

        public RecurrenceEnum Recurrence
        {
            get { return recurrence; }
            set { recurrence = value; }
        }

        public int Occurrence
        {
            get { return occurrence; }
            set { occurrence = value; }
        }
    }
}
