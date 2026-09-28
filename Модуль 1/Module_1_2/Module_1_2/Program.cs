using System;
using System.Linq;

namespace Module_1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // главный цикл программы для выбора задач
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Модуль 1.2");
                Console.WriteLine("1. Нормировка массива");
                Console.WriteLine("2. Замена первого максимального элемента");
                Console.WriteLine("3. Вывод первых K простых чисел");
                Console.WriteLine("4. Элементы между min и max");
                Console.WriteLine("5. Фильтрация согласных букв");
                Console.WriteLine("6. Формирование массива sorted-индексов");
                Console.WriteLine("0. Выход");

                // ввод номера задачи от 0 до 6 с проверкой
                int choice = ReadInt("Выберите номер задачи (0-6): ", 0, 6);
                if (choice == 0) break;

                Console.Clear();
                switch (choice)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                    case 4: Task4(); break;
                    case 5: Task5(); break;
                    case 6: Task6(); break;
                }
            }
        }

        // задача 1: делим все элементы массива на максимальный по модулю элемент
        static void Task1()
        {
            Console.WriteLine("Задача 1: Нормировка массива");
            int n = ReadInt("Введите размер массива N (N > 0): ", 1, int.MaxValue);
            double[] array = new double[n];

            // заполняем массив числами с клавиатуры
            for (int i = 0; i < n; i++)
            {
                array[i] = ReadDouble($"Элемент [{i}]: ");
            }

            // находим самый большой по модулю элемент
            double maxAbs = array.Select(Math.Abs).Max();

            // если максимум равен нулю, нормировать нельзя
            if (maxAbs == 0)
            {
                Console.WriteLine("Все элементы равны 0. Нормировка невозможна.");
                return;
            }

            // делим каждый элемент на найденный максимум и выводим
            Console.WriteLine("\nНормированный массив:");
            for (int i = 0; i < n; i++)
            {
                array[i] /= maxAbs;
                Console.Write($"{array[i]:F4} ");
            }
            Console.WriteLine();
        }

        // задача 2: заменяем первый найденный максимум на новое число
        static void Task2()
        {
            Console.WriteLine("Задача 2: Замена максимального");
            Random rnd = new Random();
            int[] array = new int[10];

            // заполняем массив случайными числами
            for (int i = 0; i < array.Length; i++)
                array[i] = rnd.Next(-50, 51);

            Console.WriteLine("Исходный массив:\n" + string.Join(" ", array));

            int newValue = ReadInt("\nВведите значение для замены: ");

            // ищем индекс первого максимального элемента и заменяем его
            int maxIdx = Array.IndexOf(array, array.Max());
            array[maxIdx] = newValue;

            Console.WriteLine("\nИзмененный массив:\n" + string.Join(" ", array));
        }

        // задача 3: генерируем и выводим k простых чисел по 10 штук в строке
        static void Task3()
        {
            Console.WriteLine("Задача 3: K простых чисел");
            int k = ReadInt("Введите количество простых чисел K: ", 1, int.MaxValue);

            int count = 0; // счетчик найденных простых чисел
            int num = 2;    // проверяемое число

            Console.WriteLine($"\nПервые {k} простых чисел:");
            while (count < k)
            {
                // если число простое, выводим его
                if (IsPrime(num))
                {
                    Console.Write($"{num}\t");
                    count++;

                    // делаем перенос строки каждые 10 чисел
                    if (count % 10 == 0)
                        Console.WriteLine();
                }
                num++;
            }
            Console.WriteLine();
        }

        // задача 4: выводим подмассив от минимального до максимального элемента
        static void Task4()
        {
            Console.WriteLine("Задача 4: Элементы между min и max");
            int k = ReadInt("Введите размер массива K: ", 1, int.MaxValue);
            int a = ReadInt("Введите левую границу A: ");
            int b = ReadInt("Введите правую границу B (B > A): ", a + 1, int.MaxValue);

            Random rnd = new Random();
            int[] array = new int[k];

            // заполняем случайными числами в диапазоне от a до b
            for (int i = 0; i < k; i++)
                array[i] = rnd.Next(a, b + 1);

            Console.WriteLine("\nСгенерированный массив:\n" + string.Join(" ", array));

            // находим индексы минимума и максимума
            int minIdx = Array.IndexOf(array, array.Min());
            int maxIdx = Array.IndexOf(array, array.Max());

            // определяем левую и правую границу для цикла
            int start = Math.Min(minIdx, maxIdx);
            int end = Math.Max(minIdx, maxIdx);

            Console.WriteLine($"\nИндекс min: {minIdx}, индекс max: {maxIdx}");
            Console.Write("Элементы между ними (включая границы): ");

            // выводим все элементы от меньшего индекса до большего
            for (int i = start; i <= end; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }

        // задача 5: выбираем из массива символов только согласные буквы
        static void Task5()
        {
            Console.WriteLine("Задача 5: Согласные буквы");
            int k = ReadInt("Введите размер массива K: ", 1, int.MaxValue);

            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string vowels = "аеёиоуыэюя";

            Random rnd = new Random();
            char[] source = new char[k];

            // генерируем случайный массив символов
            for (int i = 0; i < k; i++)
                source[i] = alphabet[rnd.Next(alphabet.Length)];

            // оставляем только те буквы, которых нет в строке гласных
            char[] consonants = source.Where(c => !vowels.Contains(c)).ToArray();

            Console.WriteLine("\nИсходный массив символов:\n" + string.Join(" ", source));
            Console.WriteLine("\nМассив согласных букв:\n" +
                (consonants.Length > 0 ? string.Join(" ", consonants) : "Согласных нет"));
        }

        // задача 6: создаем массив индексов, сортирующий исходный массив
        static void Task6()
        {
            Console.WriteLine("Задача 6: Массив индексов");
            Random rnd = new Random();
            double[] array = new double[10];

            // заполняем случайными вещественными числами
            for (int i = 0; i < array.Length; i++)
                array[i] = -10.0 + rnd.NextDouble() * 20.0;

            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i]:F2}\t");
            Console.WriteLine();

            // сортируем индексы от 0 до n по возрастанию значений элементов
            int[] indices = Enumerable.Range(0, array.Length)
                                      .OrderBy(i => array[i])
                                      .ToArray();

            Console.WriteLine("\nМассив индексов (по возрастанию элементов):");
            Console.WriteLine(string.Join(" ", indices));

            // выводим элементы по отсортированным индексам для проверки
            Console.WriteLine("\nПроверка (элементы по новым индексам):");
            foreach (int idx in indices)
                Console.Write($"{array[idx]:F2}\t");
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

        // ввод целого числа с проверкой на тип и диапазон
        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= min && result <= max)
                    return result;
                Console.WriteLine("Ошибка! Введите корректное целое число.");
            }
        }

        // ввод вещественного числа с проверкой
        static double ReadDouble(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out result))
                    return result;
                Console.WriteLine("Ошибка! Введите число.");
            }
        }
    }
}