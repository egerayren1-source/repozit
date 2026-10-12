using System;
using System.Collections.Generic;

namespace Task_4
{
    public interface IBook // интерфейс книги
    {
        string Title { get; } // название
        string Author { get; } // автор
        bool IsAvailable { get; } // статус доступности
        bool IssueBook(); // процедура выдачи
        void ReturnBook(); // процедура возврата
    }

    public class PaperBook : IBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; private set; } // статус печатной книги

        public PaperBook(string title, string author) // конструктор
        {
            Title = title;
            Author = author;
            IsAvailable = true; // изначально доступна
        }

        public bool IssueBook() // выдача печатной книги
        {
            if (!IsAvailable) return false; // если уже выдана
            IsAvailable = false; // фиксация выдачи
            return true;
        }

        public void ReturnBook() => IsAvailable = true; // возврат книги
    }

    public class EBook : IBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable => true; // электронная книга доступна всегда

        public EBook(string title, string author) // конструктор
        {
            Title = title;
            Author = author;
        }

        public bool IssueBook() => true; // всегда выдается
        public void ReturnBook() { } // возврат не требуется
    }

    internal class Program
    {
        static void Main()
        {
            List<IBook> library = new List<IBook> // каталог книг
            {
                new PaperBook("Война и мир", "Л.Н. Толстой"), // печатное издание
                new EBook("C# 10 и .NET 6", "Марк J. Прайс") // электронное издание
            };

            bool exit = false; // флаг цикла

            while (!exit) // главный цикл
            {
                Console.WriteLine("\nЭлектронный каталог библиотеки");
                Console.WriteLine("1 - Посмотреть список книг");
                Console.WriteLine("2 - Взять книгу из библиотеки");
                Console.Write("Ваш выбор (1 или 2): ");

                string choice = Console.ReadLine() ?? ""; // чтение выбора

                if (choice == "1")
                {
                    Console.WriteLine("\nКаталог книг:");
                    for (int i = 0; i < library.Count; i++) // перебор каталога
                    {
                        var b = library[i];
                        string status = b.IsAvailable ? "Доступна" : "Выдана на руки"; // определение статуса
                        Console.WriteLine($"{i + 1}. '{b.Title}' ({b.Author}) - Статус: {status}"); // вывод информации
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Введите номер книги для выдачи: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= library.Count) // проверка индекса
                    {
                        if (library[index - 1].IssueBook()) Console.WriteLine("Книга успешно выдана!"); // успешная выдача
                        else Console.WriteLine("Извините, эта книга сейчас недоступна."); // отказ
                    }
                    else Console.WriteLine("Неверный номер книги."); // ошибка индекса
                }

                Console.Write("\nЖелаете продолжить? (1 - Да, 0 - Выйти): ");
                if ((Console.ReadLine() ?? "") == "0") exit = true; // завершение работы
            }
        }
    }
}