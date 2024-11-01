namespace Habit_Tracking_Console_App.Objects
{
    class TaskObject
    {
        private string name;
        private string description;
        private int importance;
        private DateTime startDate;
        private DateTime dueDate;
        private bool completed;

        public TaskObject()
        {
            name = string.Empty;
            description = string.Empty;
            importance = 0;
            startDate = DateTime.Now;
            dueDate = DateTime.Now;
            completed = false;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Imporatance
        {
            get { return importance; }
            set { importance = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }

    }
}
