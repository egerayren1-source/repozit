using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("задача 2: массив с ограниченной суммой");

        Console.Write("введите максимальную сумму (N > 0): ");
        int maxSum = int.Parse(Console.ReadLine());

        if (maxSum <= 0)
        {
            Console.WriteLine("ошибка: число должно быть больше 0!");
            return;
        }

        Random rnd = new Random();
        int currentSum = 0;
        int count = 0;

        int[] tempArray = new int[maxSum];

        // продолжаем цикл, пока текущая сумма меньше максимальной
        while (currentSum < maxSum)
        {
            // считаем, сколько еще можно добавить
            int remaining = maxSum - currentSum;

            // если осталось добрать больше или равно 9, берем от 1 до 9
            // если осталось меньше 9, берем от 1 до remaining
            int maxRandomValue = Math.Min(9, remaining);
            int val = rnd.Next(1, maxRandomValue + 1);

            tempArray[count] = val;
            currentSum += val;
            count++;
        }

        // формирование итогового массива
        int[] result = new int[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = tempArray[i];
        }

        Console.WriteLine($"\nсгенерированный массив ({count} элементов):");
        for (int i = 0; i < count; i++)
        {
            Console.Write(result[i] + " ");
        }
        Console.WriteLine();
        Console.WriteLine($"сумма элементов: {currentSum} (предел: {maxSum})");
    }
}