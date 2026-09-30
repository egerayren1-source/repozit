using System;
class Program
{
    static void Main()
    {
        // генерация случайного числа от 1 до 100
        Random rnd = new Random();
        int num = rnd.Next(1, 101);

        int vvod = 0;
        int popytki = 0;

        Console.WriteLine("угадай число от 1 до 100:");

        // цикл пока число не угадано
        while (vvod != num)
        {
            Console.Write("введите число: ");
            vvod = Convert.ToInt32(Console.ReadLine());
            popytki++;

            // проверка введенного значения
            if (vvod < num)
            {
                Console.WriteLine("загаданное число больше");
            }
            else if (vvod > num)
            {
                Console.WriteLine("загаданное число меньше");
            }
            else
            {
                Console.WriteLine($"вы угадали число за {popytki} попыток!");
            }
        }
    }
}