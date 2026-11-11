using System;
using System.Globalization;

internal static class ConsoleHelper
{
    public static int ReadInt(string message)
    {
        int value = 0;
        bool isValid = false;

        while (!isValid)
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out value))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Ошибка. Введите целое число.");
            }
        }

        return value;
    }

    public static int ReadPositiveInt(string message)
    {
        int value = 0;
        bool isValid = false;

        while (!isValid)
        {
            value = ReadInt(message);

            if (value > 0)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Число должно быть больше нуля.");
            }
        }

        return value;
    }

    public static string ReadString(string message)
    {
        string value = "";

        while (value.Length == 0)
        {
            Console.Write(message);
            value = (Console.ReadLine() ?? "").Trim();

            if (value.Length == 0)
            {
                Console.WriteLine("Строка не должна быть пустой.");
            }
        }

        return value;
    }

    public static string ReadStringMaxLength(
        string message, int maxLength)
    {
        string value = "";

        while (value.Length == 0 || value.Length > maxLength)
        {
            Console.Write(message);
            value = (Console.ReadLine() ?? "").Trim();

            if (value.Length == 0)
            {
                Console.WriteLine("Строка не должна быть пустой.");
            }
            else if (value.Length > maxLength)
            {
                Console.WriteLine(
                    "Максимальная длина — " + maxLength + " символов.");
            }
        }

        return value;
    }

    public static DateTime ReadBirthDate(string message)
    {
        DateTime date = DateTime.MinValue;
        bool isValid = false;

        while (!isValid)
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? "";

            if (DateTime.TryParseExact(
                input,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                if (date.Year >= 1800 && date.Year <= 2100)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Год должен быть от 1800 до 2100.");
                }
            }
            else
            {
                Console.WriteLine(
                    "Неверная дата. Используйте формат ДД.ММ.ГГГГ.");
            }
        }

        return date;
    }
}