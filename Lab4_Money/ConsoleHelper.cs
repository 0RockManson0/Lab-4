using System;

internal static class ConsoleHelper
{
    private static bool IsEmpty(string s)
    {
        bool isEmpty = true;
        int i = 0;
        
        if (s == null)
        {
            return true;
        }
        
        for (i = 0; i < s.Length; i++)
        {
            if (!char.IsWhiteSpace(s[i]))
            {
                isEmpty = false;
                break;
            }
        }
        return isEmpty;
    }

    public static int ReadInt(string prompt)
    {
        string input = "";
        int result = 0;
        bool isValid = false;
        
        while (!isValid)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            isValid = int.TryParse(input, out result);
            
            if (!isValid)
            {
                Console.WriteLine(
                    "Некорректный ввод. " +
                    "Введите целое число.");
            }
        }
        return result;
    }

    public static uint ReadUInt(string prompt)
    {
        string input = "";
        uint result = 0;
        bool isValid = false;
        
        while (!isValid)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            isValid = uint.TryParse(input, out result);
            
            if (!isValid)
            {
                Console.WriteLine(
                    "Некорректный ввод. " +
                    "Введите целое неотрицательное число.");
            }
        }
        return result;
    }

    public static uint ReadUIntInRange(
        string prompt, uint min, uint max)
    {
        uint result = 0;
        bool isValid = false;
        
        while (!isValid)
        {
            result = ReadUInt(prompt);
            if (result >= min && result <= max)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    $"Число должно быть " +
                    $"от {min} до {max}.");
            }
        }
        return result;
    }

    public static string ReadString(string prompt)
    {
        string result = "";
        bool isValid = false;
        
        while (!isValid)
        {
            Console.Write(prompt);
            result = Console.ReadLine();
            isValid = !IsEmpty(result);
            
            if (!isValid)
            {
                Console.WriteLine(
                    "Строка не может быть пустой.");
            }
        }
        return result;
    }
}