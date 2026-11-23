using System;

internal class Money
{
    private uint _rubles;
    private byte _kopeks;


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


    public byte Kopeks
    {
        get
        {
            return _kopeks;
        }

        set
        {
            ulong totalRubles =
                (ulong)_rubles + (uint)(value / 100);
            if (totalRubles > uint.MaxValue)
            {
                throw new OverflowException(
                    "Сумма превышает " +
                    "допустимое значение.");
            }

            _rubles = (uint)totalRubles;
            _kopeks = (byte)(value % 100);
        }
    }


    public Money()
    {
        _rubles = 0;
        _kopeks = 0;
    }


    public Money(uint rubles, byte kopeks)
    {
        _rubles = rubles;
        _kopeks = 0;
        Kopeks = kopeks;
    }


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
    
    private ulong ToTotalKopeks()
    {
        return (ulong)_rubles * 100 + _kopeks;
    }
    
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

    // МЕТОД ЗАДАНИЯ 6 

    public Money SubtractKopeks(uint kopeks)
    {
        ulong total = ToTotalKopeks();

        if (kopeks > total)
        {
            Console.WriteLine(
                "Ошибка: сумма не может быть " +
                "отрицательной.");
            return new Money();
        }

        return FromTotalKopeks(total - kopeks);
    }


    public override string ToString()
    {
        return _rubles + " руб. " + _kopeks + " коп.";
    }


    // ОПЕРАТОРЫ ЗАДАНИЯ 7

    public static Money operator ++(Money money)
    {
        if (money == null)
        {
            return new Money(0, 1);
        }

        return FromTotalKopeks(
            money.ToTotalKopeks() + 1);
    }
    
    public static Money operator --(Money money)
    {
        if (money == null)
        {
            return new Money();
        }

        return money.SubtractKopeks(1);
    }


    public static explicit operator uint(Money money)
    {
        if (money == null)
        {
            return 0;
        }

        return money._rubles;
    }

    public static implicit operator bool(Money money)
    {
        if (money == null)
        {
            return false;
        }

        return money._rubles != 0 || money._kopeks != 0;
    }
    
    public static Money operator -(
        Money money, uint kopeks)
    {
        if (money == null)
        {
            return new Money();
        }

        return money.SubtractKopeks(kopeks);
    }
    
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
                "Ошибка: результат не может быть " +
                "отрицательным.");
            return new Money();
        }

        return FromTotalKopeks(totalKopeks - totalMoney);
    }
    
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
                "Ошибка: сумма не может быть " +
                "отрицательной.");
            return new Money();
        }

        return FromTotalKopeks(firstTotal - secondTotal);
    }
}