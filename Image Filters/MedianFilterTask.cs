using System;
using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var result = new double[width, height];

            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    result[x,y] = GetMediane(FindNearestValues(original, x, y, width, height));

            return result;
        }

        private static double[] FindNearestValues(double[,] original, int x, int y, int width, int height)
        {
            var result = new List<double>();
            var leftBorder = x - 1;
            var topBorder = y - 1;
            var rightBorder = x + 1;
            var bottomBorder = y + 1;

            if (leftBorder < 0)
                leftBorder = x;
            if (topBorder < 0)
                topBorder = y;
            if (rightBorder >= width)
                rightBorder = x;
            if (bottomBorder >= height)
                bottomBorder = y;

            for (int i = leftBorder; i <= rightBorder; i++)
                for (int j = topBorder; j <= bottomBorder; j++)
                    result.Add(original[i, j]);

            return result.ToArray();
        }

        private static double GetMediane(double[] array)
        {
            var result = 0.0;
            var lenght = array.Length;
            Array.Sort(array);
            if (lenght % 2 == 0)
                result = (array[lenght / 2] + array[lenght / 2 - 1] )/ 2;
            else
                result = array[lenght / 2];
            return result;
        }
	}
}