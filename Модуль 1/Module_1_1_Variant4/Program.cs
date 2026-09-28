using System;

class Program
{
    static void Main()
    {
        bool keepRunning = true;

        // Главное меню для выбора задач Варианта 4
        while (keepRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Модуль 1.1 — Вариант 4 ===");
            Console.WriteLine("1. Игра 'Угадай число' (1-100)");
            Console.WriteLine("2. Среднее арифметическое трех чисел");
            Console.WriteLine("3. Проверка подстроки в строке");
            Console.WriteLine("4. Массив из 10 чисел и сумма элементов");
            Console.WriteLine("5. Игра FizzBuzz (1-100)");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыберите номер задания: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Task1_GuessNumber();
                    break;
                case "2":
                    Task2_AverageOfThree();
                    break;
                case "3":
                    Task3_SubstringCheck();
                    break;
                case "4":
                    Task4_ArraySum();
                    break;
                case "5":
                    Task5_FizzBuzz();
                    break;
                case "0":
                    keepRunning = false;
                    continue;
                default:
                    Console.WriteLine("Неверный выбор! Нажмите Enter...");
                    break;
            }

            if (keepRunning)
            {
                Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
            }
        }
    }

    // Задание 1. Игра "Угадай число"
    static void Task1_GuessNumber()
    {
        Console.WriteLine("--- Задание 1: Угадай число от 1 до 100 ---");
        Random random = new Random();
        int targetNumber = random.Next(1, 101); // Генерируем случайное число от 1 до 100
        int userGuess = 0;
        int attempts = 0;

        Console.WriteLine("Загадано число от 1 до 100. Попробуйте угадать!");

        while (userGuess != targetNumber)
        {
            Console.Write("Введите ваш вариант: ");
            if (int.TryParse(Console.ReadLine(), out userGuess))
            {
                attempts++;
                if (userGuess < targetNumber)
                {
                    Console.WriteLine("Загаданное число БОЛЬШЕ.");
                }
                else if (userGuess > targetNumber)
                {
                    Console.WriteLine("Загаданное число МЕНЬШЕ.");
                }
                else
                {
                    Console.WriteLine($"\nПоздравляем! Вы угадали число {targetNumber} за {attempts} попыток!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода! Пожалуйста, вводите целые числа.");
            }
        }
    }

    // Задание 2. Среднее арифметическое трех чисел
    static void Task2_AverageOfThree()
    {
        Console.WriteLine("--- Задание 2: Среднее арифметическое трех чисел ---");

        Console.Write("Введите первое число: ");
        double num1 = double.Parse(Console.ReadLine());

        Console.Write("Введите второе число: ");
        double num2 = double.Parse(Console.ReadLine());

        Console.Write("Введите третье число: ");
        double num3 = double.Parse(Console.ReadLine());

        double average = (num1 + num2 + num3) / 3.0;

        Console.WriteLine($"Среднее арифметическое: {average:F2}");
    }

    // Задание 3. Проверка подстроки
    static void Task3_SubstringCheck()
    {
        Console.WriteLine("--- Задание 3: Проверка подстроки ---");

        Console.Write("Введите основную (первую) строку: ");
        string mainString = Console.ReadLine();

        Console.Write("Введите подстроку (вторую строку) для поиска: ");
        string subString = Console.ReadLine();

        // Метод Contains возвращает true, если subString содержится в mainString
        if (mainString.Contains(subString))
        {
            Console.WriteLine("Результат: Вторая строка ЯВЛЯЕТСЯ подстрокой первой.");
        }
        else
        {
            Console.WriteLine("Результат: Вторая строка НЕ является подстрокой первой.");
        }
    }

    // Задание 4. Массив из 10 чисел и их сумма
    static void Task4_ArraySum()
    {
        Console.WriteLine("--- Задание 4: Сумма элементов массива ---");

        int[] numbers = new int[10];
        Random random = new Random();
        int sum = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(1, 101); // Заполнение случайными числами
            sum += numbers[i];
        }

        Console.WriteLine("Сгенерированный массив: " + string.Join(", ", numbers));
        Console.WriteLine($"Сумма всех элементов массива: {sum}");
    }

    // Задание 5. Игра FizzBuzz
    static void Task5_FizzBuzz()
    {
        Console.WriteLine("--- Задание 5: FizzBuzz от 1 до 100 ---");

        for (int i = 1; i <= 100; i++)
        {
            // Если число делится и на 3, и на 5 одновременно
            if (i % 3 == 0 && i % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
            }
            else if (i % 3 == 0)
            {
                Console.WriteLine("Fizz");
            }
            else if (i % 5 == 0)
            {
                Console.WriteLine("Buzz");
            }
            else
            {
                Console.WriteLine(i);
            }
        }
    }
}