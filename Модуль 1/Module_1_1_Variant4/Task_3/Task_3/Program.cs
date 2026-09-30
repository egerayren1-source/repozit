using System;
class Program
{
    static void Main()
    {
        // ввод исходных строк
        Console.Write("введите первую строку: ");
        string str1 = Console.ReadLine();

        Console.Write("введите вторую строку: ");
        string str2 = Console.ReadLine();

        // проверка на вхождение подстроки
        if (str1.Contains(str2))
        {
            Console.WriteLine("вторая строка является подстрокой первой");
        }
        else
        {
            Console.WriteLine("вторая строка не является подстрокой первой");
        }
    }
}