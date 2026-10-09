using System;

class Programm
{
    static void Main(string[] args)
    {
        const double E = 0.0001;
        double X, Y, SN = 0, SE = 0;
        const int N = 30;

        for (X = 0.1; X <= 1; X += 0.1)
        {   
            for (int i = 1; i <= N; i++) SN += Math.Pow(-1, i + 1)
                    * Math.Pow(X, 2 * i + 1) / (4 * i * i - 1);             // Рассчет суммы с заданным кол-вом

            double term = Math.Pow(X, 3) / 3;
            int n = 1;

            while (term >= E)
            {
                SE += term;
                ++n;
                term = Math.Pow(-1, n + 1) * Math.Pow(X, 2 * n + 1) / (4 * n * n - 1);         // Рассчет суммы по приближениям
            }

            Y = (1 + X * X) / 2 * Math.Atan(X) - X / 2;

            Console.WriteLine($"{Math.Round(X * 10, 0)}) X = {Math.Round(X, 1),-15:F8} SN = {SN, -15:F8} SE = {SE, -15:F8} Y = {Y, -15:F8}");
        }
    }
}
