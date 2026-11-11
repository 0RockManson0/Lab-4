using System;
using System.Text;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        int choice = 0;

        while (true)
        {
            Console.WriteLine("\nЛабораторная работа №4. Money");
            Console.WriteLine("6 — Проверка класса Money");
            Console.WriteLine("7 — Проверка перегруженных операторов");
            Console.WriteLine("0 — Выход");

            choice = ConsoleHelper.ReadInt("Ваш выбор: ");

            switch (choice)
            {
                case 0:
                    return;
                case 6:
                    RunTask6();
                    break;
                case 7:
                    RunTask7();
                    break;
                default:
                    Console.WriteLine("Такого задания нет.");
                    break;
            }
        }
    }

    // =========================
    // ЗАДАНИЕ 6
    // =========================

    private static void RunTask6()
    {
        Console.WriteLine(
            "\nЗадание 6. Класс Money и добавление копеек");

        // Проверка конструктора без параметров.

        Money empty = new Money();

        Console.WriteLine(
            "Конструктор без параметров: " + empty);

        // Проверка конструктора с параметрами.

        Money original = ConsoleHelper.ReadMoney(
            "Введите исходную сумму");

        Console.WriteLine(
            "Исходная сумма: " + original);

        // Проверка конструктора копирования.

        Money copy = new Money(original);

        Console.WriteLine(
            "Копия объекта: " + copy);

        // Проверка свойств.

        Console.WriteLine(
            "\nКоличество рублей: " + original.Rubles);

        Console.WriteLine(
            "Количество копеек: " + original.Kopeks);

        // Проверка изменения свойств.

        uint newRubles = ConsoleHelper.ReadUInt(
            "\nВведите новое количество рублей: ");

        byte newKopeks = ConsoleHelper.ReadKopeks(
            "Введите новое количество копеек: ");

        original.Rubles = newRubles;
        original.Kopeks = newKopeks;

        Console.WriteLine(
            "Сумма после изменения свойств: " + original);

        // Проверка метода AddKopeks.

        uint additionalKopeks = ConsoleHelper.ReadUInt(
            "\nСколько копеек добавить: ");

        Money result = original.AddKopeks(additionalKopeks);

        Console.WriteLine(
            "Результат добавления: " + result);

        Console.WriteLine(
            "Исходный объект после вызова метода: " + original);
    }

    // =========================
    // ЗАДАНИЕ 7
    // =========================

    private static void RunTask7()
    {
        Console.WriteLine(
            "\nЗадание 7. Перегрузка операторов Money");

        Money first = ConsoleHelper.ReadMoney(
            "Введите первую сумму");

        Console.WriteLine("Первая сумма: " + first);

        // Проверка ++.

        Money incremented = first;

        incremented++;

        Console.WriteLine(
            "После ++: " + incremented);

        // Проверка --.

        Money decremented = first;

        decremented--;

        Console.WriteLine(
            "После --: " + decremented);

        // Проверка явного преобразования uint.

        uint rubles = (uint)first;

        Console.WriteLine(
            "Явное преобразование в uint: " + rubles);

        // Проверка неявного преобразования bool.

        bool isNotZero = first;

        Console.WriteLine(
            "Неявное преобразование в bool: " + isNotZero);

        // Проверка Money - uint.

        uint kopeks = ConsoleHelper.ReadUInt(
            "\nСколько копеек вычесть из первой суммы: ");

        Money result1 = first - kopeks;

        Console.WriteLine(
            "Money - uint: " + result1);

        // Проверка uint - Money.

        uint startingKopeks = ConsoleHelper.ReadUInt(
            "\nВведите количество копеек для операции uint - Money: ");

        Money result2 = startingKopeks - first;

        Console.WriteLine(
            "uint - Money: " + result2);

        // Проверка Money - Money.

        Money second = ConsoleHelper.ReadMoney(
            "\nВведите вторую сумму");

        Money result3 = first - second;

        Console.WriteLine(
            "Money - Money: " + result3);
    }
}