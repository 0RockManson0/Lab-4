using System;
using System.Collections.Generic;
using System.IO;

internal static class Tasks
{
    // ==========================================
    // ЗАДАНИЕ 1: List — симметрическая разность
    // ==========================================
    private static List<int> ReadIntList(string name)
    {
        List<int> list = new List<int>();
        int count = 0;
        int i = 0;
        int num = 0;
        
        Console.WriteLine($"\nВвод списка {name}:");
        count = ConsoleHelper.ReadInt("Количество элементов: ");
        
        for (i = 0; i < count; i++)
        {
            num = ConsoleHelper.ReadInt($"Элемент {i + 1}: ");
            list.Add(num);
        }
        return list;
    }

    private static void PrintList(List<int> list, string title)
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

    private static bool ContainsInt(List<int> list, int value)
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
        Console.WriteLine("\nЗадание 1: Симметрическая разность списков");
    
        List<int> L1 = ReadIntList("L1");
        List<int> L2 = ReadIntList("L2");
    
        PrintList(L1, "L1");
        PrintList(L2, "L2");
    
        List<int> L = new List<int>();
        int i = 0;
        int num = 0;
    
        // Элементы из L1, которых нет в L2
        for (i = 0; i < L1.Count; i++)
        {
            num = L1[i];
            // ✅ ПРОВЕРКА: нет в L2 И нет уже в L
            if (!ContainsInt(L2, num) && !ContainsInt(L, num))
            {
                L.Add(num);
            }
        }
    
        // Элементы из L2, которых нет в L1
        for (i = 0; i < L2.Count; i++)
        {
            num = L2[i];
            // ✅ ПРОВЕРКА: нет в L1 И нет уже в L
            if (!ContainsInt(L1, num) && !ContainsInt(L, num))
            {
                L.Add(num);
            }
        }
    
        PrintList(L, "Результат L");
    }
    

    // ==========================================
    // ЗАДАНИЕ 2: LinkedList — удалить между min и max
    // ==========================================
    public static void RunTask2()
    {
        Console.WriteLine("\nЗадание 2: LinkedList — удалить между min и max");
        
        LinkedList<int> list = new LinkedList<int>();
        int count = 0;
        int i = 0;
        int num = 0;
        
        count = ConsoleHelper.ReadInt("Количество элементов: ");
        for (i = 0; i < count; i++)
        {
            num = ConsoleHelper.ReadInt($"Элемент {i + 1}: ");
            list.AddLast(num);
        }
        
        Console.Write("\nИсходный список: ");
        foreach (int val in list)
        {
            Console.Write(val + " ");
        }
        Console.WriteLine();
        
        if (list.Count < 3)
        {
            Console.WriteLine("Недостаточно элементов для операции.");
            return;
        }
        
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
        
        Console.WriteLine($"Min: {minNode.Value}, Max: {maxNode.Value}");
        
        if (minNode == maxNode)
        {
            Console.WriteLine("Min и Max — один элемент, удалять нечего.");
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

    // ==========================================
    // ЗАДАНИЕ 3: HashSet — ТВ-шоу
    // ==========================================
    private static HashSet<string> ReadShowList(string viewerName)
    {
        HashSet<string> shows = new HashSet<string>();
        int count = 0;
        int i = 0;
        string show = "";
        
        Console.WriteLine($"\nЗритель '{viewerName}':");
        count = ConsoleHelper.ReadInt("Количество любимых шоу: ");
        
        for (i = 0; i < count; i++)
        {
            show = ConsoleHelper.ReadString($"Шоу {i + 1}: ");
            shows.Add(show);
        }
        return shows;
    }

    public static void RunTask3()
    {
        Console.WriteLine("\nЗадание 3: HashSet — ТВ-шоу");
        
        int n = ConsoleHelper.ReadInt("Количество телезрителей: ");
        
        HashSet<string> allShows = new HashSet<string>();
        List<HashSet<string>> viewers = new List<HashSet<string>>();
        
        int i = 0;
        for (i = 0; i < n; i++)
        {
            HashSet<string> viewerShows = ReadShowList($"Зритель {i + 1}");
            viewers.Add(viewerShows);
            
            foreach (string show in viewerShows)
            {
                allShows.Add(show);
            }
        }
        
        HashSet<string> likedByAll = new HashSet<string>();
        HashSet<string> likedBySome = new HashSet<string>();
        HashSet<string> likedByNone = new HashSet<string>();
        
        foreach (string show in allShows)
        {
            int count = 0;
            for (i = 0; i < viewers.Count; i++)
            {
                if (viewers[i].Contains(show))
                {
                    count++;
                }
            }
            
            if (count == n)
            {
                likedByAll.Add(show);
            }
            else if (count > 0)
            {
                likedBySome.Add(show);
            }
            else
            {
                likedByNone.Add(show);
            }
        }
        
        Console.WriteLine("\nНравятся всем:");
        foreach (string show in likedByAll)
        {
            Console.WriteLine("  - " + show);
        }
        
        Console.WriteLine("\nНравятся некоторым:");
        foreach (string show in likedBySome)
        {
            Console.WriteLine("  - " + show);
        }
        
        Console.WriteLine("\nНе нравятся никому:");
        foreach (string show in likedByNone)
        {
            Console.WriteLine("  - " + show);
        }
    }

    // ==========================================
    // ЗАДАНИЕ 4: HashSet — цифры в тексте
    // ==========================================
    private static void FillTextFile(string path)
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
            writer.WriteLine($"Случайное число: {num}");
        }
        finally
        {
            writer.Close();
        }
    }

    public static void RunTask4()
    {
        Console.WriteLine("\nЗадание 4: HashSet — цифры в тексте");
        
        string path = "text4.txt";
        string fullPath = Path.GetFullPath(path);
        Console.WriteLine($"Полный путь: {fullPath}");
        
        FillTextFile(path);
        Console.WriteLine("Файл заполнен.");
        
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
        
        HashSet<char> digits = new HashSet<char>();
        int i = 0;
        
        reader = new StreamReader(path);
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
        
        Console.WriteLine("\nВстречающиеся цифры:");
        foreach (char c in digits)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();
    }

    // ==========================================
    // ЗАДАНИЕ 5: Dictionary — самый старший (O(n))
    // ==========================================
    private struct Person
    {
        public string LastName;
        public string FirstName;
        public DateTime BirthDate;
    }

    private static DateTime ParseDate(string dateStr)
    {
        string[] parts = dateStr.Split('.');
        int day = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int year = int.Parse(parts[2]);
        
        if (day < 1 || day > 31)
        {
            throw new FormatException("День должен быть от 01 до 31");
        }
        if (month < 1 || month > 12)
        {
            throw new FormatException("Месяц должен быть от 01 до 12");
        }
        if (year < 1800 || year > 2100)
        {
            throw new FormatException("Год должен быть от 1800 до 2100");
        }
        
        return new DateTime(year, month, day);
    }

    public static void RunTask5()
    {
        Console.WriteLine("\nЗадание 5: Dictionary — самый старший человек");
        
        int n = ConsoleHelper.ReadInt("Количество людей: ");
        
        DateTime oldestDate = DateTime.MaxValue;
        List<Person> oldestPeople = new List<Person>();
        
        int i = 0;
        for (i = 0; i < n; i++)
        {
            Console.WriteLine($"\nЧеловек {i + 1}:");
            string lastName = ConsoleHelper.ReadString("Фамилия: ");
            string firstName = ConsoleHelper.ReadString("Имя: ");
            string dateStr = ConsoleHelper.ReadString("Дата рождения (ДД.ММ.ГГГГ): ");
            
            DateTime birthDate = DateTime.MinValue;  // ✅ ИСПРАВЛЕНО: инициализация
            bool isValid = false;
            
            while (!isValid)
            {
                try
                {
                    birthDate = ParseDate(dateStr);
                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                    dateStr = ConsoleHelper.ReadString("Повторите ввод (ДД.ММ.ГГГГ): ");
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
        
        Console.WriteLine($"\nСамый старший человек (дата: {oldestDate:dd.MM.yyyy}):");
        if (oldestPeople.Count == 1)
        {
            Console.WriteLine($"{oldestPeople[0].LastName} {oldestPeople[0].FirstName}");
        }
        else
        {
            Console.WriteLine($"Количество людей с одинаковой датой: {oldestPeople.Count}");
            for (i = 0; i < oldestPeople.Count; i++)
            {
                Console.WriteLine($"  {oldestPeople[i].LastName} {oldestPeople[i].FirstName}");
            }
        }
    }
}