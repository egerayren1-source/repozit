using System;
class Program
{
    static void Main()
    {
        // ввод трех чисел
        Console.WriteLine($"расчёт сернего арифметического по 3 числам");

        Console.Write("введите первое число: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("введите второе число: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("введите третье число: ");
        double c = Convert.ToDouble(Console.ReadLine());

        // расчет среднего значения
        double srednee = (a + b + c) / 3.0;

        // вывод результата
        Console.WriteLine($"среднее арифметическое: {srednee:F2}");
    }
}