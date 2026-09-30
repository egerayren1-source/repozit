using System;
class Program
{
    static void Main()
    {
        int[] mas = new int[10];
        Random rnd = new Random();
        int summa = 0;

        // заполнение массива и подсчет суммы
        for (int i = 0; i < mas.Length; i++)
        {
            mas[i] = rnd.Next(1, 101);
            summa += mas[i];
        }

        // вывод элементов массива
        Console.Write("элементы массива: ");
        for (int i = 0; i < mas.Length; i++)
        {
            Console.Write(mas[i] + " ");
        }
        Console.WriteLine();

        // вывод итоговой суммы
        Console.WriteLine($"сумма всех элементов: {summa}");
    }
}