using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Module9_Task1
{
    // Описание структуры данных студента
    class Student
    {
        // Инициализация полей значениями по умолчанию
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public double Grade { get; set; }

        public override string ToString() => $"Имя: {Name} | Группа: {Group} | Балл: {Grade}";
    }

    class Program
    {
        static List<Student> students = new List<Student>();
        const string path = "students.txt";

        static void Main()
        {
            // Загрузка данных из файла при запуске приложения
            LoadFromFile();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ СТУДЕНТАМИ ===");
                Console.WriteLine("1. Показать всех");
                Console.WriteLine("2. Добавить студента");
                Console.WriteLine("3. Удалить студента");
                Console.WriteLine("4. Поиск по имени");
                Console.WriteLine("5. Сортировка по баллу");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите пункт: ");

                string choice = Console.ReadLine() ?? string.Empty;
                Console.WriteLine();

                // Обработка исключений при вводе и выполнении операций
                try
                {
                    if (choice == "1")
                    {
                        if (students.Count == 0) Console.WriteLine("Список пуст.");
                        else foreach (var s in students) Console.WriteLine(s);
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Введите имя: ");
                        string name = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Имя не может быть пустым!");

                        Console.Write("Введите группу: ");
                        string group = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(group)) throw new Exception("Группа не может быть пустой!");

                        Console.Write("Введите балл: ");
                        if (!double.TryParse(Console.ReadLine(), out double grade)) throw new Exception("Неверный формат балла!");

                        students.Add(new Student { Name = name.Trim(), Group = group.Trim(), Grade = grade });
                        SaveToFile();
                        Console.WriteLine("Студент добавлен!");
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введите имя студента для удаления: ");
                        string name = (Console.ReadLine() ?? string.Empty).Trim();
                        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Имя для удаления не может быть пустым!");

                        var target = students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                        if (target != null)
                        {
                            students.Remove(target);
                            SaveToFile();
                            Console.WriteLine("Студент удален!");
                        }
                        else { Console.WriteLine("Студент не найден."); }
                    }
                    else if (choice == "4")
                    {
                        Console.Write("Введите имя для поиска: ");
                        string query = (Console.ReadLine() ?? string.Empty).Trim();
                        if (string.IsNullOrWhiteSpace(query)) throw new Exception("Строка поиска не может быть пустой!");

                        var result = students.Where(s => !string.IsNullOrEmpty(s.Name) && s.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
                        int count = 0;
                        foreach (var s in result) { Console.WriteLine(s); count++; }
                        if (count == 0) Console.WriteLine("Ничего не найдено.");
                    }
                    else if (choice == "5")
                    {
                        students = students.OrderByDescending(s => s.Grade).ToList();
                        Console.WriteLine("Отсортировано по убыванию балла!");
                    }
                    else if (choice == "0")
                    {
                        break;
                    }
                    else if (!string.IsNullOrWhiteSpace(choice))
                    {
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                    }
                }
                catch (Exception ex)
                {
                    // Вывод сообщения об ошибке
                    Console.WriteLine($"\nОШИБКА: {ex.Message}");
                }

                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        // Сохранение списка в текстовый файл
        static void SaveToFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    foreach (var s in students)
                        sw.WriteLine($"{s.Name},{s.Group},{s.Grade}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи файла: {ex.Message}");
            }
        }

        // Чтение данных из текстового файла
        static void LoadFromFile()
        {
            if (!File.Exists(path)) return;
            try
            {
                students.Clear();
                string[] lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split(',');
                    if (parts.Length == 3 && double.TryParse(parts[2], out double grade))
                    {
                        students.Add(new Student { Name = parts[0].Trim(), Group = parts[1].Trim(), Grade = grade });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
            }
        }
    }
}