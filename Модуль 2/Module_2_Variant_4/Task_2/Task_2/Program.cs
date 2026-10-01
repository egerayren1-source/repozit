using System;
using System.Collections.Generic;

namespace Task2
{
    public class Book
    {
        public string Title { get; set; }  // название книги
        public string Author { get; set; } // автор книги
        public int Year { get; set; }      // год издания

        public Book(string title, string author, int year) // конструктор книги
        {
            Title = title ?? "";
            Author = author ?? "";
            Year = year;
        }

        public override string ToString() => $"«{Title}», автор: {Author}, год: {Year}"; // строковое представление книги
    }

    public class Library
    {
        private List<Book> books = new List<Book>(); // список книг (коллекция)

        public void AddBook(Book book) // добавление книги
        {
            books.Add(book);
            Console.WriteLine("книга добавлена");
        }

        public void RemoveBook(string title) // удаление книги по названию
        {
            Book? book = books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)); // поиск без учета регистра
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("книга удалена");
            }
            else Console.WriteLine("книга не найдена");
        }

        public void FindByAuthor(string author) // поиск книг по автору
        {
            Console.WriteLine($"\n--- поиск по автору \"{author}\" ---");
            bool found = false;
            foreach (var b in books)
            {
                if (b.Author.IndexOf(author, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine(b);
                    found = true;
                }
            }
            if (!found) Console.WriteLine("ничего не найдено");
        }

        public void FindByYear(int year) // поиск книг по году
        {
            Console.WriteLine($"\n--- книги {year} года ---");
            bool found = false;
            foreach (var b in books)
            {
                if (b.Year == year)
                {
                    Console.WriteLine(b);
                    found = true;
                }
            }
            if (!found) Console.WriteLine("ничего не найдено");
        }

        public void SortByTitle() // сортировка по названию
        {
            books.Sort((a, b) => a.Title.CompareTo(b.Title));
            Console.WriteLine("отсортировано по названию");
        }

        public void SortByYear() // сортировка по году
        {
            books.Sort((a, b) => a.Year.CompareTo(b.Year));
            Console.WriteLine("отсортировано по году");
        }

        public void PrintAll() // вывод всех книг
        {
            Console.WriteLine("\n--- список всех книг ---");
            if (books.Count == 0) { Console.WriteLine("библиотека пуста"); return; }
            for (int i = 0; i < books.Count; i++) Console.WriteLine($"{i + 1}. {books[i]}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();

            lib.AddBook(new Book("Война и мир", "Лев Толстой", 1869)); // добавление тестовых книг
            lib.AddBook(new Book("Преступление и наказание", "Федор Достоевский", 1866));

            while (true) // главное меню
            {
                Console.WriteLine("\n--- меню ---\n1 - все книги\n2 - добавить\n3 - удалить\n4 - по автору\n5 - по году\n6 - сорт. по названию\n7 - сорт. по году\n0 - выход");
                Console.Write("выбор: ");
                string choice = Console.ReadLine() ?? "";

                if (choice == "1") lib.PrintAll();
                else if (choice == "2")
                {
                    Console.Write("название: "); string t = Console.ReadLine() ?? "";
                    Console.Write("автор: "); string a = Console.ReadLine() ?? "";
                    Console.Write("год: "); int.TryParse(Console.ReadLine(), out int y);
                    lib.AddBook(new Book(t, a, y));
                }
                else if (choice == "3")
                {
                    Console.Write("название для удаления: ");
                    lib.RemoveBook(Console.ReadLine() ?? "");
                }
                else if (choice == "4")
                {
                    Console.Write("автор: ");
                    lib.FindByAuthor(Console.ReadLine() ?? "");
                }
                else if (choice == "5")
                {
                    Console.Write("год: ");
                    int.TryParse(Console.ReadLine(), out int y);
                    lib.FindByYear(y);
                }
                else if (choice == "6") { lib.SortByTitle(); lib.PrintAll(); }
                else if (choice == "7") { lib.SortByYear(); lib.PrintAll(); }
                else if (choice == "0") break;
            }
        }
    }
}