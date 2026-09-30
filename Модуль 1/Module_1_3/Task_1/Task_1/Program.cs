using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 1: сокращение дроби");

        Console.Write("введите неотрицательный числитель: ");
        int numerator = int.Parse(Console.ReadLine());

        Console.Write("введите положительный знаменатель: ");
        int denominator = int.Parse(Console.ReadLine());

        if (denominator <= 0)
        {
            Console.WriteLine("ошибка: знаменатель должен быть больше 0!");
            return;
        }

        // нахождение наибольшего общего делителя
        int gcd = GetGcd(numerator, denominator);

        // сокращение числителя и знаменателя
        int reducedNumerator = numerator / gcd;
        int reducedDenominator = denominator / gcd;

        Console.WriteLine($"\nисходная дробь: {numerator}/{denominator}");
        Console.WriteLine($"сокращенная дробь: {reducedNumerator}/{reducedDenominator}");
    }

    // статический метод вычисления нод (алгоритм евклида)
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
}