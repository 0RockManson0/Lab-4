using System;
using System.Collections.Generic;
using System.IO;

internal static class Tasks
{
    private static List<int> ReadIntList(string name)
    {
        List<int> list = new List<int>();
        int count = 0;
        int i = 0;
        int num = 0;
        
        Console.WriteLine($"\nВвод списка {name}:");
        count = ConsoleHelper.ReadPositiveInt(
            "Количество элементов: ");
        
        for (i = 0; i < count; i++)
        {
            num = ConsoleHelper.ReadInt(
                $"Элемент {i + 1}: ");
            list.Add(num);
        }
        return list;
    }

    private static void PrintList(
        List<int> list, string title)
    {
        int i = 0;
        Console.Write($"\n{title}: [");
        for (i = 0; i < list.Count; i++)
        {
            Console.Write(list[i]);
            if (i < list.Count - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }

    private static bool ContainsInt(
        List<int> list, int value)
    {
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
            if (list[i] == value)
            {
                return true;
            }
        }
        return false;
    }

    public static void RunTask1()
    {
        Console.WriteLine(
            "\nЗадание 1: " +
            "Симметрическая разность списков");
        
        List<int> L1 = ReadIntList("L1");
        List<int> L2 = ReadIntList("L2");
        
        PrintList(L1, "L1");
        PrintList(L2, "L2");
        
        List<int> L = new List<int>();
        int i = 0;
        int num = 0;
        
        for (i = 0; i < L1.Count; i++)
        {
            num = L1[i];
            if (!ContainsInt(L2, num) && 
                !ContainsInt(L, num))
            {
                L.Add(num);
            }
        }
        
        for (i = 0; i < L2.Count; i++)
        {
            num = L2[i];
            if (!ContainsInt(L1, num) && 
                !ContainsInt(L, num))
            {
                L.Add(num);
            }
        }
        
        PrintList(L, "Результат L");
    }
    
    public static void RunTask2()
    {
        Console.WriteLine(
            "\nЗадание 2: LinkedList — " +
            "удалить между min и max");
        
        LinkedList<int> list = new LinkedList<int>();
        int count = 0;
        int i = 0;
        int num = 0;
        
        count = ConsoleHelper.ReadIntInRange(
            "Количество элементов (минимум 3): ", 
            3, int.MaxValue);
        
        for (i = 0; i < count; i++)
        {
            num = ConsoleHelper.ReadInt(
                $"Элемент {i + 1}: ");
            list.AddLast(num);
        }
        
        Console.Write("\nИсходный список: ");
        foreach (int val in list)
        {
            Console.Write(val + " ");
        }
        Console.WriteLine();
        
        LinkedListNode<int> minNode = list.First;
        LinkedListNode<int> maxNode = list.First;
        LinkedListNode<int> current = list.First;
        
        while (current != null)
        {
            if (current.Value < minNode.Value)
            {
                minNode = current;
            }
            if (current.Value > maxNode.Value)
            {
                maxNode = current;
            }
            current = current.Next;
        }
        
        Console.WriteLine(
            $"Min: {minNode.Value}, " +
            $"Max: {maxNode.Value}");
        
        if (minNode == maxNode)
        {
            Console.WriteLine(
                "Min и Max — один элемент, " +
                "удалять нечего.");
            return;
        }
        
        LinkedListNode<int> first = minNode;
        LinkedListNode<int> last = maxNode;
        
        LinkedListNode<int> temp = minNode;
        bool minBeforeMax = false;
        while (temp != null)
        {
            if (temp == maxNode)
            {
                minBeforeMax = true;
                break;
            }
            temp = temp.Next;
        }
        
        if (!minBeforeMax)
        {
            first = maxNode;
            last = minNode;
        }
        
        if (first.Next == last)
        {
            Console.WriteLine(
                "Между min и max нет элементов.");
            return;
        }
        
        LinkedListNode<int> toDelete = first.Next;
        while (toDelete != last)
        {
            LinkedListNode<int> next = toDelete.Next;
            list.Remove(toDelete);
            toDelete = next;
        }
        
        Console.Write("Результат: ");
        foreach (int val in list)
        {
            Console.Write(val + " ");
        }
        Console.WriteLine();
    }
    
    public static void RunTask3()
    {
        Console.WriteLine(
            "\nЗадание 3: HashSet — ТВ-шоу");
        
        int showCount = ConsoleHelper.ReadPositiveInt(
            "Количество шоу в перечне: ");
        
        List<string> allShows = new List<string>();
        int i = 0;
        string show = "";
        
        for (i = 0; i < showCount; i++)
        {
            while (true)
            {
                show = ConsoleHelper.ReadString(
                    $"Шоу {i + 1}: ");
                
                bool isDuplicate = false;
                int j = 0;
                for (j = 0; j < allShows.Count; j++)
                {
                    if (allShows[j].ToLower() == 
                        show.ToLower())
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                
                if (isDuplicate)
                {
                    Console.WriteLine(
                        "Шоу с таким названием " +
                        "уже есть. Введите другое.");
                }
                else
                {
                    allShows.Add(show);
                    break;
                }
            }
        }
        
        int viewerCount = ConsoleHelper.ReadPositiveInt(
            "Количество телезрителей: ");
        
        int[] likesCount = new int[showCount];
        for (i = 0; i < showCount; i++)
        {
            likesCount[i] = 0;
        }
        
        for (i = 0; i < viewerCount; i++)
        {
            Console.WriteLine(
                $"\nЗритель {i + 1}:");
            Console.WriteLine("Доступные шоу:");
            int j = 0;
            for (j = 0; j < showCount; j++)
            {
                Console.WriteLine(
                    $"  {j + 1}. {allShows[j]}");
            }
            
            string input = ConsoleHelper.ReadString(
                "Введите номера любимых шоу " +
                "через пробел (или 0, " +
                "если нет любимых): ");
            
            string[] parts = input.Split(' ');
            HashSet<int> chosen = 
                new HashSet<int>();
            
            for (j = 0; j < parts.Length; j++)
            {
                int num = 0;
                if (int.TryParse(parts[j], out num))
                {
                    if (num == 0)
                    {
                        continue;
                    }
                    
                    if (num < 1 || num > showCount)
                    {
                        Console.WriteLine(
                            $"  Номер {num} вне " +
                            $"диапазона 1-{showCount}, " +
                            $"пропускаем.");
                        continue;
                    }
                    
                    if (chosen.Contains(num))
                    {
                        Console.WriteLine(
                            $"  Номер {num} уже " +
                            $"выбран, пропускаем.");
                        continue;
                    }
                    
                    likesCount[num - 1]++;
                    chosen.Add(num);
                }
                else
                {
                    Console.WriteLine(
                        $"  '{parts[j]}' — " +
                        $"не число, пропускаем.");
                }
            }
        }
        
        List<string> likedByAll = new List<string>();
        List<string> likedBySome = new List<string>();
        List<string> likedByNone = new List<string>();
        
        for (i = 0; i < showCount; i++)
        {
            if (likesCount[i] == viewerCount)
            {
                likedByAll.Add(allShows[i]);
            }
            else if (likesCount[i] > 0)
            {
                likedBySome.Add(allShows[i]);
            }
            else
            {
                likedByNone.Add(allShows[i]);
            }
        }
        
        Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ===");
        
        Console.WriteLine(
            $"\nНравятся ВСЕМ " +
            $"({viewerCount} зрителям):");
        if (likedByAll.Count == 0)
        {
            Console.WriteLine("  (нет таких шоу)");
        }
        else
        {
            for (i = 0; i < likedByAll.Count; i++)
            {
                Console.WriteLine(
                    $"  - {likedByAll[i]}");
            }
        }
        
        Console.WriteLine(
            $"\nНравятся НЕКОТОРЫМ " +
            $"(но не всем):");
        if (likedBySome.Count == 0)
        {
            Console.WriteLine("  (нет таких шоу)");
        }
        else
        {
            for (i = 0; i < likedBySome.Count; i++)
            {
                Console.WriteLine(
                    $"  - {likedBySome[i]}");
            }
        }
        
        Console.WriteLine(
            $"\nНе нравятся НИКОМУ:");
        if (likedByNone.Count == 0)
        {
            Console.WriteLine("  (нет таких шоу)");
        }
        else
        {
            for (i = 0; i < likedByNone.Count; i++)
            {
                Console.WriteLine(
                    $"  - {likedByNone[i]}");
            }
        }
    }
    
    private static int ChooseMode()
    {
        int mode = 0;
        bool isValid = false;
        
        while (!isValid)
        {
            Console.WriteLine("\nВыберите режим:");
            Console.WriteLine("1 - Случайная генерация");
            Console.WriteLine("2 - Ручной ввод");
            mode = ConsoleHelper.ReadInt("Ваш выбор: ");
            
            if (mode == 1 || mode == 2)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine(
                    "Некорректный ввод. " +
                    "Введите 1 или 2.");
            }
        }
        return mode;
    }

    private static void FillTextFileRandom(string path)
    {
        string[] sampleTexts = {
            "В 2024 году было 365 дней.",
            "Температура упала до -15 градусов.",
            "Код ошибки 404: страница не найдена.",
            "В году 12 месяцев и около 52 недель.",
            "Число Пи равно 3.14159..."
        };
        
        Random rnd = new Random();
        StreamWriter writer = new StreamWriter(path);
        int i = 0;
        
        try
        {
            for (i = 0; i < sampleTexts.Length; i++)
            {
                writer.WriteLine(sampleTexts[i]);
            }
            int num = rnd.Next(100, 999);
            writer.WriteLine(
                $"Случайное число: {num}");
        }
        finally
        {
            writer.Close();
        }
    }

    private static void FillTextFileManual(string path)
    {
        int lineCount = ConsoleHelper.ReadPositiveInt(
            "Количество строк текста: ");
        
        StreamWriter writer = new StreamWriter(path);
        int i = 0;
        string line = "";
        
        try
        {
            for (i = 0; i < lineCount; i++)
            {
                line = ConsoleHelper.ReadString(
                    $"Строка {i + 1}: ");
                writer.WriteLine(line);
            }
        }
        finally
        {
            writer.Close();
        }
    }

    private static void PrintFileContent(string path)
    {
        Console.WriteLine("\nСодержимое файла:");
        StreamReader reader = new StreamReader(path);
        string line = "";
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        finally
        {
            reader.Close();
        }
    }

    public static void RunTask4()
    {
        Console.WriteLine(
            "\nЗадание 4: HashSet — " +
            "цифры в тексте");
        
        string path = "text4.txt";
        string fullPath = Path.GetFullPath(path);
        Console.WriteLine($"Полный путь: {fullPath}");
        
        int mode = ChooseMode();
        
        if (mode == 1)
        {
            FillTextFileRandom(path);
            Console.WriteLine(
                "Файл заполнен случайными данными.");
        }
        else
        {
            FillTextFileManual(path);
            Console.WriteLine(
                "Файл заполнен вручную.");
        }
        
        PrintFileContent(path);
        
        HashSet<char> digits = new HashSet<char>();
        int i = 0;
        string line = "";
        
        StreamReader reader = new StreamReader(path);
        try
        {
            while ((line = reader.ReadLine()) != null)
            {
                for (i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        digits.Add(line[i]);
                    }
                }
            }
        }
        finally
        {
            reader.Close();
        }
        
        Console.WriteLine(
            "\nВстречающиеся цифры:");
        if (digits.Count == 0)
        {
            Console.WriteLine("  (цифры не найдены)");
        }
        else
        {
            foreach (char c in digits)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
        }
    }
    private struct Person
    {
        public string LastName;
        public string FirstName;
        public DateTime BirthDate;
    }

    private static DateTime ParseDate(string dateStr)
    {
        string[] parts = dateStr.Split('.');
        
        if (parts.Length != 3)
        {
            throw new FormatException(
                "Формат должен быть ДД.ММ.ГГГГ");
        }
        
        int day = 0;
        int month = 0;
        int year = 0;
        
        if (!int.TryParse(parts[0], out day))
        {
            throw new FormatException(
                "День должен быть числом");
        }
        if (!int.TryParse(parts[1], out month))
        {
            throw new FormatException(
                "Месяц должен быть числом");
        }
        if (!int.TryParse(parts[2], out year))
        {
            throw new FormatException(
                "Год должен быть числом");
        }
        
        if (day < 1 || day > 31)
        {
            throw new FormatException(
                "День должен быть от 01 до 31");
        }
        if (month < 1 || month > 12)
        {
            throw new FormatException(
                "Месяц должен быть от 01 до 12");
        }
        if (year < 1800 || year > 2100)
        {
            throw new FormatException(
                "Год должен быть от 1800 до 2100");
        }
        
        return new DateTime(year, month, day);
    }

    public static void RunTask5()
    {
        Console.WriteLine(
            "\nЗадание 5: " +
            "Самый старший человек");
        
        int n = ConsoleHelper.ReadPositiveInt(
            "Количество людей: ");
        
        DateTime oldestDate = DateTime.MaxValue;
        List<Person> oldestPeople = new List<Person>();
        
        int i = 0;
        for (i = 0; i < n; i++)
        {
            Console.WriteLine(
                $"\nЧеловек {i + 1}:");
            
            string lastName = 
                ConsoleHelper.ReadStringWithMaxLength(
                    "Фамилия (до 20 символов): ", 20);
            
            string firstName = 
                ConsoleHelper.ReadStringWithMaxLength(
                    "Имя (до 20 символов): ", 20);
            
            string dateStr = "";
            DateTime birthDate = DateTime.MinValue;
            bool isValid = false;
            
            while (!isValid)
            {
                dateStr = ConsoleHelper.ReadString(
                    "Дата рождения (ДД.ММ.ГГГГ): ");
                
                try
                {
                    birthDate = ParseDate(dateStr);
                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Ошибка: " + ex.Message);
                    Console.WriteLine(
                        "Повторите ввод.");
                }
            }
            
            Person p = new Person();
            p.LastName = lastName;
            p.FirstName = firstName;
            p.BirthDate = birthDate;
            
            if (birthDate < oldestDate)
            {
                oldestDate = birthDate;
                oldestPeople.Clear();
                oldestPeople.Add(p);
            }
            else if (birthDate == oldestDate)
            {
                oldestPeople.Add(p);
            }
        }
        
        Console.WriteLine(
            $"\nСамый старший человек " +
            $"(дата: {oldestDate:dd.MM.yyyy}):");
        if (oldestPeople.Count == 1)
        {
            Console.WriteLine(
                $"{oldestPeople[0].LastName} " +
                $"{oldestPeople[0].FirstName}");
        }
        else
        {
            Console.WriteLine(
                $"Количество людей с одинаковой " +
                $"датой: {oldestPeople.Count}");
            for (i = 0; i < oldestPeople.Count; i++)
            {
                Console.WriteLine(
                    $"  {oldestPeople[i].LastName} " +
                    $"{oldestPeople[i].FirstName}");
            }
        }
    }
}