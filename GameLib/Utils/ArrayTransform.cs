using System;
namespace GameLib
{
    public static class ArrayTransform
    {
        public static T[,] RotateMatrix<T>(T[,] matrix, bool r = true)
        {
            int n = matrix.GetLength(0);
            T[,] ret = new T[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (r)
                    {
                        ret[i, j] = matrix[n - j - 1, i];
                    }
                    else
                    {
                        ret[n - j - 1, i] = matrix[i, j];
                    }

                }
            }

            return ret;
        }

        public static T[,] FlipHorizontal<T>(T[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            T[,] ret = new T[colLength, rowLength];

            for (int i = 0; i < colLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    ret[i, j] = matrix[colLength - i - 1, j];
                }
            }

            return ret;
        }

        public static T[,] FlipVertical<T>(T[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            T[,] ret = new T[rowLength, colLength];


            for (int i = 0; i < colLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    ret[i, j] = matrix[i, rowLength - j - 1];
                }
            }

            return ret;
        }
    }

}
