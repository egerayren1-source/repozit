using System;
class Person
{
    // закрытые поля
    private string name;
    private int age;
    private string address;

    // конструктор по умолчанию
    public Person()
    {
        name = "неизвестно";
        age = 0;
        address = "не указан";
    }

    // конструктор с параметрами
    public Person(string name, int age, string address)
    {
        SetName(name);
        SetAge(age);
        SetAddress(address);
    }

    // методы установки значений (сеттеры)
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetAge(int age)
    {
        if (age >= 0)
        {
            this.age = age;
        }
        else
        {
            Console.WriteLine("ошибка: возраст не может быть отрицательным!");
        }
    }

    public void SetAddress(string address)
    {
        this.address = address;
    }

    // методы получения значений (геттеры)
    public string GetName()
    {
        return name;
    }

    public int GetAge()
    {
        return age;
    }

    public string GetAddress()
    {
        return address;
    }

    // метод вывода информации о человеке
    public void PrintInfo()
    {
        Console.WriteLine($"имя: {name}, возраст: {age}, адрес: {address}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("задача 1: класс person\n");

        // создание первого объекта через конструктор с параметрами
        Person person1 = new Person("Филипков Захар", 17, "пр-д. Алексея Гаранина, а всё...");

        // создание второго объекта и установка значений через сеттеры
        Person person2 = new Person();
        person2.SetName("Евгения Нюберг");
        person2.SetAge(19);
        person2.SetAddress("ул. Ленина, д. 25");

        // вывод информации с помощью метода класса
        Console.WriteLine("информация о созданных объектах:");
        person1.PrintInfo();
        person2.PrintInfo();

        // демонстрация работы геттеров
        Console.WriteLine($"\nпроверка геттера для person1: {person1.GetName()} проживает по адресу {person1.GetAddress()}");
    }
}