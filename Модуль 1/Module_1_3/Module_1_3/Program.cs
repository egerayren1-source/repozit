using System;
using System.Linq;

namespace Module_1_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // главный цикл программы
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Модуль 1.3 Статические методы");
                Console.WriteLine("1. Сокращение обыкновенной дроби (НОД)");
                Console.WriteLine("2. Массив минимальной длины с суммой <= N");
                Console.WriteLine("3. Сортировка строк матрицы по сумме элементов");
                Console.WriteLine("0. Выход");

                int choice = ReadInt("Выберите номер задачи (0-3): ", 0, 3);
                if (choice == 0) break;

                Console.Clear();
                switch (choice)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                }
            }
        }

        // задача 1: сокращение дроби с использованием статического метода вычисления НОД
        static void Task1()
        {
            Console.WriteLine("Задача 1: Сокращение дроби");
            int numerator = ReadInt("Введите неотрицательный числитель: ", 0, int.MaxValue);
            int denominator = ReadInt("Введите положительный знаменатель: ", 1, int.MaxValue);

            // нахождение наибольшего общего делителя
            int gcd = GetGcd(numerator, denominator);

            // сокращение числителя и знаменателя
            int reducedNumerator = numerator / gcd;
            int reducedDenominator = denominator / gcd;

            Console.WriteLine($"\nИсходная дробь: {numerator}/{denominator}");
            Console.WriteLine($"Сокращенная дробь: {reducedNumerator}/{reducedDenominator}");
        }

        // задача 2: генерация массива минимальной длины, сумма элементов которого не превышает заданное число
        static void Task2()
        {
            Console.WriteLine("Задача 2: Массив с ограниченной суммой");
            int maxSum = ReadInt("Введите максимальное значение суммы (больше 0): ", 1, int.MaxValue);

            Random rnd = new Random();
            int currentSum = 0;
            int count = 0;

            // создание временного массива заведомо достаточного размера
            int[] tempArray = new int[maxSum];

            while (true)
            {
                // генерация случайного числа от 1 до 9
                int val = rnd.Next(1, 10);

                // если добавление элемента превысит сумму, прекращаем генерацию
                if (currentSum + val > maxSum)
                {
                    break;
                }

                tempArray[count] = val;
                currentSum += val;
                count++;
            }

            // копирование элементов в массив точного размера
            int[] result = new int[count];
            Array.Copy(tempArray, result, count);

            Console.WriteLine($"\nСгенерированный массив ({count} элементов):");
            Console.WriteLine(string.Join(" ", result));
            Console.WriteLine($"Сумма элементов: {currentSum} (предел: {maxSum})");
        }

        // задача 3: сортировка строк квадратной матрицы по возрастанию сумм элементов
        static void Task3()
        {
            Console.WriteLine("Задача 3: Сортировка строк матрицы");
            int n = ReadInt("Введите размер квадратной матрицы N: ", 1, 100);

            int[,] matrix = new int[n, n];
            Random rnd = new Random();

            // заполнение матрицы случайными числами от -50 до 50
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = rnd.Next(-50, 51);
                }
            }

            Console.WriteLine("\nИсходная матрица:");
            PrintMatrixWithSums(matrix, n);

            // сортировка строк методом пузырька по суммам элементов
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (GetRowSum(matrix, i, n) > GetRowSum(matrix, j, n))
                    {
                        SwapRows(matrix, i, j, n);
                    }
                }
            }

            Console.WriteLine("\nМатрица после сортировки строк по возрастанию сумм:");
            PrintMatrixWithSums(matrix, n);
        }

        // статический метод вычисления наибольшего общего делителя (алгоритм евклида)
        static int GetGcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // вычисление суммы элементов отдельной строки матрицы
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

        // ввод целых чисел с валидацией ввода
        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= min && result <= max)
                    return result;
                Console.WriteLine("Ошибка! Введите корректное целое число.");
            }
        }
    }
}