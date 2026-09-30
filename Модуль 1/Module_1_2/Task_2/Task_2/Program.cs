using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 2: замена максимального элемента");

        Random random = new Random();
        int[] array = new int[10];

        // заполнение случайными числами
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(-50, 51);
        }

        Console.WriteLine("исходный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();

        Console.Write("\nвведите число для замены: ");
        int newValue = int.Parse(Console.ReadLine());

        // поиск индекса первого максимума
        int maxIndex = 0;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        // замена элемента
        array[maxIndex] = newValue;

        Console.WriteLine("\nизмененный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }
}