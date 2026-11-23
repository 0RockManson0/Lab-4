using System;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        int choice = 0;
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Лабораторная работа №4. " +
                "Перегрузка операций (Money)");
            Console.WriteLine(
                "Выберите задание (6-7) или 0 для выхода:");
            choice = ConsoleHelper.ReadInt("Ваш выбор: ");

            switch (choice)
            {
                case 0:
                    isRunning = false;
                    break;
                case 6:
                    RunTask6();
                    break;
                case 7:
                    RunTask7();
                    break;
                default:
                    Console.WriteLine("Введите 6, 7 или 0.");
                    break;
            }
        }

        Console.WriteLine("Выход.");
    }
    

    private static Money ReadMoney(
        string title, uint maxKopeks)
    {
        uint rubles = 0;
        uint kopeks = 0;

        Console.WriteLine();
        Console.WriteLine(title);
        rubles = ConsoleHelper.ReadUInt("Рубли: ");
        kopeks = ConsoleHelper.ReadUIntInRange(
            "Копейки (0-" + maxKopeks + "): ",
            0, maxKopeks);
        return new Money(rubles, (byte)kopeks);
    }

    private static void PrintMoney(
        string title, Money money)
    {
        Console.WriteLine(title + ": " + money);
    }

    // Задание 6

    private static void TestConstructors()
    {
        Console.WriteLine();
        Console.WriteLine("--- Конструкторы ---");

        Money empty = new Money();
        PrintMoney("Money()", empty);

        Money custom = ReadMoney(
            "Money(uint, byte): введите копейки " +
            "до 255 для проверки нормализации", 255);
        PrintMoney("Money(uint, byte)", custom);

        Money copy = new Money(custom);
        PrintMoney("Money(Money) — копия", copy);

        Money nullCopy = new Money(null);
        PrintMoney("Money(null)", nullCopy);

        copy.Rubles = copy.Rubles + 1;
        PrintMoney("Копия изменена", copy);
        PrintMoney("Оригинал (не изменился)", custom);
    }

    private static void TestProperties()
    {
        Console.WriteLine();
        Console.WriteLine("--- Свойства ---");

        Money money = ReadMoney("Введите сумму", 99);
        PrintMoney("Исходная сумма", money);
        Console.WriteLine(
            "Rubles = " + money.Rubles +
            ", Kopeks = " + money.Kopeks);

        money.Rubles = ConsoleHelper.ReadUInt(
            "Новое значение Rubles: ");
        PrintMoney("После установки Rubles", money);

        uint kopeks = ConsoleHelper.ReadUIntInRange(
            "Новое значение Kopeks (0-255): ", 0, 255);
        money.Kopeks = (byte)kopeks;
        PrintMoney(
            "После установки Kopeks (нормализация)",
            money);
    }

    private static void TestToString()
    {
        Console.WriteLine();
        Console.WriteLine("--- ToString ---");

        Money money = ReadMoney("Введите сумму", 99);
        Console.WriteLine(
            "ToString(): " + money.ToString());
        Console.WriteLine($"Интерполяция: {money}");
    }

    private static void TestSubtractKopeks()
    {
        Console.WriteLine();
        Console.WriteLine(
            "--- Метод задания 6: SubtractKopeks ---");

        Money money = ReadMoney("Исходная сумма", 99);
        PrintMoney("Исходная сумма", money);

        uint kopeks = ConsoleHelper.ReadUInt(
            "Сколько копеек вычесть: ");
        Money result = money.SubtractKopeks(kopeks);
        PrintMoney("Результат", result);
        PrintMoney(
            "Исходная сумма (не изменилась)", money);
    }

    public static void RunTask6()
    {
        Console.WriteLine();
        Console.WriteLine(
            "Задание 6. Класс Money: вычитание копеек");
        TestConstructors();
        TestProperties();
        TestToString();
        TestSubtractKopeks();
    }

    // Задание 7

    private static void TestIncrement()
    {
        Console.WriteLine();
        Console.WriteLine("--- Оператор ++ ---");

        Money money = ReadMoney(
            "Введите сумму (например 5 руб. 99 коп.)",
            99);
        PrintMoney("До", money);

        Money old = money++;
        PrintMoney("money после money++", money);
        PrintMoney(
            "Значение выражения money++ (старое)", old);

        Money current = ++money;
        PrintMoney("money после ++money", money);
        PrintMoney("Значение выражения ++money", current);
    }

    private static void TestDecrement()
    {
        Console.WriteLine();
        Console.WriteLine("--- Оператор -- ---");

        Money money = ReadMoney(
            "Введите сумму (попробуйте 5 руб. 0 коп. " +
            "и 0 руб. 0 коп.)", 99);
        PrintMoney("До", money);

        Money old = money--;
        PrintMoney("money после money--", money);
        PrintMoney(
            "Значение выражения money-- (старое)", old);

        Money current = --money;
        PrintMoney("money после --money", money);
        PrintMoney("Значение выражения --money", current);
    }

    private static void TestConversions()
    {
        Console.WriteLine();
        Console.WriteLine("--- Преобразования типов ---");

        Money money = ReadMoney("Введите сумму", 99);
        PrintMoney("Сумма", money);

        uint rubles = (uint)money;
        Console.WriteLine("(uint)money = " + rubles);

        bool isNonZero = money;
        Console.WriteLine("bool(money) = " + isNonZero);
        if (money)
        {
            Console.WriteLine("Сумма не равна нулю.");
        }
        else
        {
            Console.WriteLine("Сумма равна нулю.");
        }

        Money zero = new Money();
        bool zeroFlag = zero;
        Console.WriteLine(
            "bool(new Money()) = " + zeroFlag);
    }

    private static void TestMoneyMinusNumber()
    {
        Console.WriteLine();
        Console.WriteLine("--- Money - uint ---");

        Money money = ReadMoney("Введите сумму", 99);
        uint kopeks = ConsoleHelper.ReadUInt(
            "Вычесть копеек: ");
        Money result = money - kopeks;
        PrintMoney("Результат", result);
    }

    private static void TestNumberMinusMoney()
    {
        Console.WriteLine();
        Console.WriteLine("--- uint - Money ---");

        uint kopeks = ConsoleHelper.ReadUInt(
            "Количество копеек: ");
        Money money = ReadMoney("Введите сумму", 99);
        Money result = kopeks - money;
        PrintMoney("Результат", result);
    }

    private static void TestMoneyMinusMoney()
    {
        Console.WriteLine();
        Console.WriteLine("--- Money - Money ---");

        Money first = ReadMoney("Первая сумма", 99);
        Money second = ReadMoney("Вторая сумма", 99);
        PrintMoney("first", first);
        PrintMoney("second", second);
        PrintMoney("first - second", first - second);
        PrintMoney("second - first", second - first);
    }

    private static void TestOverflow()
    {
        Console.WriteLine();
        Console.WriteLine(
            "--- Защита от переполнения ---");

        Money big = new Money(uint.MaxValue, 99);
        PrintMoney("Максимальная сумма", big);
        try
        {
            Money bigger = big++;
            PrintMoney("После ++", bigger);
        }
        catch (OverflowException exception)
        {
            Console.WriteLine(
                "Ожидаемое исключение: " +
                exception.Message);
        }

        Money limit = new Money(uint.MaxValue, 0);
        try
        {
            limit.Kopeks = 200;
            PrintMoney("После установки Kopeks", limit);
        }
        catch (OverflowException exception)
        {
            Console.WriteLine(
                "Ожидаемое исключение: " +
                exception.Message);
        }
    }

    public static void RunTask7()
    {
        Console.WriteLine();
        Console.WriteLine(
            "Задание 7. Перегруженные операции Money");
        TestIncrement();
        TestDecrement();
        TestConversions();
        TestMoneyMinusNumber();
        TestNumberMinusMoney();
        TestMoneyMinusMoney();
        TestOverflow();
    }
}