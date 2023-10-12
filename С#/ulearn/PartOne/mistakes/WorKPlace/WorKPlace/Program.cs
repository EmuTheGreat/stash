using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorKPlace
{
    internal class Program
    {
        enum Lights
        {
            Red,
            Green = 1,
            Blue
        }
        static void Main()
        {
            var x = (int)Lights.Red;
            Console.WriteLine(x);
        }
    }
}
