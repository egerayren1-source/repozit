using System;

// интерфейс для объектов, которые можно отрисовать
interface IDrawable
{
    void Draw();
}

// класс круг, реализующий IDrawable
class Circle : IDrawable
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public void Draw()
    {
        Console.WriteLine($"рисуем круг с радиусом {radius}");
    }
}

// класс прямоугольник, реализующий IDrawable
class Rectangle : IDrawable
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public void Draw()
    {
        Console.WriteLine($"рисуем прямоугольник со сторонами {width} x {height}");
    }
}

// класс треугольник, реализующий IDrawable
class Triangle : IDrawable
{
    private double sideA;
    private double sideB;
    private double sideC;

    public Triangle(double a, double b, double c)
    {
        sideA = a;
        sideB = b;
        sideC = c;
    }

    public void Draw()
    {
        Console.WriteLine($"рисуем треугольник со сторонами {sideA}, {sideB}, {sideC}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("задача 4: интерфейс idrawable\n");

        // массив объектов, реализующих интерфейс IDrawable
        IDrawable[] drawables = new IDrawable[]
        {
            new Circle(2.4),
            new Rectangle(1.0, 8.0),
            new Triangle(7.0, 3.0, 5.0),
            new Circle(6.0)
        };

        Console.WriteLine("вызов метода draw() для всех объектов массива:");
        for (int i = 0; i < drawables.Length; i++)
        {
            Console.Write($"объект {i + 1}: ");
            drawables[i].Draw();
        }
    }
}