using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 6: массив индексов");

        Random random = new Random();
        double[] array = new double[10];

        // заполнение массива
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = -10.0 + random.NextDouble() * 20.0;
        }

        Console.WriteLine("исходный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"{array[i]:F2}\t");
        }
        Console.WriteLine();

        // массив индексов [0, 1, 2, ..., 9]
        int[] indices = new int[array.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = i;
        }

        // сортировка массива индексов пузырьком по значениям из array
        for (int i = 0; i < indices.Length - 1; i++)
        {
            for (int j = 0; j < indices.Length - 1 - i; j++)
            {
                if (array[indices[j]] > array[indices[j + 1]])
                {
                    int temp = indices[j];
                    indices[j] = indices[j + 1];
                    indices[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("\nмассив индексов (по возрастанию элементов):");
        for (int i = 0; i < indices.Length; i++)
        {
            Console.Write(indices[i] + " ");
        }
        Console.WriteLine();

        Console.WriteLine("\nпроверка (элементы по отсортированным индексам):");
        for (int i = 0; i < indices.Length; i++)
        {
            Console.Write($"{array[indices[i]]:F2}\t");
        }
        Console.WriteLine();
    }
}