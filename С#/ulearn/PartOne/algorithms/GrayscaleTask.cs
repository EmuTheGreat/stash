namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
			var width = original.GetLength(0);
			var height = original.GetLength(1);
            var grayScale = new double[width, height];

			var red = 0.299 / 255;
			var green = 0.587 / 255;
			var blue = 0.114 / 255;

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
				{
					var pixel = original[x, y];
                    grayScale[x, y] = red * pixel.R + green * pixel.G + blue * pixel.B;
				}
			
			return grayScale;
        }
    }
}