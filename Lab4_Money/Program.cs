using System;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        
        int choice = 0;
        
        while (true)
        {
            Console.WriteLine(
                "\nЛабораторная работа №4. " +
                "Перегрузка операций (Money)");
            Console.WriteLine(
                "Выберите задание (6-7) " +
                "или 0 для выхода:");
            Console.Write("Ваш выбор: ");
            choice = ConsoleHelper.ReadInt("");
            
            switch (choice)
            {
                case 0:
                    Console.WriteLine("Выход.");
                    return;
                case 6: RunTask6(); break;
                case 7: RunTask7(); break;
                default:
                    Console.WriteLine(
                        "Неверный номер задания.");
                    break;
            }
        }
    }

    private static void RunTask6()
    {
        Console.WriteLine(
            "\nЗадание 6: " +
            "Вычитание копеек из Money");
        
        uint rubles = ConsoleHelper.ReadUInt(
            "Рубли: ");
        
        // ВАЛИДАЦИЯ: копейки строго 0-99
        uint kopeksInput = 
            ConsoleHelper.ReadUIntInRange(
                "Копейки (0-99): ", 0, 99);
        byte kopeks = (byte)kopeksInput;
        
        Money m = new Money(rubles, kopeks);
        Console.WriteLine(
            $"Исходная сумма: {m}");
        
        uint subtractKopeks = 
            ConsoleHelper.ReadUInt(
                "Сколько копеек вычесть: ");
        Money result = m - subtractKopeks;
        Console.WriteLine(
            $"Результат: {result}");
    }

    private static void RunTask7()
    {
        Console.WriteLine(
            "\nЗадание 7: " +
            "Все операции Money");
        
        uint rubles = ConsoleHelper.ReadUInt(
            "Рубли: ");
        
        // ВАЛИДАЦИЯ: копейки строго 0-99
        uint kopeksInput = 
            ConsoleHelper.ReadUIntInRange(
                "Копейки (0-99): ", 0, 99);
        byte kopeks = (byte)kopeksInput;
        
        Money m1 = new Money(rubles, kopeks);
        Console.WriteLine($"m1 = {m1}");
        
        // Тест ++
        Money m2 = m1++;
        Console.WriteLine(
            $"m1++ = {m1} " +
            $"(до инкремента было {m2})");
        
        // Тест --
        Money m3 = m1--;
        Console.WriteLine(
            $"m1-- = {m1} " +
            $"(до декремента было {m3})");
        
        // Тест явного приведения к uint
        uint rublesOnly = (uint)m1;
        Console.WriteLine(
            $"(uint)m1 = {rublesOnly} " +
            $"(только рубли)");
        
        // Тест неявного приведения к bool
        bool isNonZero = m1;
        Console.WriteLine(
            $"bool(m1) = {isNonZero}");
        
        // Тест Money - Money
        Console.WriteLine("\nВторая сумма:");
        uint rubles2 = ConsoleHelper.ReadUInt(
            "Рубли: ");
        uint kopeksInput2 = 
            ConsoleHelper.ReadUIntInRange(
                "Копейки (0-99): ", 0, 99);
        byte kopeks2 = (byte)kopeksInput2;
        
        Money m4 = new Money(rubles2, kopeks2);
        Console.WriteLine($"m4 = {m4}");
        
        Money diff = m1 - m4;
        Console.WriteLine($"m1 - m4 = {diff}");
        
        // Тест Money - uint
        uint subKopeks = 
            ConsoleHelper.ReadUInt(
                "Вычесть копеек (uint): ");
        Money diff2 = m1 - subKopeks;
        Console.WriteLine(
            $"m1 - {subKopeks} коп. = {diff2}");
    }
}