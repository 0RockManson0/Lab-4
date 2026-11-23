using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

internal static class Tasks
{


    private static void PrintTaskHeader(
        int number, string title)
    {
        Console.WriteLine();
        Console.WriteLine(
            "Задание " + number + ". " + title);
    }

    private static void PrintFilePath(string path)
    {
        Console.WriteLine(
            "Файл: " + Path.GetFullPath(path));
    }

    private static int ReadFillMode()
    {
        Console.WriteLine(
            "Выберите способ заполнения файла:");
        Console.WriteLine("1 — случайная генерация");
        Console.WriteLine("2 — ручной ввод");
        return ConsoleHelper.ReadIntInRange(
            "Ваш выбор: ", 1, 2);
    }
    
    // ЗАДАНИЕ 1. LIST

    private static List<T> ReadList<T>(
        string name, Func<string, T> readElement)
    {
        List<T> list = new List<T>();
        int count = ConsoleHelper.ReadPositiveInt(
            "Количество элементов списка " + name + ": ");
        int i = 0;
        T value = default(T);

        for (i = 0; i < count; i++)
        {
            value = readElement(
                "Элемент " + (i + 1) + ": ");
            list.Add(value);
        }

        return list;
    }

    private static bool ContainsValue<T>(
        List<T> list, T value)
    {
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(
                list[i], value))
            {
                return true;
            }
        }

        return false;
    }

    private static List<T> GetSymmetricDifference<T>(
        List<T> first, List<T> second)
    {
        List<T> result = new List<T>();
        int i = 0;
        T element = default(T);

        for (i = 0; i < first.Count; i++)
        {
            element = first[i];
            if (!ContainsValue(second, element) &&
                !ContainsValue(result, element))
            {
                result.Add(element);
            }
        }

        for (i = 0; i < second.Count; i++)
        {
            element = second[i];
            if (!ContainsValue(first, element) &&
                !ContainsValue(result, element))
            {
                result.Add(element);
            }
        }

        return result;
    }

    private static void PrintList<T>(
        List<T> list, string name)
    {
        int i = 0;
        Console.Write(name + ": [");
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

    private static void ProcessLists<T>(
        List<T> first, List<T> second)
    {
        List<T> result =
            GetSymmetricDifference(first, second);
        PrintList(first, "L1");
        PrintList(second, "L2");
        PrintList(result, "Результат L");
    }

    private static int ReadElementType()
    {
        Console.WriteLine(
            "Выберите тип элементов списка:");
        Console.WriteLine("1 — целые числа (int)");
        Console.WriteLine("2 — вещественные числа (double)");
        Console.WriteLine("3 — символы (char)");
        Console.WriteLine("4 — строки (string)");
        return ConsoleHelper.ReadIntInRange(
            "Ваш выбор: ", 1, 4);
    }

    private static void SolveTask1(int elementType)
    {
        if (elementType == 1)
        {
            List<int> first = ReadList<int>(
                "L1", ConsoleHelper.ReadInt);
            List<int> second = ReadList<int>(
                "L2", ConsoleHelper.ReadInt);
            ProcessLists(first, second);
        }
        else if (elementType == 2)
        {
            List<double> first = ReadList<double>(
                "L1", ConsoleHelper.ReadDouble);
            List<double> second = ReadList<double>(
                "L2", ConsoleHelper.ReadDouble);
            ProcessLists(first, second);
        }
        else if (elementType == 3)
        {
            List<char> first = ReadList<char>(
                "L1", ConsoleHelper.ReadChar);
            List<char> second = ReadList<char>(
                "L2", ConsoleHelper.ReadChar);
            ProcessLists(first, second);
        }
        else
        {
            List<string> first = ReadList<string>(
                "L1", ConsoleHelper.ReadString);
            List<string> second = ReadList<string>(
                "L2", ConsoleHelper.ReadString);
            ProcessLists(first, second);
        }
    }

    public static void RunTask1()
    {
        PrintTaskHeader(
            1, "Симметрическая разность списков");
        int elementType = ReadElementType();
        SolveTask1(elementType);
    }
    
    // ЗАДАНИЕ 2. LINKEDLIST

    private static LinkedList<int> ReadLinkedList()
    {
        LinkedList<int> list = new LinkedList<int>();
        int count = ConsoleHelper.ReadPositiveInt(
            "Количество элементов: ");
        int i = 0;
        int value = 0;

        for (i = 0; i < count; i++)
        {
            value = ConsoleHelper.ReadInt(
                "Элемент " + (i + 1) + ": ");
            list.AddLast(value);
        }

        return list;
    }

    private static void PrintLinkedList(
        LinkedList<int> list, string title)
    {
        LinkedListNode<int> current = list.First;
        Console.Write(title + ": ");
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }

        Console.WriteLine();
    }

    private static LinkedListNode<int> FindMinNode(
        LinkedList<int> list)
    {
        LinkedListNode<int> minNode = list.First;
        LinkedListNode<int> current = list.First;

        while (current != null)
        {
            if (current.Value < minNode.Value)
            {
                minNode = current;
            }

            current = current.Next;
        }

        return minNode;
    }

    private static LinkedListNode<int> FindMaxNode(
        LinkedList<int> list)
    {
        LinkedListNode<int> maxNode = list.First;
        LinkedListNode<int> current = list.First;

        while (current != null)
        {
            if (current.Value > maxNode.Value)
            {
                maxNode = current;
            }

            current = current.Next;
        }

        return maxNode;
    }

    private static bool IsNodeBefore(
        LinkedListNode<int> start,
        LinkedListNode<int> target)
    {
        LinkedListNode<int> current = start;
        while (current != null)
        {
            if (current == target)
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    private static bool RemoveBetweenMinMax(
        LinkedList<int> list)
    {
        if (list.Count < 3)
        {
            return false;
        }

        LinkedListNode<int> minNode = FindMinNode(list);
        LinkedListNode<int> maxNode = FindMaxNode(list);
        LinkedListNode<int> first = null;
        LinkedListNode<int> last = null;
        LinkedListNode<int> current = null;
        LinkedListNode<int> next = null;
        bool removed = false;

        if (minNode == maxNode)
        {
            return false;
        }

        first = minNode;
        last = maxNode;
        if (!IsNodeBefore(minNode, maxNode))
        {
            first = maxNode;
            last = minNode;
        }

        if (first.Next == last)
        {
            return false;
        }

        current = first.Next;
        while (current != null && current != last)
        {
            next = current.Next;
            list.Remove(current);
            current = next;
            removed = true;
        }

        return removed;
    }

    private static void ProcessLinkedList(
        LinkedList<int> list)
    {
        bool removed = false;
        PrintLinkedList(list, "Исходный список");
        removed = RemoveBetweenMinMax(list);
        if (!removed)
        {
            Console.WriteLine(
                "Между минимумом и максимумом " +
                "удалять нечего.");
        }

        PrintLinkedList(list, "Результат");
    }

    public static void RunTask2()
    {
        PrintTaskHeader(
            2, "Удаление элементов между min и max");
        LinkedList<int> list = ReadLinkedList();
        ProcessLinkedList(list);
    }
    
    // ЗАДАНИЕ 3. HASHSET

    private static bool ContainsString(
        List<string> list, string value)
    {
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
            if (string.Equals(
                list[i], value,
                StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private static List<string> ReadAllShows()
    {
        List<string> shows = new List<string>();
        int count = ConsoleHelper.ReadPositiveInt(
            "Количество шоу в перечне: ");
        int i = 0;
        string show = "";
        bool added = false;

        for (i = 0; i < count; i++)
        {
            added = false;
            while (!added)
            {
                show = ConsoleHelper.ReadString(
                    "Название шоу " + (i + 1) + ": ");
                if (ContainsString(shows, show))
                {
                    Console.WriteLine(
                        "Такое название уже есть. " +
                        "Повторите ввод.");
                }
                else
                {
                    shows.Add(show);
                    added = true;
                }
            }
        }

        return shows;
    }

    private static void PrintShowMenu(
        List<string> allShows)
    {
        int i = 0;
        Console.WriteLine("Перечень шоу:");
        for (i = 0; i < allShows.Count; i++)
        {
            Console.WriteLine(
                "  " + (i + 1) + " — " + allShows[i]);
        }
    }

    private static HashSet<string> ReadViewerFavorites(
        List<string> allShows, int viewerNumber)
    {
        HashSet<string> favorites = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);
        string line = "";
        string[] parts = null;
        int i = 0;
        int number = 0;

        PrintShowMenu(allShows);
        line = ConsoleHelper.ReadString(
            "Зритель " + viewerNumber +
            ": номера любимых шоу через пробел " +
            "(0 — нет любимых): ");
        parts = line.Split(
            new char[] { ' ', '\t' },
            StringSplitOptions.RemoveEmptyEntries);

        for (i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out number))
            {
                Console.WriteLine(
                    "  «" + parts[i] +
                    "» — не число, пропуск.");
                continue;
            }

            if (number == 0)
            {
                continue;
            }

            if (number < 1 || number > allShows.Count)
            {
                Console.WriteLine(
                    "  Номер " + number +
                    " вне диапазона, пропуск.");
                continue;
            }

            if (favorites.Contains(allShows[number - 1]))
            {
                Console.WriteLine(
                    "  Шоу «" + allShows[number - 1] +
                    "» уже выбрано, пропуск.");
                continue;
            }

            favorites.Add(allShows[number - 1]);
        }

        return favorites;
    }

    private static List<HashSet<string>> ReadViewers(
        List<string> allShows)
    {
        List<HashSet<string>> viewers =
            new List<HashSet<string>>();
        int viewerCount = ConsoleHelper.ReadPositiveInt(
            "Количество телезрителей: ");
        int i = 0;

        for (i = 0; i < viewerCount; i++)
        {
            Console.WriteLine();
            viewers.Add(
                ReadViewerFavorites(allShows, i + 1));
        }

        return viewers;
    }

    private static void ClassifyShows(
        List<string> allShows,
        List<HashSet<string>> viewers,
        out List<string> likedByAll,
        out List<string> likedBySome,
        out List<string> likedByNone)
    {
        likedByAll = new List<string>();
        likedBySome = new List<string>();
        likedByNone = new List<string>();
        int i = 0;
        int j = 0;
        int likes = 0;

        for (i = 0; i < allShows.Count; i++)
        {
            likes = 0;
            for (j = 0; j < viewers.Count; j++)
            {
                if (viewers[j].Contains(allShows[i]))
                {
                    likes++;
                }
            }

            if (likes == viewers.Count)
            {
                likedByAll.Add(allShows[i]);
            }
            else if (likes > 0)
            {
                likedBySome.Add(allShows[i]);
            }
            else
            {
                likedByNone.Add(allShows[i]);
            }
        }
    }

    private static void PrintShows(
        string title, List<string> shows)
    {
        int i = 0;
        Console.WriteLine();
        Console.WriteLine(title);
        if (shows.Count == 0)
        {
            Console.WriteLine("  Нет таких шоу.");
            return;
        }

        for (i = 0; i < shows.Count; i++)
        {
            Console.WriteLine("  — " + shows[i]);
        }
    }

    private static void PrintShowClassification(
        List<string> allShows,
        List<HashSet<string>> viewers)
    {
        List<string> likedByAll;
        List<string> likedBySome;
        List<string> likedByNone;

        ClassifyShows(
            allShows, viewers,
            out likedByAll,
            out likedBySome,
            out likedByNone);
        PrintShows("Нравятся всем:", likedByAll);
        PrintShows("Нравятся некоторым:", likedBySome);
        PrintShows("Не нравятся никому:", likedByNone);
    }

    public static void RunTask3()
    {
        PrintTaskHeader(3, "Телевизионные шоу");
        List<string> allShows = ReadAllShows();
        List<HashSet<string>> viewers =
            ReadViewers(allShows);
        PrintShowClassification(allShows, viewers);
    }


    // ЗАДАНИЕ 4. HASHSET


    private static void FillTextFileRandom(string path)
    {
        string[] sampleTexts = {
            "В 2024 году было 365 дней.",
            "Температура упала до -15 градусов.",
            "Код ошибки 404: страница не найдена.",
            "В году 12 месяцев и около 52 недель.",
            "Число Пи равно 3.14159..."
        };
        Random random = new Random();
        int i = 0;
        int number = 0;

        using (StreamWriter writer = new StreamWriter(
            path, false, Encoding.UTF8))
        {
            for (i = 0; i < sampleTexts.Length; i++)
            {
                writer.WriteLine(sampleTexts[i]);
            }

            number = random.Next(100, 1000);
            writer.WriteLine(
                "Случайное число: " + number);
        }
    }

    private static void FillTextFileManual(string path)
    {
        int count = ConsoleHelper.ReadPositiveInt(
            "Количество строк в файле: ");
        int i = 0;
        string line = "";

        using (StreamWriter writer = new StreamWriter(
            path, false, Encoding.UTF8))
        {
            for (i = 0; i < count; i++)
            {
                line = ConsoleHelper.ReadString(
                    "Строка " + (i + 1) + ": ");
                writer.WriteLine(line);
            }
        }
    }

    private static void FillTextFileByMode(string path)
    {
        int mode = ReadFillMode();
        if (mode == 1)
        {
            FillTextFileRandom(path);
            Console.WriteLine(
                "Файл заполнен случайными данными.");
        }
        else
        {
            FillTextFileManual(path);
            Console.WriteLine("Файл заполнен вручную.");
        }
    }

    private static void PrintTextFile(string path)
    {
        string line = "";
        Console.WriteLine();
        Console.WriteLine("Содержимое файла:");
        using (StreamReader reader = new StreamReader(
            path, Encoding.UTF8))
        {
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    private static HashSet<char> FindDigitsInFile(
        string path)
    {
        HashSet<char> digits = new HashSet<char>();
        string line = "";
        int i = 0;

        using (StreamReader reader = new StreamReader(
            path, Encoding.UTF8))
        {
            while ((line = reader.ReadLine()) != null)
            {
                for (i = 0; i < line.Length; i++)
                {
                    if (line[i] >= '0' && line[i] <= '9')
                    {
                        digits.Add(line[i]);
                    }
                }
            }
        }

        return digits;
    }

    private static void PrintDigits(
        HashSet<char> digits)
    {
        char digit = '0';

        Console.WriteLine();
        Console.WriteLine(
            "Цифры, которые встречаются в тексте:");
        if (digits.Count == 0)
        {
            Console.WriteLine("  Цифр в тексте нет.");
            return;
        }

        for (digit = '0'; digit <= '9'; digit++)
        {
            if (digits.Contains(digit))
            {
                Console.Write(digit + " ");
            }
        }

        Console.WriteLine();
    }

    public static void RunTask4()
    {
        PrintTaskHeader(
            4, "Поиск цифр в текстовом файле");
        string path = "text4.txt";
        PrintFilePath(path);
        FillTextFileByMode(path);
        PrintTextFile(path);
        HashSet<char> digits = FindDigitsInFile(path);
        PrintDigits(digits);
    }
    
    // ЗАДАНИЕ 5. DICTIONARY

    private static string ReadPersonName(string message)
    {
        string value = "";
        bool isValid = false;

        while (!isValid)
        {
            value = ConsoleHelper.ReadStringMaxLength(
                message, 20);
            if (value.IndexOf(' ') >= 0)
            {
                Console.WriteLine(
                    "Значение не должно " +
                    "содержать пробелов.");
            }
            else
            {
                isValid = true;
            }
        }

        return value;
    }

    private static void FillPeopleFile(string path)
    {
        int count = ConsoleHelper.ReadPositiveInt(
            "Количество людей: ");
        int i = 0;
        string lastName = "";
        string firstName = "";
        DateTime birthDate = DateTime.MinValue;

        using (StreamWriter writer = new StreamWriter(
            path, false, Encoding.UTF8))
        {
            writer.WriteLine(count);
            for (i = 0; i < count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Человек " + (i + 1));
                lastName = ReadPersonName(
                    "Фамилия (до 20 символов): ");
                firstName = ReadPersonName(
                    "Имя (до 20 символов): ");
                birthDate = ConsoleHelper.ReadBirthDate(
                    "Дата рождения (ДД.ММ.ГГГГ): ");
                writer.WriteLine(
                    lastName + " " + firstName + " " +
                    birthDate.ToString("dd.MM.yyyy"));
            }
        }
    }

    private static Dictionary<DateTime, int>
        FindOldestPeople(
            string path, out string oldestName)
    {
        Dictionary<DateTime, int> oldest =
            new Dictionary<DateTime, int>();
        DateTime oldestDate = DateTime.MaxValue;
        oldestName = "";
        string line = "";
        string[] parts = null;
        string fullName = "";
        int count = 0;
        int i = 0;
        DateTime birthDate = DateTime.MinValue;

        using (StreamReader reader = new StreamReader(
            path, Encoding.UTF8))
        {
            line = reader.ReadLine();
            if (line == null ||
                !int.TryParse(line, out count) ||
                count <= 0)
            {
                throw new FormatException(
                    "Неверная первая строка: " +
                    "ожидалось количество людей.");
            }

            for (i = 0; i < count; i++)
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    throw new FormatException(
                        "В файле недостаточно записей.");
                }

                parts = line.Split(
                    new char[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                {
                    throw new FormatException(
                        "Неверная запись: " + line);
                }

                if (parts[0].Length > 20 ||
                    parts[1].Length > 20)
                {
                    throw new FormatException(
                        "Фамилия или имя длиннее " +
                        "20 символов.");
                }

                if (!DateTime.TryParseExact(
                    parts[2],
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out birthDate))
                {
                    throw new FormatException(
                        "Неверная дата в файле: " +
                        parts[2]);
                }

                if (birthDate.Year < 1800 ||
                    birthDate.Year > 2100)
                {
                    throw new FormatException(
                        "Год рождения должен быть " +
                        "от 1800 до 2100.");
                }

                fullName = parts[0] + " " + parts[1];

                if (birthDate < oldestDate)
                {
                    oldestDate = birthDate;
                    oldestName = fullName;
                    oldest.Clear();
                    oldest.Add(oldestDate, 1);
                }
                else if (birthDate == oldestDate)
                {
                    oldest[oldestDate] =
                        oldest[oldestDate] + 1;
                }
            }
        }

        return oldest;
    }

    private static void PrintOldestResult(
        Dictionary<DateTime, int> oldest,
        string oldestName)
    {
        foreach (KeyValuePair<DateTime, int> entry
            in oldest)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Самая ранняя дата рождения: " +
                entry.Key.ToString("dd.MM.yyyy"));
            if (entry.Value == 1)
            {
                Console.WriteLine(oldestName);
            }
            else
            {
                Console.WriteLine(entry.Value);
            }
        }
    }

    public static void RunTask5()
    {
        PrintTaskHeader(
            5, "Поиск самого старшего человека");
        string path = "people5.txt";
        PrintFilePath(path);
        FillPeopleFile(path);
        string oldestName;
        Dictionary<DateTime, int> oldest =
            FindOldestPeople(path, out oldestName);
        PrintOldestResult(oldest, oldestName);
    }
}