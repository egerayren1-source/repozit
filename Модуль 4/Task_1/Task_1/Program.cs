using System;

namespace Task_1
{
    public interface IShape // интерфейс для расчета площади и периметра
    {
        string Name { get; } // наименование фигуры
        double GetArea(); // метод вычисления площади
        double GetPerimeter(); // метод вычисления периметра
    }

    public class Circle : IShape
    {
        public double Radius { get; set; } // радиус круга
        public string Name => "Круг";

        public Circle(double radius) => Radius = radius;

        public double GetArea() => Math.PI * Radius * Radius;
        public double GetPerimeter() => 2 * Math.PI * Radius;
    }

    public class Rectangle : IShape
    {
        public double Width { get; set; } // ширина прямоугольника
        public double Height { get; set; } // высота прямоугольника
        public string Name => "Прямоугольник";

        public Rectangle(double width, double height) { Width = width; Height = height; }

        public double GetArea() => Width * Height;
        public double GetPerimeter() => 2 * (Width + Height);
    }

    public class Triangle : IShape
    {
        public double SideA { get; set; } // первая сторона
        public double SideB { get; set; } // вторая сторона
        public double SideC { get; set; } // третья сторона
        public string Name => "Треугольник";

        public Triangle(double a, double b, double c) { SideA = a; SideB = b; SideC = c; }

        public double GetArea()
        {
            double p = GetPerimeter() / 2; // полупериметр для формулы Герона
            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
        }

        public double GetPerimeter() => SideA + SideB + SideC;
    }

    internal class Program
    {
        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nРасчет параметров геометрических фигур");
                Console.WriteLine("1 - Круг");
                Console.WriteLine("2 - Прямоугольник");
                Console.WriteLine("3 - Треугольник");
                Console.Write("Выберите фигуру (1-3): ");

                string choice = Console.ReadLine();
                IShape shape = null; // полиморфная ссылка на объект интерфейса

                if (choice == "1")
                {
                    Console.Write("Введите радиус круга: ");
                    if (double.TryParse(Console.ReadLine(), out double r)) shape = new Circle(r);
                }
                else if (choice == "2")
                {
                    Console.Write("Введите ширину: ");
                    double.TryParse(Console.ReadLine(), out double w);
                    Console.Write("Введите высоту: ");
                    double.TryParse(Console.ReadLine(), out double h);
                    shape = new Rectangle(w, h);
                }
                else if (choice == "3")
                {
                    Console.Write("Введите сторону A: ");
                    double.TryParse(Console.ReadLine(), out double a);
                    Console.Write("Введите сторону B: ");
                    double.TryParse(Console.ReadLine(), out double b);
                    Console.Write("Введите сторону C: ");
                    double.TryParse(Console.ReadLine(), out double c);
                    shape = new Triangle(a, b, c);
                }

                if (shape != null)
                {
                    Console.WriteLine($"\nРезультаты для фигуры '{shape.Name}':");
                    Console.WriteLine($"Площадь: {shape.GetArea():F2}");
                    Console.WriteLine($"Периметр: {shape.GetPerimeter():F2}");
                }
                else
                {
                    Console.WriteLine("Некорректный выбор или ввод данных.");
                }

                Console.Write("\nЖелаете продолжить? (1 - Да, 0 - Выйти): ");
                if (Console.ReadLine() == "0") exit = true;
            }
        }
    }
}