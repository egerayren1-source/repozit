using System;
using System.Collections.Generic;

namespace Task2
{
    public interface IProduct // интерфейс описания товара
    {
        string Name { get; } // наименование товара
        double Price { get; } // цена товара
        int Quantity { get; set; } // количество на складе
        double GetTotalValue(); // расчёт общей стоимости
    }

    public class FoodProduct : IProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public FoodProduct(string name, double price, int quantity) // конструктор
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public double GetTotalValue() => Price * Quantity; // стоимость остатка еды
    }

    public class ElectronicsProduct : IProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public ElectronicsProduct(string name, double price, int quantity) // конструктор
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public double GetTotalValue() => Price * Quantity; // стоимость остатка электроники
    }

    internal class Program
    {
        static void Main()
        {
            List<IProduct> inventory = new List<IProduct> // складской каталог
            {
                new FoodProduct("Молоко", 2.50, 40), // тестовый товар 1
                new ElectronicsProduct("Телевизор", 1200.00, 5) // тестовый товар 2
            };

            bool exit = false; // флаг цикла

            while (!exit) // цикл взаимодействия
            {
                Console.WriteLine("\nУчет товаров на складе");
                Console.WriteLine("1 - Посмотреть список всех товаров");
                Console.WriteLine("2 - Добавить новый товар");
                Console.Write("Выберите действие (1 или 2): ");

                string choice = Console.ReadLine() ?? ""; // чтение выбора

                if (choice == "1")
                {
                    Console.WriteLine("\nСписок товаров на складе:");
                    foreach (var p in inventory) // перебор товаров
                    {
                        Console.WriteLine($" - {p.Name} | Цена: {p.Price} руб. | Остаток: {p.Quantity} шт. | Всего: {p.GetTotalValue()} руб."); // вывод данных
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Введите название товара: ");
                    string name = Console.ReadLine() ?? "Без названия"; // чтение наименования с защитой от null
                    Console.Write("Введите цену за единицу: ");
                    double.TryParse(Console.ReadLine(), out double price); // парсинг цены
                    Console.Write("Введите количество на складе: ");
                    int.TryParse(Console.ReadLine(), out int qty); // парсинг количества

                    Console.Write("Категория (1 - Еда, 2 - Электроника): ");
                    string cat = Console.ReadLine() ?? ""; // чтение категории

                    if (cat == "2") inventory.Add(new ElectronicsProduct(name, price, qty)); // добавление электроники
                    else inventory.Add(new FoodProduct(name, price, qty)); // добавление еды

                    Console.WriteLine("Товар успешно добавлен!");
                }

                Console.Write("\nЖелаете продолжить? (1 - Да, 0 - Выйти): ");
                if ((Console.ReadLine() ?? "") == "0") exit = true; // завершение работы
            }
        }
    }
}