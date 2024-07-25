using Azure.Core;
using System.Globalization;
using TimeFlow.Models;

namespace TimeFlow.utils
{
    public class WeekHelper
    {
        public static int GetCurrentWeekNumber()
        {
            return ISOWeek.GetWeekOfYear(DateTime.Now);
        }

        public static List<FeuilleJour> InitializeJours(int Semaine, int year)
        {
            List<FeuilleJour> jours = new List<FeuilleJour>();

            // Set the current date to the first day of the specified week
            DateTime currentDate = getMonday(year, Semaine);

            // Create entries for 7 days, starting from the specified week
            for (int i = 0; i < 7; i++)
            {
                FeuilleJour jour = new()
                {
                    JourNom = currentDate.ToString("dddd"),
                    Date = currentDate,
                };

                jours.Add(jour);

                // Move to the next day
                currentDate = currentDate.AddDays(1);
            }

            return jours;
        }
        public static DateTime getMonday(int year, int weekNumber)
        {
            return ISOWeek.ToDateTime(year, weekNumber, DayOfWeek.Monday);
        }

        public static int getWeekNumber(DateTime date)
        {
            return ISOWeek.GetWeekOfYear(date);
        }

        public static int getWeekNumber(string weekString)
        {
            var week = weekString.Split("-W");
            return Convert.ToInt32(week[1]);
        }

        public static string getWeekString(DateTime date)
        {
            int week = ISOWeek.GetWeekOfYear(date);
            bool islessThan10 = week < 10;
            return $"{date.Year}-W{(islessThan10 ? "0" : "")}{week}";
        }

        public static string getWeekString(int year, int weekNumber)
        {
            bool islessThan10 = weekNumber < 10;
            return $"{year}-W{(islessThan10 ? "0" : "")}{weekNumber}";
        }

        public static DateTime getMonday(string weekString)
        {
            var week = weekString.Split("-W");
            return ISOWeek.ToDateTime(Convert.ToInt32(week[0]), Convert.ToInt32(week[1]), DayOfWeek.Monday);
        }

        public static DateTime getSunday(string weekString)
        {
            var week = weekString.Split("-W");
            return ISOWeek.ToDateTime(Convert.ToInt32(week[0]), Convert.ToInt32(week[1]), DayOfWeek.Sunday);
        }

        internal static int getYear(string weekString)
        {
            var week = weekString.Split("-W");
            return Convert.ToInt32(week[0]);
        }
    }
}
