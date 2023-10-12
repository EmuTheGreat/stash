using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Program
    {
        static void Main()
        {
            string userInput = Console.ReadLine();
            Console.WriteLine(Calculate(userInput));
        }
        static double Calculate(string userInput)
        {
            string[] values = userInput.Split(' ');
            double sum = double.Parse(values[0]);
            double percent = double.Parse(values[1]);
            int period = int.Parse(values[2]);

            double percentInMounth = Math.Pow((1 + percent / 1200), period);

            return sum * percentInMounth;
        }
    }
}
