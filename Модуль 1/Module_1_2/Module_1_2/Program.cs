using System;

namespace Module_1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- Модуль 1.2 ---");
                Console.WriteLine("1. Задача 1 (Нормировка массива)");
                Console.WriteLine("2. Задача 2 (Замена максимального)");
                Console.WriteLine("3. Задача 3 (Первые K простых чисел)");
                Console.WriteLine("4. Задача 4 (Элементы между min и max)");
                Console.WriteLine("5. Задача 5 (Фильтр согласных букв)");
                Console.WriteLine("6. Задача 6 (Сортировка индексов)");
                Console.WriteLine("0. Выход");

                int choice = ReadInt("Выберите номер задачи: ", 0, 6);
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

        // задача 1: нормировка массива делением на макс. по модулю
        static void Task1()
        {
            int n = ReadInt("Введите размер массива N (N > 0): ", 1, int.MaxValue);
            double[] array = new double[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = ReadDouble($"Введите элемент [{i}]: ");
            }

            // поиск максимального по модулю элемента
            double maxAbs = Math.Abs(array[0]);
            for (int i = 1; i < n; i++)
            {
                if (Math.Abs(array[i]) > maxAbs)
                {
                    maxAbs = Math.Abs(array[i]);
                }
            }

            if (maxAbs == 0)
            {
                Console.WriteLine("Все элементы равны 0. Нормировка невозможна.");
                return;
            }

            Console.WriteLine("\nНормированный массив:");
            for (int i = 0; i < n; i++)
            {
                array[i] /= maxAbs;
                Console.Write($"{array[i]:F4} ");
            }
            Console.WriteLine();
        }

        // задача 2: замена максимального элемента заданным числом
        static void Task2()
        {
            int[] array = new int[10];
            Random rnd = new Random();

            Console.WriteLine("Исходный массив (из 10 элементов):");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(-50, 51);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();

            int newValue = ReadInt("\nВведите целое число для замены: ");

            // поиск индекса первого максимального элемента
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }

            array[maxIndex] = newValue;

            Console.WriteLine("\nИзмененный массив:");
            foreach (int item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        // задача 3: вывод K простых чисел по 10 в строке
        static void Task3()
        {
            int k = ReadInt("Введите количество простых чисел K (K > 0): ", 1, int.MaxValue);
            int count = 0;
            int number = 2; // первое простое число

            Console.WriteLine($"\nПервые {k} простых чисел:");
            while (count < k)
            {
                if (IsPrime(number))
                {
                    Console.Write($"{number}\t");
                    count++;

                    // перевод строки каждые 10 чисел
                    if (count % 10 == 0)
                    {
                        Console.WriteLine();
                    }
                }
                number++;
            }
            Console.WriteLine();
        }

        // задача 4: элементы между min и max (включая их)
        static void Task4()
        {
            int k = ReadInt("Введите размер массива K (K > 0): ", 1, int.MaxValue);
            int a = ReadInt("Введите левую границу A: ");
            int b = ReadInt("Введите правую границу B (B > A): ", a + 1, int.MaxValue);

            int[] array = new int[k];
            Random rnd = new Random();

            Console.WriteLine("\nСгенерированный массив:");
            for (int i = 0; i < k; i++)
            {
                array[i] = rnd.Next(a, b);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();

            // поиск индексов минимального и максимального элементов
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < k; i++)
            {
                if (array[i] < array[minIndex]) minIndex = i;
                if (array[i] > array[maxIndex]) maxIndex = i;
            }

            int start = Math.Min(minIndex, maxIndex);
            int end = Math.Max(minIndex, maxIndex);

            Console.WriteLine($"\nИндекс min: {minIndex}, индекс max: {maxIndex}");
            Console.WriteLine("Элементы между ними (включая границы):");
            for (int i = start; i <= end; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }

        // задача 5: фильтрация русских согласных букв
        static void Task5()
        {
            int k = ReadInt("Введите размер массива K (K > 0): ", 1, int.MaxValue);

            string vowels = "аеёиоуыэюя";
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

            char[] source = new char[k];
            char[] consonantsTemp = new char[k];
            int count = 0;

            Random rnd = new Random();

            // генерация букв и выборка согласных
            for (int i = 0; i < k; i++)
            {
                source[i] = alphabet[rnd.Next(alphabet.Length)];

                // проверка, что символ - буква и не гласная
                if (char.IsLetter(source[i]) && !vowels.Contains(source[i]))
                {
                    consonantsTemp[count] = source[i];
                    count++;
                }
            }

            // формирование итогового массива точного размера
            char[] consonants = new char[count];
            Array.Copy(consonantsTemp, consonants, count);

            Console.WriteLine("\nИсходный массив:");
            Console.WriteLine(string.Join(" ", source));

            Console.WriteLine("\nМассив согласных букв:");
            Console.WriteLine(count > 0 ? string.Join(" ", consonants) : "Согласные не найдены");
        }

        // задача 6: массив индексов, сортирующий исходный массив по возрастанию
        static void Task6()
        {
            double[] array = new double[10];
            int[] indices = new int[10];
            Random rnd = new Random();

            Console.WriteLine("Исходный вещественный массив [-10, 10):");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = -10.0 + rnd.NextDouble() * 20.0;
                indices[i] = i; // начальная инициализация индексов
                Console.Write($"{array[i]:F2}\t");
            }
            Console.WriteLine();

            // сортировка массива индексов на основе значений исходного массива
            for (int i = 0; i < indices.Length - 1; i++)
            {
                for (int j = i + 1; j < indices.Length; j++)
                {
                    if (array[indices[i]] > array[indices[j]])
                    {
                        int temp = indices[i];
                        indices[i] = indices[j];
                        indices[j] = temp;
                    }
                }
            }

            Console.WriteLine("\nМассив индексов в порядке возрастания значений:");
            Console.WriteLine(string.Join(" ", indices));

            Console.WriteLine("\nПроверка (элементы по новым индексам):");
            foreach (int idx in indices)
            {
                Console.Write($"{array[idx]:F2}\t");
            }
            Console.WriteLine();
        }

        // проверка числа на простоту
        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        // безопасный ввод целых чисел
        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }

        // безопасный ввод вещественных чисел
        static double ReadDouble(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out result))
                {
                    return result;
                }
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }
    }
}