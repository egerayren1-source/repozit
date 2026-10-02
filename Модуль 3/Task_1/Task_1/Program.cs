using System;

namespace Task1
{
    public delegate double AreaCalculator(); // делегат без параметров, возвращающий площадь типу double

    public abstract class Shape
    {
        public abstract string Name { get; } // абстрактное свойство для получения названия фигуры
        public abstract double GetArea(); // абстрактный метод вычисления площади, переопределяемый в наследниках
    }

    public class Circle : Shape
    {
        public double Radius { get; set; } // радиус окружности в произвольных единицах измерения
        public override string Name => "Круг"; // переопределение наименования для объекта типа Circle
        public Circle(double radius) => Radius = radius; // конструктор инициализации радиуса круга
        public override double GetArea() => Math.PI * Radius * Radius; // расчет площади круга по формуле S = π * r²
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; } // ширина прямоугольника
        public double Height { get; set; } // высота прямоугольника
        public override string Name => "Прямоугольник"; // переопределение наименования для объекта типа Rectangle
        public Rectangle(double width, double height) { Width = width; Height = height; } // конструктор установки сторон
        public override double GetArea() => Width * Height; // расчет площади прямоугольника по формуле S = w * h
    }

    public class Triangle : Shape
    {
        public double BaseLength { get; set; } // длина основания треугольника
        public double Height { get; set; } // высота, проведенная к основанию треугольника
        public override string Name => "Треугольник"; // переопределение наименования для объекта типа Triangle
        public Triangle(double baseLength, double height) { BaseLength = baseLength; Height = height; } // конструктор параметров
        public override double GetArea() => 0.5 * BaseLength * Height; // расчет площади треугольника по формуле S = 0.5 * a * h
    }

    internal class Program
    {
        static void Main()
        {
            Shape[] shapes = { new Circle(6), new Rectangle(7, 2), new Triangle(3, 9) }; // массив объектов базового типа
            foreach (var shape in shapes) // перебор элементов полиморфного массива фигур
            {
                AreaCalculator calc = shape.GetArea; // связывание метода GetArea конкретного экземпляра с делегатом
                Console.WriteLine($"{shape.Name}: площадь = {calc():F2}"); // вызов метода через делегат calc с округлением
            }
        }
    }
}