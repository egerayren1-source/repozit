using System;

namespace Task5
{
    public delegate void SortStrategy(int[] array); // делегат для инкапсуляции алгоритмов сортировки целочисленного массива

    public static class ArraySorter
    {
        public static void BubbleSort(int[] array) // реализация алгоритма пузырьковой сортировки
        {
            for (int i = 0; i < array.Length - 1; i++) // внешний цикл количества проходов
                for (int j = 0; j < array.Length - i - 1; j++) // внутренний цикл сравнения соседних пар
                    if (array[j] > array[j + 1]) // проверка нарушения порядка возрастания
                        (array[j], array[j + 1]) = (array[j + 1], array[j]); // обмен значений элементов через кортеж
        }

        public static void QuickSort(int[] array) => QuickSortAlg(array, 0, array.Length - 1); // точечный запуск рекурсивной быстрой сортировки

        private static void QuickSortAlg(int[] array, int low, int high) // рекурсивный метод быстрой сортировки (QuickSort)
        {
            if (low >= high) return; // базовый случай остановки рекурсии при подмассиве из одного элемента
            int pivot = array[high], i = low - 1; // выбор опорного элемента pivot и инициализация индекса меньших элементов i
            for (int j = low; j < high; j++) // цикл разделения подмассива относительно pivot
            {
                if (array[j] <= pivot) // проверка элемента на превышение опорного
                {
                    i++; // сдвиг границы меньших элементов
                    (array[i], array[j]) = (array[j], array[i]); // перестановка элемента в левую часть
                }
            }
            (array[i + 1], array[high]) = (array[high], array[i + 1]); // помещение опорного элемента pivot на его итоговую позицию
            QuickSortAlg(array, low, i); // рекурсивная сортировка левого подмассива
            QuickSortAlg(array, i + 2, high); // рекурсивная сортировка правого подмассива
        }
    }

    internal class Program
    {
        static void Main()
        {
            int[] originalArray = { 42, 12, 89, 7, 33, 21, 56 }; // базовый неотсортированный массив чисел
            bool exit = false; // флаг управления главным циклом выполнения программы

            while (!exit) // цикл повторения работы программы до команды выхода
            {
                Console.WriteLine("\nПрограмма сортировки массива чисел"); // вывод названия программы
                Console.WriteLine("Назначение: демонстрация алгоритмов сортировки с применением делегатов."); // описание назначения программы

                int[] workingArray = (int[])originalArray.Clone(); // создание копии массива для сохранения исходных данных
                Console.WriteLine($"\nИсходный массив: {string.Join(", ", originalArray)}"); // вывод элементов исходного массива

                Console.WriteLine("\nКраткое описание методов:"); // заголовок блока пояснений
                Console.WriteLine(" - Пузырьковая: последовательно сравнивает соседние пары (простая, но медленная на больших объемах)."); // описание пузырьковой сортировки
                Console.WriteLine(" - Быстрая: делит массив на части относительно опорного элемента (быстрая и эффективная)."); // описание быстрой сортировки

                Console.WriteLine("\nВыберите алгоритм сортировки:"); // вывод меню выбора алгоритма
                Console.WriteLine("1 - Пузырьковая сортировка (Bubble Sort)"); // вариант простой пузырьковой сортировки
                Console.WriteLine("2 - Быстрая сортировка (Quick Sort)"); // вариант быстрой рекурсивной сортировки
                Console.Write("Ваш выбор (1 или 2): "); // запрос ввода пользователя

                string choice = Console.ReadLine(); // ввод пользователем алгоритма для обработки

                SortStrategy sorter = (choice == "2") ? ArraySorter.QuickSort : ArraySorter.BubbleSort; // привязка соответствующего метода к делегату sorter
                sorter(workingArray); // сортировка рабочей копии массива путем вызова делегата sorter

                Console.WriteLine($"\nРезультат ({((choice == "2") ? "Быстрая" : "Пузырьковая")} сортировка):"); // заголовок результатов
                Console.WriteLine($"Отсортированный массив: {string.Join(", ", workingArray)}"); // вывод итогового отсортированного массива

                Console.Write("\nЖелаете выполнить повторную сортировку? (1 - Да, 0 - Выйти): "); // запрос на продолжение работы
                if (Console.ReadLine() == "0") exit = true; // установка флага выхода при выборе нуля
            }
        }
    }
}