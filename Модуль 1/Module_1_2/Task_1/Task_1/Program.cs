using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 1: нормировка массива");

        Console.Write("введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());

        double[] array = new double[n];

        // ввод элементов с клавиатуры
        for (int i = 0; i < n; i++)
        {
            Console.Write($"элемент [{i}]: ");
            array[i] = double.Parse(Console.ReadLine());
        }

        // поиск максимального по модулю элемента
        double maxAbs = Math.Abs(array[0]);
        for (int i = 1; i < n; i++)
        {
            if (Math.Abs(array[i]) > maxAbs)
            {
                maxAbs = Math.Abs(array[i]);
            }
        }

        // проверка деления на ноль
        if (maxAbs == 0)
        {
            Console.WriteLine("все элементы равны 0. нормировка невозможна.");
            return;
        }

        // нормировка и вывод
        Console.WriteLine("\nнормированный массив:");
        for (int i = 0; i < n; i++)
        {
            array[i] /= maxAbs;
            Console.Write($"{array[i]:F4} ");
        }
        Console.WriteLine();
    }
}