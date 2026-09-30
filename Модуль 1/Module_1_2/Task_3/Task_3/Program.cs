using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 3: K простых чисел");

        Console.Write("введите количество простых чисел K: ");
        int k = int.Parse(Console.ReadLine());

        int count = 0;
        int number = 2;

        Console.WriteLine($"\nпервые {k} простых чисел:");

        while (count < k)
        {
            if (IsPrime(number))
            {
                Console.Write($"{number}\t");
                count++;

                // перенос строки каждые 10 чисел
                if (count % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            number++;
        }
        Console.WriteLine();
    }

    // проверка числа на простоту
    static bool IsPrime(int n)
    {
        if (n < 2) return false;

        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0) return false;
        }

        return true;
    }
}