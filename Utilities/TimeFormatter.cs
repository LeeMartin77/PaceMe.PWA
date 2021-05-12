namespace PaceMe.BlazorApp.Utilities
{
    public static class TimeFormatter
    {
        public static string SecondsToTime(int seconds)
        {
            int wholehours = seconds / (60 * 60);
            if (wholehours > 0) 
            {
                return $"{wholehours}:{((seconds - (wholehours * 60 * 60)) / 60):00}:{(seconds % 60):00}";
            }
            return $"{(seconds / 60)}:{(seconds % 60):00}";
        }
    }
}