using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 4: элементы между min и max");

        Console.Write("введите размер массива K: ");
        int k = int.Parse(Console.ReadLine());

        Console.Write("введите левую границу A: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("введите правую границу B: ");
        int b = int.Parse(Console.ReadLine());

        Random random = new Random();
        int[] array = new int[k];

        // генерация массива
        for (int i = 0; i < k; i++)
        {
            array[i] = random.Next(a, b + 1);
        }

        Console.WriteLine("\nсгенерированный массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();

        // поиск индексов min и max
        int minIndex = 0;
        int maxIndex = 0;

        for (int i = 1; i < k; i++)
        {
            if (array[i] < array[minIndex]) minIndex = i;
            if (array[i] > array[maxIndex]) maxIndex = i;
        }

        int start = Math.Min(minIndex, maxIndex);
        int end = Math.Max(minIndex, maxIndex);

        Console.WriteLine($"\nиндекс min: {minIndex}, индекс max: {maxIndex}");
        Console.Write("элементы между ними (включая границы): ");

        for (int i = start; i <= end; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }
}