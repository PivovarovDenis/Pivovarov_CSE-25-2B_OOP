using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Globalization;

class Programm
{
    static double ReadDouble(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (float.TryParse(input,
                               NumberStyles.Float,
                               CultureInfo.InvariantCulture, 
                               out float result)) return result;

            Console.WriteLine("Ошибка: Введите число.");
        }
    }

    static void PrintCounters(double n, double m)
    {
        Console.WriteLine($"Текущие значения: n = {n}, m = {m}.");
    }

    static bool IsInArea(double hypotenuse, double X1, double Y1)
    {
        if (hypotenuse <= 1)
        {
            if (X1 <= 0 || Y1 <= 0)
            {
                Console.WriteLine($"Точка с координатами ({Math.Round(X1, 3)}, {Math.Round(Y1, 3)}) входит в заштрихованную область.");
                return true;
            }
            
        }
        Console.WriteLine($"Точка с координатами ({Math.Round(X1, 3)}, {Math.Round(Y1, 3)}) не входит в заштрихованную область.");
        return false;

    }

    static void Main(string[] args)
    {
        Console.WriteLine("Задача № 1:");
        double n, m, x;
        double firstResult;
        bool result;
        // Задача 1.1
        n = ReadDouble("Введите n:");
        m = ReadDouble("Введите m:");

        if (m - 1 != 0)
        {
            firstResult = Math.Round(n++ / --m, 3);
            Console.WriteLine($"\n1) n++ / --m = {firstResult}.");
            PrintCounters(n, m);
        }
        else Console.WriteLine("\n1) Ошибка, вычисление невозможно: --m = 0, делить на 0 нельзя.");

        // Задача 1.2
        n = ReadDouble("\nВведите n:");
        m = ReadDouble("Введите m:");

        if (m != 0)
        {
            result = n-- > n / m++;
            Console.WriteLine($"\n2) n-- > n / m++ = {result}.");
            PrintCounters(n, m);
        }
        else Console.WriteLine("\n2) Ошибка, вычисление невозможно: m = 0, делить на 0 нельзя.");

        // Задача 1.3
        n = ReadDouble("\nВведите n:");
        m = ReadDouble("Введите m:");

        result = m < n++;

        Console.WriteLine($"\n3) m < n++ = {result}.");
        PrintCounters(n, m);

        // Задача 1.4
        x = ReadDouble("\nВведите x:");

        double sinus = Math.Sin(x);
        double cosinus = Math.Cos(x);

        Console.WriteLine($"\n4) 1 + x * cos(x)^2 + sin(x)^3 = {1 + x * Math.Pow(cosinus, 2) + Math.Pow(sinus, 3)}.");
        Console.WriteLine($"x = {x}.");

        // Задача 2
        Console.WriteLine("\nЗадача № 2:");

        double X1, Y1;

        X1 = ReadDouble("\nВведите X:");
        Y1 = ReadDouble("Введите Y:");

        double hypotenuse = Math.Sqrt(X1 * X1 + Y1 * Y1);

        result = IsInArea(hypotenuse, X1, Y1);

        // Задача 3
        Console.WriteLine("\nЗадача № 3:");

        const float fla = 1000f, flb = 0.0001f;
        float flResult;
        const double dbla = 1000, dblb = 0.0001;
        double dblResult;

        flResult = ((float)Math.Pow(fla - flb, 4) - ((float)Math.Pow(fla, 4) - 4 * (float)Math.Pow(fla, 3) * flb))
            / (6 * fla * fla * flb * flb - 4 * fla * (float)Math.Pow(flb, 3) + (float)Math.Pow(flb, 4));                // Формула из варианта задания для float 

        dblResult = (Math.Pow(dbla - dblb, 4) - (Math.Pow(dbla, 4) - 4 * Math.Pow(dbla, 3) * dblb))
            / (6 * dbla * dbla * dblb * dblb - 4 * dbla * Math.Pow(dblb, 3) + Math.Pow(dblb, 4));               // Формула из варианта задания для double

        Console.WriteLine($"\nЗначение при a и b - float: {flResult}.");
        Console.WriteLine($"Значение при a и b - double: {dblResult}.");
    }
}
