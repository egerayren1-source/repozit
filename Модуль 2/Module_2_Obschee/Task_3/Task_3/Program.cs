using System;

// класс автора
class Author
{
    public string Name { get; set; }
    public int BirthYear { get; set; }

    public Author(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }

    public string GetInfo()
    {
        return $"{Name} ({BirthYear} г.р.)";
    }
}

// класс книги (содержит объект класса Author - композиция)
class Book
{
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public Author BookAuthor { get; set; }

    public Book(string title, int releaseYear, Author author)
    {
        Title = title;
        ReleaseYear = releaseYear;
        BookAuthor = author;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"книга: \"{Title}\", год издания: {ReleaseYear}, автор: {BookAuthor.GetInfo()}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("задача 3: композиция классов author и book\n");

        // создание объектов авторов
        Author author1 = new Author("лев толстой", 1828);
        Author author2 = new Author("антуан де сент-экзюпери", 1900);
        Author author3 = new Author("борис Васильев", 1924);

        // создание объектов книг
        Book book1 = new Book("война и мир", 1869, author1);
        Book book2 = new Book("маленький принц", 1943, author2);
        Book book3 = new Book("а зори здесь тихие...", 1969, author3);

        // вывод информации о книгах
        Console.WriteLine("список книг в библиотеке:");
        book1.PrintInfo();
        book2.PrintInfo();
        book3.PrintInfo();
    }
}