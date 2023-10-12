using System;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var threshold = GetThreshold(width, height, whitePixelsFraction, original);
            var filteredPixels = new double[width, height];;

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    filteredPixels[x, y] = original[x, y] >= threshold ? 1.0 : 0.0;
            
            return filteredPixels;
        }
        
        static double GetThreshold(int width, int height, double whitePixelsFraction, double[,] original)
        {
            var totalPixels = width * height;
            var pixels = new double[totalPixels];
            var totalWhitePixels = (int)(totalPixels * whitePixelsFraction);
            var i = 0;
            
            foreach (var e in original) pixels[i++] = e;
            Array.Sort(pixels);
            
            if (totalWhitePixels == 0) return double.MaxValue;
            else if (totalWhitePixels == totalPixels) return -1d;
            return pixels[totalPixels - totalWhitePixels];
        }
    }
}