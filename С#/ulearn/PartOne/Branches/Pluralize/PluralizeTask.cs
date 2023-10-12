using System;

namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
            int lastValue = count % 10;
            int penultValue = count % 100 / 10;

            if (penultValue != 1 && lastValue == 1) 
                return "рубль";
            
            else if (lastValue > 1 && penultValue != 1 && lastValue < 5) 
                return "рубля";
            
            else return "рублей";
        }
	}
}