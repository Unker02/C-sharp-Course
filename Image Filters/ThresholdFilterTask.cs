using System.Collections.Generic;
using System;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
            var result = new double[original.GetLength(0), original.GetLength(1)];
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var threshold = 0.0;
            var whitePixelsCount = (int)(whitePixelsFraction * width * height);

            threshold = FindThreshold(whitePixelsCount, GetSortedValues(original, width, height));

            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    if (original[x, y] >= threshold)
                        result[x, y] = 1;
                    else
                        result[x, y] = 0;
                }
            return result;
        }

        private static double FindThreshold (int whitePixelsCount, double[] originalValues)
        {
            if (whitePixelsCount == 0)
                return double.MaxValue;
            else
                return originalValues[originalValues.Length - whitePixelsCount];
        }

        private static double[] GetSortedValues (double[,] original, int width, int height)
        {
            var list = new List<double>();
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    list.Add(original[x, y]);
            double[] result = list.ToArray();
            Array.Sort(result);
            return result;
        }
	}
}