using System;

// базовый класс геометрической фигуры
class Shape
{
    // виртуальные методы площади и периметра
    public virtual double Area()
    {
        return 0;
    }

    public virtual double Perimeter()
    {
        return 0;
    }
}

// производный класс круг
class Circle : Shape
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    // переопределение метода площади
    public override double Area()
    {
        return Math.PI * radius * radius;
    }

    // переопределение метода периметра (длины окружности)
    public override double Perimeter()
    {
        return 2 * Math.PI * radius;
    }
}

// производный класс прямоугольник
class Rectangle : Shape
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    // переопределение метода площади
    public override double Area()
    {
        return width * height;
    }

    // переопределение метода периметра
    public override double Perimeter()
    {
        return 2 * (width + height);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("задача 2: наследование и полиморфизм\n");

        // создание объектов фигур
        Circle circle = new Circle(3.0);
        Rectangle rectangle = new Rectangle(7.0, 9.0);

        Console.WriteLine("круг (радиус = 3)");
        Console.WriteLine($"площадь: {circle.Area():F2}");
        Console.WriteLine($"периметр: {circle.Perimeter():F2}\n");

        Console.WriteLine("прямоугольник (7 x 9)");
        Console.WriteLine($"площадь: {rectangle.Area():F2}");
        Console.WriteLine($"периметр: {rectangle.Perimeter():F2}\n");

        // демонстрация полиморфизма через массив базового класса
        Shape[] shapes = new Shape[] { circle, rectangle };
        Console.WriteLine("демонстрация полиморфизма через массив shape[]");
        for (int i = 0; i < shapes.Length; i++)
        {
            Console.WriteLine($"фигура #{i + 1}: площадь = {shapes[i].Area():F2}, периметр = {shapes[i].Perimeter():F2}");
        }
    }
}