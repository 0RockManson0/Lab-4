using System;

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
                Console.WriteLine("Введите целое число.");
            }
        }

        return value;
    }

    public static uint ReadUInt(string message)
    {
        uint value = 0;
        bool isValid = false;

        while (!isValid)
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? "";

            if (uint.TryParse(input, out value))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Введите целое неотрицательное число.");
            }
        }

        return value;
    }

    public static byte ReadKopeks(string message)
    {
        uint value = 0;

        while (true)
        {
            value = ReadUInt(message);

            if (value <= 99)
            {
                return (byte)value;
            }

            Console.WriteLine(
                "Количество копеек должно быть от 0 до 99.");
        }
    }

    public static Money ReadMoney(string title)
    {
        Console.WriteLine("\n" + title);

        uint rubles = ReadUInt("Рубли: ");
        byte kopeks = ReadKopeks("Копейки (0-99): ");

        return new Money(rubles, kopeks);
    }
}