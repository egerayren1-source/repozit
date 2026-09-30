using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("задача 5: согласные буквы");

        Console.Write("введите размер массива K: ");
        int k = int.Parse(Console.ReadLine());

        string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string vowels = "аеёиоуыэюя";

        Random random = new Random();
        char[] source = new char[k];

        // генерация массива символов
        for (int i = 0; i < k; i++)
        {
            source[i] = alphabet[random.Next(alphabet.Length)];
        }

        Console.WriteLine("\nисходный массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write(source[i] + " ");
        }
        Console.WriteLine();

        // подсчет количества согласных
        int consonantsCount = 0;
        for (int i = 0; i < k; i++)
        {
            if (!vowels.Contains(source[i]))
            {
                consonantsCount++;
            }
        }

        // создание массива согласных без LINQ
        char[] consonants = new char[consonantsCount];
        int index = 0;

        for (int i = 0; i < k; i++)
        {
            if (!vowels.Contains(source[i]))
            {
                consonants[index] = source[i];
                index++;
            }
        }

        Console.WriteLine("\nмассив согласных букв:");
        if (consonantsCount > 0)
        {
            for (int i = 0; i < consonants.Length; i++)
            {
                Console.Write(consonants[i] + " ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("согласных букв нет.");
        }
    }
}