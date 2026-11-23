using System;
using System.Globalization;

internal static class ConsoleHelper
{
    public static int ReadInt(string message)
    {
        int value = 0;
        bool isValid = false;
        string input = "";

        while (!isValid)
        {
            Console.Write(message);
            input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out value))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Ошибка. Введите целое число.");
            }
        }

        return value;
    }

    public static int ReadIntInRange(
        string message, int min, int max)
    {
        int value = 0;
        bool isValid = false;

        while (!isValid)
        {
            value = ReadInt(message);
            if (value >= min && value <= max)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Число должно быть от " + min +
                    " до " + max + ".");
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
                Console.WriteLine(
                    "Число должно быть больше нуля.");
            }
        }

        return value;
    }

    public static uint ReadUInt(string message)
    {
        uint value = 0;
        bool isValid = false;
        string input = "";

        while (!isValid)
        {
            Console.Write(message);
            input = Console.ReadLine() ?? "";
            if (uint.TryParse(input, out value))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Ошибка. Введите целое " +
                    "неотрицательное число.");
            }
        }

        return value;
    }

    public static uint ReadUIntInRange(
        string message, uint min, uint max)
    {
        uint value = 0;
        bool isValid = false;

        while (!isValid)
        {
            value = ReadUInt(message);
            if (value >= min && value <= max)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Число должно быть от " + min +
                    " до " + max + ".");
            }
        }

        return value;
    }

    public static double ReadDouble(string message)
    {
        double value = 0;
        bool isValid = false;
        string input = "";

        while (!isValid)
        {
            Console.Write(message);
            input = Console.ReadLine() ?? "";
            if (double.TryParse(
                    input,
                    NumberStyles.Float,
                    CultureInfo.CurrentCulture,
                    out value) ||
                double.TryParse(
                    input,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out value))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Ошибка. Введите вещественное число.");
            }
        }

        return value;
    }

    public static char ReadChar(string message)
    {
        char value = ' ';
        bool isValid = false;
        string input = "";

        while (!isValid)
        {
            Console.Write(message);
            input = Console.ReadLine() ?? "";
            if (input.Length == 1)
            {
                value = input[0];
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Введите ровно один символ.");
            }
        }

        return value;
    }

    public static string ReadString(string message)
    {
        string value = "";
        bool isValid = false;

        while (!isValid)
        {
            Console.Write(message);
            value = (Console.ReadLine() ?? "").Trim();
            if (value.Length > 0)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Строка не должна быть пустой.");
            }
        }

        return value;
    }

    public static string ReadStringMaxLength(
        string message, int maxLength)
    {
        string value = "";
        bool isValid = false;

        while (!isValid)
        {
            value = ReadString(message);
            if (value.Length <= maxLength)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Максимальная длина — " + maxLength +
                    " символов.");
            }
        }

        return value;
    }

    public static DateTime ReadBirthDate(string message)
    {
        DateTime date = DateTime.MinValue;
        bool isValid = false;
        string input = "";

        while (!isValid)
        {
            Console.Write(message);
            input = Console.ReadLine() ?? "";
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
                    Console.WriteLine(
                        "Год должен быть от 1800 до 2100.");
                }
            }
            else
            {
                Console.WriteLine(
                    "Неверная дата. " +
                    "Используйте формат ДД.ММ.ГГГГ.");
            }
        }

        return date;
    }
}