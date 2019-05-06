using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sxSize = sx.GetLength(0);

            var sy = Transpose(sx);

            for (int x = 0; x < width ; x++)
                for (int y = 0; y < height; y++)
                {
                    var nearest = GetNearest(g, x, y, sxSize);
                    var gx = Multiply(sx, nearest, nearest.GetLength(0));
                    var gy = Multiply(sy, nearest, nearest.GetLength(1));
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }
        
        private static double[,] Transpose(double[,] sx)
        {
            var n = sx.GetUpperBound(0) + 1;
            var m = sx.GetUpperBound(1) + 1;
            var result = new double[m, n];
            for (var i = 0; i < n; i++)
                for (var j = 0; j < m; j++)
                    result[j, i] = sx[i, j];
            return result;
        }
        
        public static double[,] GetNearest(double[,] g, int x, int y, int size)
        {
            var result = new double[size, size];
            var mid = size / 2;

            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                {
                    if (ZeroBorder(g, x, y, size))
                        continue;
                    else
                        result[i, j] = g[x - mid + i, y - mid + j];
                }
            return result;
        }

        private static double Multiply(double[,] sxy, double[,] nearest, int size)
        {
            var sum = 0d;
            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    sum += sxy[i, j] * nearest[i, j];
            return sum;
        }

        private static bool ZeroBorder(double[,] g, int x, int y, int size)
        {
            var mid = size / 2;
            if (x - mid < 0)
                return true;
            else if (y - mid < 0)
                return true;
            else if (x - mid + size - 1 >= g.GetLength(0))
                return true;
            else if (y - mid + size - 1 >= g.GetLength(1))
                return true;
            return false;
        }
    }
}