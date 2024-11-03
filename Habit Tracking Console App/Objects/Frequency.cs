namespace Habit_Tracking_Console_App.Objects
{
    public enum FrequencyInterval
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    struct Frequency(FrequencyInterval interval, int occurence)
    {
        public FrequencyInterval interval = interval;
        public int occurences = occurence;
    }
}
