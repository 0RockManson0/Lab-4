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
            Console.WriteLine("\nЛабораторная работа №4. Коллекции");
            Console.WriteLine("Выберите задание (1-5) или 0 для выхода:");
            Console.Write("Ваш выбор: ");
            choice = ConsoleHelper.ReadInt("");
            
            switch (choice)
            {
                case 0:
                    Console.WriteLine("Выход.");
                    return;
                case 1: Tasks.RunTask1(); break;
                case 2: Tasks.RunTask2(); break;
                case 3: Tasks.RunTask3(); break;
                case 4: Tasks.RunTask4(); break;
                case 5: Tasks.RunTask5(); break;
                default:
                    Console.WriteLine("Неверный номер задания.");
                    break;
            }
        }
    }
}