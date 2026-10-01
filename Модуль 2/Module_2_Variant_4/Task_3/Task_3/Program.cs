using System;
using System.Collections.Generic;

namespace Task3
{
    public class FixedStringArray
    {
        private string[] items; // внутренний массив строк

        public int Length => items.Length; // длина массива

        public FixedStringArray(int size) // конструктор с указанием размера
        {
            items = new string[size > 0 ? size : 1];
            Array.Fill(items, ""); // заполнение пустыми строками
        }

        public string this[int index] // индексатор с проверкой границ
        {
            get => CheckIndex(index) ? items[index] : "";
            set { if (CheckIndex(index)) items[index] = value ?? ""; }
        }

        private bool CheckIndex(int index) // контроль выхода за пределы
        {
            if (index >= 0 && index < items.Length) return true;
            Console.WriteLine($"ошибка: индекс {index} выходит за границы (0..{items.Length - 1})");
            return false;
        }

        public static FixedStringArray ConcatElements(FixedStringArray a, FixedStringArray b) // поэлементное сцепление
        {
            int maxLen = Math.Max(a.Length, b.Length);
            FixedStringArray res = new FixedStringArray(maxLen);
            for (int i = 0; i < maxLen; i++) res[i] = (i < a.Length ? a[i] : "") + (i < b.Length ? b[i] : "");
            return res;
        }

        public static FixedStringArray MergeUnique(FixedStringArray a, FixedStringArray b) // слияние без повторов
        {
            List<string> list = new List<string>();
            for (int i = 0; i < a.Length; i++) if (!list.Contains(a[i])) list.Add(a[i]);
            for (int i = 0; i < b.Length; i++) if (!list.Contains(b[i])) list.Add(b[i]);
            FixedStringArray res = new FixedStringArray(list.Count);
            for (int i = 0; i < list.Count; i++) res[i] = list[i];
            return res;
        }

        public void PrintElement(int index) // вывод одного элемента по индексу
        {
            if (CheckIndex(index)) Console.WriteLine($"элемент [{index}] = \"{items[index]}\"");
        }

        public void PrintAll(string name = "массив") // вывод всего массива
        {
            Console.WriteLine($"\n--- {name} (размер: {Length}) ---");
            for (int i = 0; i < items.Length; i++) Console.WriteLine($"[{i}]: \"{items[i]}\"");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FixedStringArray arr1 = new FixedStringArray(3); // инициализация 1 массива
            arr1[0] = "Яблоко"; arr1[1] = "Банан"; arr1[2] = "Груша";

            FixedStringArray arr2 = new FixedStringArray(3); // инициализация 2 массива
            arr2[0] = "Сок"; arr2[1] = "Банан"; arr2[2] = "Джем";

            while (true) // главное меню
            {
                Console.WriteLine("\nменю\n1 - показать массивы\n2 - элемент по индексу\n3 - изменить элемент\n4 - сцепление\n5 - слияние\n0 - выход");
                Console.Write("выбор: ");
                string choice = Console.ReadLine() ?? "";

                if (choice == "1") { arr1.PrintAll("массив 1"); arr2.PrintAll("массив 2"); }
                else if (choice == "2" || choice == "3")
                {
                    Console.Write("номер массива (1 или 2): ");
                    FixedStringArray arr = Console.ReadLine() == "2" ? arr2 : arr1;

                    Console.Write("индекс: ");
                    int.TryParse(Console.ReadLine(), out int idx);

                    if (choice == "2") arr.PrintElement(idx); // выводит выбранный элемент!
                    else
                    {
                        Console.Write("новое значение: ");
                        arr[idx] = Console.ReadLine() ?? "";
                    }
                }
                else if (choice == "4") FixedStringArray.ConcatElements(arr1, arr2).PrintAll("результат сцепления");
                else if (choice == "5") FixedStringArray.MergeUnique(arr1, arr2).PrintAll("результат слияния");
                else if (choice == "0") break;
            }
        }
    }
}