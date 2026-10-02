using System;

namespace Task3
{
    public delegate void TaskAction(string taskName); // делегат, принимающий имя задачи и не возвращающий значение

    public class TaskManager
    {
        public static void SendNotification(string taskName) => Console.WriteLine($"Уведомление: задача '{taskName}' отправлена"); // метод отправки уведомления
        public static void LogRecord(string taskName) => Console.WriteLine($"Журнал: задача '{taskName}' записана в лог"); // метод журналирования задачи
    }

    internal class Program
    {
        static void Main()
        {
            bool exit = false; // флаг управления главным циклом выполнения программы

            while (!exit) // цикл повторения работы программы до команды выхода
            {
                Console.Write("\nВведите название задачи: ");
                string task = Console.ReadLine(); // чтение наименования задачи из пользовательского ввода

                Console.WriteLine("Выберите действие (1 - Уведомление, 2 - Журнал): ");
                string choice = Console.ReadLine(); // чтение пользовательского выбора способа обработки

                TaskAction action = (choice == "2") ? TaskManager.LogRecord : TaskManager.SendNotification; // назначение метода делегату через тернарный оператор
                action(task); // косвенный вызов выбранного метода через переменную делегата action

                Console.WriteLine("\nЖелаете продолжить? (1 - Добавить новую задачу, 0 - Выйти): ");
                if (Console.ReadLine() == "0") exit = true; // установка флага выхода при выборе нуля
            }
        }
    }
}