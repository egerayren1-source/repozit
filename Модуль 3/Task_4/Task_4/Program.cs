using System;
using System.Collections.Generic;

namespace Task4
{
    public record LogEntry(DateTime Date, string Text); // неизменяемая структура данных записи журнала с датой и текстом
    public delegate bool FilterPredicate(LogEntry entry); // делегат-предикат, возвращающий true при соответствии записи условию

    internal class Program
    {
        static void Main()
        {
            var logs = new List<LogEntry> // инициализация коллекции тестовых записей журнала
            {
                new LogEntry(new DateTime(2026, 10, 1), "Успешный вход"), // запись за 01.10.2026
                new LogEntry(new DateTime(2026, 10, 2), "Ошибка базы данных"), // запись с ключевым словом ошибки
                new LogEntry(new DateTime(2026, 9, 15), "Обновление системы") // запись за сентябрь 2026
            };

            bool exit = false; // флаг управления главным циклом выполнения программы

            while (!exit) // цикл повторения работы программы до команды выхода
            {
                Console.WriteLine("Программа фильтрации журнала событий"); // вывод названия программы
                Console.WriteLine("Назначение: отбор записей лога по заданным критериям."); // описание назначения программы

                Console.WriteLine("\nИсходный список всех записей в журнале:"); // вывод заголовка исходного списка
                foreach (var log in logs) Console.WriteLine($" - [{log.Date:dd.MM.yyyy}] {log.Text}"); // отображение полного списка логов

                Console.WriteLine("\nВыберите критерий фильтрации:"); // вывод меню выбора фильтра
                Console.WriteLine("1 - Показать только записи за октябрь 2026 года"); // вариант фильтра по дате
                Console.WriteLine("2 - Показать только записи, содержащие слово 'Ошибка'"); // вариант фильтра по тексту
                Console.Write("Ваш выбор (1 или 2): "); // запрос ввода пользователя

                string choice = Console.ReadLine(); // считывание выбранного варианта фильтрации

                FilterPredicate predicate = (choice == "2")
                    ? item => item.Text.Contains("Ошибка") // лямбда-выражение для поиска подстроки "Ошибка"
                    : item => item.Date.Month == 10 && item.Date.Year == 2026; // лямбда-выражение проверки совпадения месяца и года

                Console.WriteLine("\nРезультаты фильтрации:"); // заголовок отфильтрованного вывода
                foreach (var log in logs) // итерация по элементам исходной коллекции logs
                {
                    if (predicate(log)) Console.WriteLine($" -> [{log.Date:dd.MM.yyyy}] {log.Text}"); // проверка записи через делегат predicate и ее вывод
                }

                Console.Write("\nЖелаете выполнить другую фильтрацию? (1 - Да, 0 - Выйти): "); // запрос на продолжение работы
                if (Console.ReadLine() == "0") exit = true; // установка флага выхода при выборе нуля
            }
        }
    }
}