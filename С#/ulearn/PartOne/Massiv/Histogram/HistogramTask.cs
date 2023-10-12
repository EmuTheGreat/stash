using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var amountOfDays = 31;
            var numberOfDay = new string[amountOfDays];
            for (int i = 0; i < amountOfDays;) numberOfDay[i] = (++i).ToString();

            var numberOfPeople = new double[amountOfDays];
            foreach (var person in names)
                if (person.Name == name && person.BirthDate.Day != 1)
                    numberOfPeople[person.BirthDate.Day - 1]++;

            return new HistogramData($"Рождаемость людей с именем '{name}'", numberOfDay, numberOfPeople);
        }
    }
}