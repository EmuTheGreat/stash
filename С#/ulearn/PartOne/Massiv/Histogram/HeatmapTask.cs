using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            int countOfDays = 30;
            int countOfMonths = 12;

            var days = new string[countOfDays];
            var months = new string[countOfMonths];
            double[,] heat = new double[countOfDays, countOfMonths];
            int birthDay;

            WriteInArray(days, countOfDays, 2);
            WriteInArray(months, countOfMonths, 1);

            foreach (var person in names)
            {
                birthDay = person.BirthDate.Day;
                if (birthDay != 1) heat[birthDay - 2, person.BirthDate.Month - 1]++;
            }

            return new HeatmapData("Пример карты интенсивностей", heat, days, months);
        }

        static void WriteInArray(string[] array , int length, int difference)
        {
            for (int i = 0; i < length; i++) 
                array[i] = (i + difference).ToString(); 
        }
    }
}