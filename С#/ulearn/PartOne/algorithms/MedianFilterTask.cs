using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var values = new List<double>();
            var filteredPixels = new double[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    filteredPixels[x, y] = ApplyFilter(x, y, original, width - 1, height - 1, values);
                    values.Clear();
                }
            
            return filteredPixels;
        }

        static double ApplyFilter(int x, int y, double[,] original, int width, int height, List<double> values)
        {
            for (int offsetX = -1; offsetX < 2; offsetX++)
                for (int offsetY = -1; offsetY < 2; offsetY++)
                {
                    var borderY = offsetY + y;
                    var borderX = offsetX + x;
                    if (borderX < 0 || borderY < 0 || borderX > width || borderY > height) continue;
                    values.Add(original[borderX, borderY]);
                }
            values.Sort();
            if (values.Count % 2 == 0) return (values[values.Count / 2 - 1] + values[values.Count / 2]) / 2;
            return values[values.Count / 2];
        }
    }
}