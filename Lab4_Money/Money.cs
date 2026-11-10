using System;

internal class Money
{
    private uint _rubles;
    private byte _kopeks;

    public uint Rubles
    {
        get { return _rubles; }
        set { _rubles = value; }
    }

    public byte Kopeks
    {
        get { return _kopeks; }
        set
        {
            _rubles += (uint)(value / 100);
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
            return;
        }
        _rubles = other._rubles;
        _kopeks = other._kopeks;
    }

    public override string ToString()
    {
        return _rubles + " руб. " + 
               _kopeks + " коп.";
    }
    
    public static Money operator -(
        Money m, uint kopeks)
    {
        if (m == null)
        {
            return new Money(0, 0);
        }
        
        ulong totalKopeks = 
            (ulong)m._rubles * 100 + m._kopeks;
        ulong subtractKopeks = (ulong)kopeks;
        
        if (subtractKopeks > totalKopeks)
        {
            Console.WriteLine(
                "Ошибка: денежная величина " +
                "не может быть меньше 0.");
            return new Money(0, 0);
        }
        
        totalKopeks -= subtractKopeks;
        
        Money result = new Money();
        result._rubles = 
            (uint)(totalKopeks / 100);
        result._kopeks = 
            (byte)(totalKopeks % 100);
        return result;
    }

    public static Money operator -(
        uint kopeks, Money m)
    {
        return m - kopeks;
    }
    
    public static Money operator ++(Money m)
    {
        if (m == null)
        {
            return new Money(0, 1);
        }
        
        Money result = new Money(m);
        result._kopeks++;
        if (result._kopeks >= 100)
        {
            result._kopeks = 0;
            result._rubles++;
        }
        return result;
    }

    public static Money operator --(Money m)
    {
        if (m == null)
        {
            return new Money(0, 0);
        }
        
        Money result = new Money(m);
        
        if (result._rubles == 0 && 
            result._kopeks == 0)
        {
            Console.WriteLine(
                "Ошибка: денежная величина " +
                "не может быть меньше 0.");
            return result;
        }
        
        if (result._kopeks == 0)
        {
            result._rubles--;
            result._kopeks = 99;
        }
        else
        {
            result._kopeks--;
        }
        return result;
    }
    
    public static explicit operator uint(Money m)
    {
        if (m == null)
        {
            return 0;
        }
        return m._rubles;
    }
    
    public static implicit operator bool(Money m)
    {
        if (m == null)
        {
            return false;
        }
        return (m._rubles != 0 || 
                m._kopeks != 0);
    }
    
    public static Money operator -(
        Money m1, Money m2)
    {
        if (m1 == null || m2 == null)
        {
            return new Money(0, 0);
        }
        
        ulong total1 = 
            (ulong)m1._rubles * 100 + m1._kopeks;
        ulong total2 = 
            (ulong)m2._rubles * 100 + m2._kopeks;
        
        if (total2 > total1)
        {
            Console.WriteLine(
                "Ошибка: денежная величина " +
                "не может быть меньше 0.");
            return new Money(0, 0);
        }
        
        ulong resultKopeks = total1 - total2;
        Money result = new Money();
        result._rubles = 
            (uint)(resultKopeks / 100);
        result._kopeks = 
            (byte)(resultKopeks % 100);
        return result;
    }
}