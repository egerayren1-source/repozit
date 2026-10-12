using System;
using System.Collections.Generic;

namespace Task_3
{
    public interface IStudent // интерфейс профиля студента
    {
        string FullName { get; } // ФИО
        int Course { get; } // курс
        double GetAverageGrade(); // расчёт среднего балла
        string GetCourseInfo(); // информация о ступени
    }

    public class JuniorStudent : IStudent
    {
        public string FullName { get; set; }
        public int Course { get; set; }
        private double[] grades; // оценки

        public JuniorStudent(string fullName, int course, double[] grades) // инициализация
        {
            FullName = fullName;
            Course = course;
            this.grades = grades;
        }

        public double GetAverageGrade()
        {
            if (grades.Length == 0) return 0; // защита от деления на ноль
            double sum = 0;
            foreach (var g in grades) sum += g; // суммирование оценок
            return sum / grades.Length; // расчёт среднего
        }

        public string GetCourseInfo() => $"{Course} курс (Младшая ступень, базовая подготовка)"; // ступень
    }

    public class SeniorStudent : IStudent
    {
        public string FullName { get; set; }
        public int Course { get; set; }
        private double[] grades; // оценки

        public SeniorStudent(string fullName, int course, double[] grades) // инициализация
        {
            FullName = fullName;
            Course = course;
            this.grades = grades;
        }

        public double GetAverageGrade()
        {
            if (grades.Length == 0) return 0; // защита от деления на ноль
            double sum = 0;
            foreach (var g in grades) sum += g; // суммирование оценок
            return sum / grades.Length; // расчёт среднего
        }

        public string GetCourseInfo() => $"{Course} курс (Старшая ступень, специализация)"; // ступень
    }

    internal class Program
    {
        static void Main()
        {
            List<IStudent> students = new List<IStudent> // базовый список
            {
                new JuniorStudent("Филипков З.Н.", 3, new double[] { 4, 9, 7, 6 }), // 3 курс
                new SeniorStudent("Ковалёв И.И.", 7, new double[] { 9, 10, 10, 8 }) // 7 курс
            };

            bool exit = false; // флаг цикла

            while (!exit) // цикл управления
            {
                Console.WriteLine("\nСистема учета студентов");
                Console.WriteLine("1 - Посмотреть список студентов");
                Console.WriteLine("2 - Добавить студента");
                Console.Write("Ваш выбор (1 или 2): ");

                string choice = Console.ReadLine() ?? ""; // чтение выбора

                if (choice == "1")
                {
                    Console.WriteLine("\nСписок студентов университета:");
                    foreach (var s in students) // перебор студентов
                    {
                        Console.WriteLine($"Студент: {s.FullName} | {s.GetCourseInfo()} | Средний балл: {s.GetAverageGrade():F2}"); // вывод сведений
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Введите ФИО студента: ");
                    string name = Console.ReadLine() ?? "Неуказано"; // чтение ФИО с защитой от null
                    Console.Write("Введите номер курса (1-4): ");
                    int.TryParse(Console.ReadLine(), out int course); // парсинг курса

                    double[] defaultGrades = { 8, 8, 9 }; // стартовые оценки

                    if (course >= 3) students.Add(new SeniorStudent(name, course, defaultGrades)); // старший курс
                    else students.Add(new JuniorStudent(name, course, defaultGrades)); // младший курс

                    Console.WriteLine("Студент успешно зачислен!");
                }

                Console.Write("\nЖелаете продолжить? (1 - Да, 0 - Выйти): ");
                if ((Console.ReadLine() ?? "") == "0") exit = true; // завершение работы
            }
        }
    }
}