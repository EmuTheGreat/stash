using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var filteredPixels = new double[width, height];
            var sy = Transpose(sx);
            var offset = sx.GetLength(0) / 2;

            for (int x = offset; x < width - offset; x++)
                for (int y = offset; y < height - offset; y++)
                {
                    var gy = DoConvolution(g, sy, offset, x, y);
                    var gx = DoConvolution(g, sx, offset, x, y);
                    filteredPixels[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return filteredPixels;
        }
        
        static double[,] Transpose(double[,] matrix)
        {
            var line = matrix.GetLength(0);
            var transposed = new double[line, line];
            for (int i = 0; i < line; i++)
                for (int j = 0; j < line; j++) transposed[i, j] = matrix[j, i];
            return transposed;
        }

        static double DoConvolution(double[,] original, double[,] matrix, int offset, int x, int y)
        {
            double result = 0;
            var line = matrix.GetLength(0);
            for (int i = 0; i < line; i++)
                for (int j = 0; j < line; j++)
                    result += matrix[i, j] * original[x - offset + i, y - offset + j];
            return result;
        }
    }
}