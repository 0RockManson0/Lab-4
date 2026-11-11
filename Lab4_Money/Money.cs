using System;

internal class Money
{
    private uint _rubles;
    private byte _kopeks;

    // Свойство для рублей.

    public uint Rubles
    {
        get
        {
            return _rubles;
        }
        set
        {
            _rubles = value;
        }
    }

    // Свойство для копеек.
    // Если передать значение больше 99,
    // лишние копейки переводятся в рубли.

    public byte Kopeks
    {
        get
        {
            return _kopeks;
        }
        set
        {
            _rubles += (uint)(value / 100);
            _kopeks = (byte)(value % 100);
        }
    }

    // Конструктор без параметров.

    public Money()
    {
        _rubles = 0;
        _kopeks = 0;
    }

    // Конструктор с параметрами.

    public Money(uint rubles, byte kopeks)
    {
        _rubles = rubles;
        _kopeks = 0;
        Kopeks = kopeks;
    }

    // Конструктор копирования.

    public Money(Money other)
    {
        if (other == null)
        {
            _rubles = 0;
            _kopeks = 0;
        }
        else
        {
            _rubles = other._rubles;
            _kopeks = other._kopeks;
        }
    }

    // Перевод суммы в копейки.

    private ulong ToTotalKopeks()
    {
        return (ulong)_rubles * 100 + _kopeks;
    }

    // Создание Money из общего количества копеек.

    private static Money FromTotalKopeks(ulong total)
    {
        ulong rubles = total / 100;
        byte kopeks = (byte)(total % 100);

        if (rubles > uint.MaxValue)
        {
            throw new OverflowException(
                "Сумма превышает допустимое значение.");
        }

        return new Money((uint)rubles, kopeks);
    }

    // Метод задания 6.
    // Добавляет указанное количество копеек.
    // Исходный объект не изменяется.

    public Money AddKopeks(uint kopeks)
    {
        ulong total = ToTotalKopeks();

        total += kopeks;

        return FromTotalKopeks(total);
    }

    // Переопределение ToString.

    public override string ToString()
    {
        return _rubles + " руб. " +
               _kopeks + " коп.";
    }

    // =========================
    // ОПЕРАТОРЫ ЗАДАНИЯ 7
    // =========================

    // ++ добавляет одну копейку.

    public static Money operator ++(Money money)
    {
        if (money == null)
        {
            return new Money(0, 1);
        }

        ulong total = money.ToTotalKopeks();

        total++;

        return FromTotalKopeks(total);
    }

    // -- вычитает одну копейку.

    public static Money operator --(Money money)
    {
        if (money == null)
        {
            return new Money();
        }

        ulong total = money.ToTotalKopeks();

        if (total == 0)
        {
            Console.WriteLine(
                "Ошибка: сумма не может быть отрицательной.");

            return new Money(money);
        }

        total--;

        return FromTotalKopeks(total);
    }

    // Явное преобразование в uint.
    // Возвращается количество рублей,
    // копейки отбрасываются.

    public static explicit operator uint(Money money)
    {
        if (money == null)
        {
            return 0;
        }

        return money._rubles;
    }

    // Неявное преобразование в bool.
    // Если сумма не равна нулю, возвращается true.

    public static implicit operator bool(Money money)
    {
        if (money == null)
        {
            return false;
        }

        return money._rubles != 0 || money._kopeks != 0;
    }

    // Money - uint.
    // Число обозначает количество копеек.

    public static Money operator -(
        Money money, uint kopeks)
    {
        if (money == null)
        {
            return new Money();
        }

        ulong total = money.ToTotalKopeks();

        if (kopeks > total)
        {
            Console.WriteLine(
                "Ошибка: сумма не может быть отрицательной.");

            return new Money();
        }

        total -= kopeks;

        return FromTotalKopeks(total);
    }

    // uint - Money.
    // Левый операнд также обозначает копейки.

    public static Money operator -(
        uint kopeks, Money money)
    {
        if (money == null)
        {
            return FromTotalKopeks(kopeks);
        }

        ulong totalMoney = money.ToTotalKopeks();
        ulong totalKopeks = kopeks;

        if (totalMoney > totalKopeks)
        {
            Console.WriteLine(
                "Ошибка: результат не может быть отрицательным.");

            return new Money();
        }

        return FromTotalKopeks(totalKopeks - totalMoney);
    }

    // Money - Money.

    public static Money operator -(
        Money first, Money second)
    {
        if (first == null || second == null)
        {
            return new Money();
        }

        ulong firstTotal = first.ToTotalKopeks();
        ulong secondTotal = second.ToTotalKopeks();

        if (secondTotal > firstTotal)
        {
            Console.WriteLine(
                "Ошибка: сумма не может быть отрицательной.");

            return new Money();
        }

        return FromTotalKopeks(firstTotal - secondTotal);
    }
}