using System;

namespace Task_5
{
    public interface IDrawableCanvas // интерфейс холста
    {
        void DrawLine(int x1, int y1, int x2, int y2); // рисование линии
        void DrawCircle(int x, int y, int radius); // рисование круга
        void DrawRectangle(int x, int y, int width, int height); // рисование прямоугольника
    }

    public class ConsoleCanvas : IDrawableCanvas
    {
        public void DrawLine(int x1, int y1, int x2, int y2) // вывод линии
        {
            Console.WriteLine($"[Холст] Нарисована линия от точек ({x1}, {y1}) до ({x2}, {y2})."); // текстовый результат
        }

        public void DrawCircle(int x, int y, int radius) // вывод окружности
        {
            Console.WriteLine($"[Холст] Нарисована окружность с центром в ({x}, {y}) и радиусом {radius}."); // текстовый результат
        }

        public void DrawRectangle(int x, int y, int width, int height) // вывод прямоугольника
        {
            Console.WriteLine($"[Холст] Нарисован прямоугольник из точки ({x}, {y}) размером {width}x{height}."); // текстовый результат
        }
    }

    internal class Program
    {
        static void Main()
        {
            IDrawableCanvas canvas = new ConsoleCanvas(); // работа с холстом через интерфейс
            bool exit = false; // флаг цикла

            while (!exit) // цикл редактора
            {
                Console.WriteLine("\nГрафический редактор (Холст)");
                Console.WriteLine("1 - Нарисовать линию");
                Console.WriteLine("2 - Нарисовать круг");
                Console.WriteLine("3 - Нарисовать прямоугольник");
                Console.Write("Выберите фигуру для рисования (1-3): ");

                string choice = Console.ReadLine() ?? ""; // чтение выбора

                if (choice == "1")
                {
                    Console.Write("Введите X1: "); int.TryParse(Console.ReadLine(), out int x1); // координата X1
                    Console.Write("Введите Y1: "); int.TryParse(Console.ReadLine(), out int y1); // координата Y1
                    Console.Write("Введите X2: "); int.TryParse(Console.ReadLine(), out int x2); // координата X2
                    Console.Write("Введите Y2: "); int.TryParse(Console.ReadLine(), out int y2); // координата Y2
                    canvas.DrawLine(x1, y1, x2, y2); // вызов отрисовки линии
                }
                else if (choice == "2")
                {
                    Console.Write("Введите X центра: "); int.TryParse(Console.ReadLine(), out int x); // центр X
                    Console.Write("Введите Y центра: "); int.TryParse(Console.ReadLine(), out int y); // центр Y
                    Console.Write("Введите радиус: "); int.TryParse(Console.ReadLine(), out int r); // радиус
                    canvas.DrawCircle(x, y, r); // вызов отрисовки круга
                }
                else if (choice == "3")
                {
                    Console.Write("Введите X угла: "); int.TryParse(Console.ReadLine(), out int x); // угол X
                    Console.Write("Введите Y угла: "); int.TryParse(Console.ReadLine(), out int y); // угол Y
                    Console.Write("Введите ширину: "); int.TryParse(Console.ReadLine(), out int w); // ширина
                    Console.Write("Введите высоту: "); int.TryParse(Console.ReadLine(), out int h); // высота
                    canvas.DrawRectangle(x, y, w, h); // вызов отрисовки прямоугольника
                }

                Console.Write("\nЖелаете нарисовать еще что-то? (1 - Да, 0 - Выйти): ");
                if ((Console.ReadLine() ?? "") == "0") exit = true; // завершение работы
            }
        }
    }
}