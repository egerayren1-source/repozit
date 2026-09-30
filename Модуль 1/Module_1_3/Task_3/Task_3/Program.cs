using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 3: сортировка строк матрицы");

        Console.Write("введите размер квадратной матрицы N: ");
        int n = int.Parse(Console.ReadLine());

        int[,] matrix = new int[n, n];
        Random rnd = new Random();

        // заполнение случайными числами от -50 до 50
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rnd.Next(-50, 51);
            }
        }

        Console.WriteLine("\nисходная матрица:");
        PrintMatrixWithSums(matrix, n);

        // сортировка строк методом пузырька по суммам элементов
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (GetRowSum(matrix, j, n) > GetRowSum(matrix, j + 1, n))
                {
                    SwapRows(matrix, j, j + 1, n);
                }
            }
        }

        Console.WriteLine("\nматрица после сортировки строк:");
        PrintMatrixWithSums(matrix, n);
    }

    // вычисление суммы элементов отдельной строки
    static int GetRowSum(int[,] matrix, int rowIndex, int cols)
    {
        int sum = 0;
        for (int j = 0; j < cols; j++)
        {
            sum += matrix[rowIndex, j];
        }
        return sum;
    }

    // обмен местами двух строк матрицы
    static void SwapRows(int[,] matrix, int row1, int row2, int cols)
    {
        for (int j = 0; j < cols; j++)
        {
            int temp = matrix[row1, j];
            matrix[row1, j] = matrix[row2, j];
            matrix[row2, j] = temp;
        }
    }

    // печать матрицы вместе с суммой каждой строки
    static void PrintMatrixWithSums(int[,] matrix, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{matrix[i, j],5} ");
            }
            Console.WriteLine($" | сумма = {GetRowSum(matrix, i, n)}");
        }
    }
}