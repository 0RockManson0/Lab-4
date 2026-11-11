using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

internal static class Tasks
{
    // =========================
    // ЗАДАНИЕ 1. LIST
    // =========================

    private static List<T> ReadList<T>(string name)
    {
        List<T> list = new List<T>();
        int count = ConsoleHelper.ReadPositiveInt(
            "Количество элементов списка " + name + ": ");

        int i = 0;

        for (i = 0; i < count; i++)
        {
            T value = default(T);

            if (typeof(T) == typeof(int))
            {
                int number = ConsoleHelper.ReadInt(
                    "Элемент " + (i + 1) + ": ");

                value = (T)(object)number;
            }
            else if (typeof(T) == typeof(string))
            {
                string text = ConsoleHelper.ReadString(
                    "Элемент " + (i + 1) + ": ");

                value = (T)(object)text;
            }

            list.Add(value);
        }

        return list;
    }

    private static bool Contains<T>(List<T> list, T value)
    {
        int i = 0;

        for (i = 0; i < list.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(list[i], value))
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

            if (!Contains(second, element) &&
                !Contains(result, element))
            {
                result.Add(element);
            }
        }

        for (i = 0; i < second.Count; i++)
        {
            element = second[i];

            if (!Contains(first, element) &&
                !Contains(result, element))
            {
                result.Add(element);
            }
        }

        return result;
    }

    private static void PrintList<T>(List<T> list, string name)
    {
        int i = 0;

        Console.Write(name + ": ");

        for (i = 0; i < list.Count; i++)
        {
            Console.Write(list[i]);

            if (i < list.Count - 1)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine();
    }

    public static void RunTask1()
    {
        Console.WriteLine("\nЗадание 1. Симметрическая разность списков");
        Console.WriteLine("Выберите тип элементов:");
        Console.WriteLine("1 — Целые числа");
        Console.WriteLine("2 — Строки");

        int type = 0;

        while (type != 1 && type != 2)
        {
            type = ConsoleHelper.ReadInt("Ваш выбор: ");

            if (type != 1 && type != 2)
            {
                Console.WriteLine("Введите 1 или 2.");
            }
        }

        if (type == 1)
        {
            List<int> first = ReadList<int>("L1");
            List<int> second = ReadList<int>("L2");

            List<int> result = GetSymmetricDifference(first, second);

            PrintList(first, "L1");
            PrintList(second, "L2");
            PrintList(result, "Результат");
        }
        else
        {
            List<string> first = ReadList<string>("L1");
            List<string> second = ReadList<string>("L2");

            List<string> result =
                GetSymmetricDifference(first, second);

            PrintList(first, "L1");
            PrintList(second, "L2");
            PrintList(result, "Результат");
        }
    }

    // =========================
    // ЗАДАНИЕ 2. LINKEDLIST
    // =========================

    private static void PrintLinkedList(LinkedList<int> list)
    {
        LinkedListNode<int> current = list.First;

        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }

        Console.WriteLine();
    }

    private static void RemoveBetweenMinimumAndMaximum(
        LinkedList<int> list)
    {
        if (list.Count < 2)
        {
            return;
        }

        int min = list.First.Value;
        int max = list.First.Value;

        LinkedListNode<int> current = list.First;

        while (current != null)
        {
            if (current.Value < min)
            {
                min = current.Value;
            }

            if (current.Value > max)
            {
                max = current.Value;
            }

            current = current.Next;
        }

        // Ищем крайние вхождения минимального
        // или максимального значения.

        LinkedListNode<int> firstBoundary = null;
        LinkedListNode<int> lastBoundary = null;

        current = list.First;

        while (current != null)
        {
            if (current.Value == min || current.Value == max)
            {
                if (firstBoundary == null)
                {
                    firstBoundary = current;
                }

                lastBoundary = current;
            }

            current = current.Next;
        }

        // Удаляем все элементы строго между границами.

        current = firstBoundary.Next;

        while (current != null && current != lastBoundary)
        {
            LinkedListNode<int> next = current.Next;
            list.Remove(current);
            current = next;
        }
    }

    public static void RunTask2()
    {
        Console.WriteLine(
            "\nЗадание 2. Удаление элементов между min и max");

        LinkedList<int> list = new LinkedList<int>();

        int count = ConsoleHelper.ReadPositiveInt(
            "Количество элементов: ");

        int i = 0;
        int number = 0;

        for (i = 0; i < count; i++)
        {
            number = ConsoleHelper.ReadInt(
                "Элемент " + (i + 1) + ": ");

            list.AddLast(number);
        }

        Console.Write("Исходный список: ");
        PrintLinkedList(list);

        RemoveBetweenMinimumAndMaximum(list);

        Console.Write("Результат: ");
        PrintLinkedList(list);
    }

    // =========================
    // ЗАДАНИЕ 3. HASHSET
    // =========================

    private static HashSet<string> ReadViewerFavorites(
        List<string> allShows, int viewerNumber)
    {
        HashSet<string> favorites =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        int count = ConsoleHelper.ReadInt(
            "Сколько шоу нравится зрителю " + viewerNumber + "?: ");

        while (count < 0 || count > allShows.Count)
        {
            Console.WriteLine(
                "Количество должно быть от 0 до " + allShows.Count);

            count = ConsoleHelper.ReadInt("Повторите ввод: ");
        }

        int i = 0;

        for (i = 0; i < count; i++)
        {
            string show = "";

            while (true)
            {
                show = ConsoleHelper.ReadString(
                    "Название любимого шоу: ");

                if (!ContainsString(allShows, show))
                {
                    Console.WriteLine(
                        "Такого шоу нет в общем перечне.");
                }
                else if (favorites.Contains(show))
                {
                    Console.WriteLine(
                        "Это шоу уже было добавлено.");
                }
                else
                {
                    favorites.Add(show);
                    break;
                }
            }
        }

        return favorites;
    }

    private static bool ContainsString(
        List<string> list, string value)
    {
        int i = 0;

        for (i = 0; i < list.Count; i++)
        {
            if (string.Equals(
                list[i], value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
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
        Console.WriteLine("\n" + title);

        if (shows.Count == 0)
        {
            Console.WriteLine("Нет таких шоу.");
            return;
        }

        int i = 0;

        for (i = 0; i < shows.Count; i++)
        {
            Console.WriteLine("- " + shows[i]);
        }
    }

    public static void RunTask3()
    {
        Console.WriteLine(
            "\nЗадание 3. Телевизионные шоу");

        List<string> allShows = new List<string>();

        int showCount = ConsoleHelper.ReadPositiveInt(
            "Количество шоу в перечне: ");

        int i = 0;

        for (i = 0; i < showCount; i++)
        {
            string show = "";

            while (true)
            {
                show = ConsoleHelper.ReadString(
                    "Название шоу " + (i + 1) + ": ");

                if (ContainsString(allShows, show))
                {
                    Console.WriteLine(
                        "Такое название уже есть.");
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

        List<HashSet<string>> viewers =
            new List<HashSet<string>>();

        for (i = 0; i < viewerCount; i++)
        {
            Console.WriteLine("\nЗритель " + (i + 1));

            viewers.Add(
                ReadViewerFavorites(allShows, i + 1));
        }

        List<string> likedByAll;
        List<string> likedBySome;
        List<string> likedByNone;

        ClassifyShows(
            allShows,
            viewers,
            out likedByAll,
            out likedBySome,
            out likedByNone);

        PrintShows("Нравятся всем:", likedByAll);
        PrintShows("Нравятся некоторым:", likedBySome);
        PrintShows("Не нравятся никому:", likedByNone);
    }

    // =========================
    // ЗАДАНИЕ 4. HASHSET
    // =========================

    private static void FillTextFile(string path)
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

    private static HashSet<char> FindDigitsInFile(string path)
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
                    if (char.IsDigit(line[i]))
                    {
                        digits.Add(line[i]);
                    }
                }
            }
        }

        return digits;
    }

    private static void PrintTextFile(string path)
    {
        string line = "";

        Console.WriteLine("\nСодержимое файла:");

        using (StreamReader reader = new StreamReader(
            path, Encoding.UTF8))
        {
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    public static void RunTask4()
    {
        Console.WriteLine(
            "\nЗадание 4. Поиск цифр в текстовом файле");

        string path = "text4.txt";

        Console.WriteLine(
            "Файл будет создан по адресу: " +
            Path.GetFullPath(path));

        FillTextFile(path);
        PrintTextFile(path);

        HashSet<char> digits = FindDigitsInFile(path);

        Console.WriteLine("\nЦифры, которые встречаются в тексте:");

        if (digits.Count == 0)
        {
            Console.WriteLine("Цифр в тексте нет.");
        }
        else
        {
            foreach (char digit in digits)
            {
                Console.Write(digit + " ");
            }

            Console.WriteLine();
        }
    }

    // =========================
    // ЗАДАНИЕ 5. DICTIONARY
    // =========================

    private struct Person
    {
        public string LastName;
        public string FirstName;
        public DateTime BirthDate;
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
                Console.WriteLine("\nЧеловек " + (i + 1));

                lastName = ConsoleHelper.ReadStringMaxLength(
                    "Фамилия (до 20 символов): ", 20);

                firstName = ConsoleHelper.ReadStringMaxLength(
                    "Имя (до 20 символов): ", 20);

                birthDate = ConsoleHelper.ReadBirthDate(
                    "Дата рождения (ДД.ММ.ГГГГ): ");

                writer.WriteLine(
                    lastName + ";" +
                    firstName + ";" +
                    birthDate.ToString("dd.MM.yyyy"));
            }
        }
    }

    private static Dictionary<DateTime, List<Person>>
        ReadPeopleFromFile(string path)
    {
        Dictionary<DateTime, List<Person>> people =
            new Dictionary<DateTime, List<Person>>();

        string line = "";
        string[] parts = null;
        int count = 0;
        int i = 0;

        using (StreamReader reader = new StreamReader(
            path, Encoding.UTF8))
        {
            line = reader.ReadLine();

            if (line == null || !int.TryParse(line, out count))
            {
                throw new FormatException(
                    "Неверный формат файла с людьми.");
            }

            for (i = 0; i < count; i++)
            {
                line = reader.ReadLine();

                if (line == null)
                {
                    throw new FormatException(
                        "В файле недостаточно записей.");
                }

                parts = line.Split(';');

                if (parts.Length != 3)
                {
                    throw new FormatException(
                        "Неверная запись: " + line);
                }

                Person person = new Person();

                person.LastName = parts[0];
                person.FirstName = parts[1];

                if (person.LastName.Length > 20 ||
                    person.FirstName.Length > 20)
                {
                    throw new FormatException(
                        "Фамилия или имя длиннее 20 символов.");
                }

                if (!DateTime.TryParseExact(
                    parts[2],
                    "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out person.BirthDate))
                {
                    throw new FormatException(
                        "Неверная дата в файле: " + parts[2]);
                }

                if (person.BirthDate.Year < 1800 ||
                    person.BirthDate.Year > 2100)
                {
                    throw new FormatException(
                        "Год рождения должен быть от 1800 до 2100.");
                }

                if (!people.ContainsKey(person.BirthDate))
                {
                    people.Add(
                        person.BirthDate,
                        new List<Person>());
                }

                people[person.BirthDate].Add(person);
            }
        }

        return people;
    }

    private static DateTime FindOldestDate(
        Dictionary<DateTime, List<Person>> people)
    {
        DateTime oldestDate = DateTime.MaxValue;

        foreach (DateTime date in people.Keys)
        {
            if (date < oldestDate)
            {
                oldestDate = date;
            }
        }

        return oldestDate;
    }

    private static void PrintOldestPeople(
        Dictionary<DateTime, List<Person>> people,
        DateTime oldestDate)
    {
        List<Person> oldestPeople = people[oldestDate];

        Console.WriteLine(
            "\nСамая ранняя дата рождения: " +
            oldestDate.ToString("dd.MM.yyyy"));

        if (oldestPeople.Count == 1)
        {
            Console.WriteLine(
                oldestPeople[0].LastName + " " +
                oldestPeople[0].FirstName);
        }
        else
        {
            Console.WriteLine(
                "Количество людей с этой датой рождения: " +
                oldestPeople.Count);
        }
    }

    public static void RunTask5()
    {
        Console.WriteLine(
            "\nЗадание 5. Поиск самого старшего человека");

        string path = "people.txt";

        Console.WriteLine(
            "Исходный файл: " + Path.GetFullPath(path));

        FillPeopleFile(path);

        Dictionary<DateTime, List<Person>> people =
            ReadPeopleFromFile(path);

        DateTime oldestDate = FindOldestDate(people);

        PrintOldestPeople(people, oldestDate);
    }
}