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
            Console.WriteLine("\nЛабораторная работа №4");
            Console.WriteLine("1 — Задание List");
            Console.WriteLine("2 — Задание LinkedList");
            Console.WriteLine("3 — Задание HashSet (телешоу)");
            Console.WriteLine("4 — Задание HashSet (цифры в файле)");
            Console.WriteLine("5 — Задание Dictionary (люди)");
            Console.WriteLine("0 — Выход");

            choice = ConsoleHelper.ReadInt("Ваш выбор: ");

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    Tasks.RunTask1();
                    break;
                case 2:
                    Tasks.RunTask2();
                    break;
                case 3:
                    Tasks.RunTask3();
                    break;
                case 4:
                    Tasks.RunTask4();
                    break;
                case 5:
                    Tasks.RunTask5();
                    break;
                default:
                    Console.WriteLine("Такого задания нет.");
                    break;
            }
        }
    }
}