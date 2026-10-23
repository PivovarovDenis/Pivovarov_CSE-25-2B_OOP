
using System;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static int[,]? matrix;
    static int[][]? jagged;
    static string[] testStrings =
    {
        "В траве сидел кузнечик! Кузнечик не трогал козявок и дружил с мухом.",
        "Мама мыла раму. Анна читала книгу!",
        "Арбуз, дом; топот: шалаш."
    };

    static Random random = new Random();

    static void Message(params object[] values)
    {
        foreach (object value in values)
            Console.Write(value + " ");

        Console.WriteLine();
    }

    static int ReadInt(string prompt, int min = 0, int max = 10000)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) &&
                value >= min && value <= max)
                return value;

            Message($"Ошибка! Введите целое число от {min} до {max}.");
        }
    }

    static int[,] CreateMatrix(int rows, int cols, bool manual,
                               int min = 1, int max = 99)
    {
        int[,] a = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (manual)
                    a[i, j] = ReadInt($"a[{i},{j}] = ", -100000, 100000);
                else
                    a[i, j] = random.Next(min, max + 1);
            }
        }

        return a;
    }

    static void PrintArray(int[,]? a)
    {
        if (a == null || a.Length == 0)
        {
            Message("Двумерный массив пуст.");
            return;
        }

        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
                Console.Write($"{a[i, j],6}");

            Console.WriteLine();
        }
    }

    static void PrintArray(int[][]? a)
    {
        if (a == null || a.Length == 0)
        {
            Message("Рваный массив пуст.");
            return;
        }

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == null || a[i].Length == 0)
            {
                Console.WriteLine("(пустая строка массива)");
                continue;
            }

            foreach (int value in a[i])
                Console.Write($"{value,6}");

            Console.WriteLine();
        }
    }

    static int[,] DeleteEvenRows(int[,]? a)
    {
        if (a == null || a.Length == 0)
            throw new InvalidOperationException(
                "Нельзя удалять строки из пустого массива.");

        int rows = a.GetLength(0);
        int cols = a.GetLength(1);
        int newRows = (rows + 1) / 2;

        int[,] result = new int[newRows, cols];
        int target = 0;

        for (int i = 0; i < rows; i += 2)
        {
            for (int j = 0; j < cols; j++)
                result[target, j] = a[i, j];

            target++;
        }

        return result;
    }

    static int[,] DeleteLastRows(int[,]? a, int count)
    {
        if (a == null || a.Length == 0)
            throw new InvalidOperationException("Массив пуст.");

        int rows = a.GetLength(0);
        int cols = a.GetLength(1);

        if (count < 0 || count > rows)
            throw new ArgumentOutOfRangeException(
                nameof(count), "Количество удаляемых строк превышает размер массива.");

        int[,] result = new int[rows - count, cols];

        for (int i = 0; i < rows - count; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = a[i, j];

        return result;
    }

    static int[][] CreateJagged(int rows, bool manual,
                                int min = 1, int max = 99)
    {
        int[][] a = new int[rows][];

        for (int i = 0; i < rows; i++)
        {
            int length = ReadInt(
                $"Количество элементов в строке {i + 1}: ", 0, 1000);

            a[i] = new int[length];

            for (int j = 0; j < length; j++)
            {
                if (manual)
                    a[i][j] = ReadInt(
                        $"a[{i}][{j}] = ", -100000, 100000);
                else
                    a[i][j] = random.Next(min, max + 1);
            }
        }

        return a;
    }

    static int[][] AddRow(int[][]? a, int[] newRow)
    {
        if (a == null)
            a = Array.Empty<int[]>();

        int[][] result = new int[a.Length + 1][];

        for (int i = 0; i < a.Length; i++)
            result[i] = a[i];

        result[result.Length - 1] = newRow;
        return result;
    }

    static string ProcessString(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";

        string result = Regex.Replace(
            text,
            @"(?<![\p{L}\p{N}_])(\p{L})\p{L}*\1(?![\p{L}\p{N}_])",
            "",
            RegexOptions.IgnoreCase);

        result = Regex.Replace(result, @"[ \t]+", " ");
        result = Regex.Replace(result, @"\s+([,;:.!?])", "$1");
        result = Regex.Replace(result, @"([,;:])(?=\S)", "$1 ");
        result = Regex.Replace(result, @" {2,}", " ");

        return result.Trim();
    }

    static string ReadString()
    {
        Console.Write("Введите строку: ");
        return Console.ReadLine() ?? "";
    }

    static string ReadTestString()
    {
        Console.WriteLine("Выберите тестовую строку:");

        for (int i = 0; i < testStrings.Length; i++)
            Console.WriteLine($"{i + 1}. {testStrings[i]}");

        int choice = ReadInt("Номер строки: ", 1, testStrings.Length);
        return testStrings[choice - 1];
    }

    static void MatrixMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- ДВУМЕРНЫЙ МАССИВ ---");
            Console.WriteLine("1. Создать массив");
            Console.WriteLine("2. Вывести массив");
            Console.WriteLine("3. Удалить все четные строки");
            Console.WriteLine("4. Удалить указанное количество строк с конца");
            Console.WriteLine("0. Назад");

            int choice = ReadInt("Ваш выбор: ", 0, 4);

            try
            {
                switch (choice)
                {
                    case 1:
                        int rows = ReadInt("Количество строк: ", 1, 1000);
                        int cols = ReadInt("Количество столбцов: ", 1, 1000);

                        Console.WriteLine("1. Вручную");
                        Console.WriteLine("2. Случайными числами");
                        int mode = ReadInt("Способ заполнения: ", 1, 2);

                        matrix = CreateMatrix(rows, cols, mode == 1);
                        Message("Массив создан.");
                        PrintArray(matrix);
                        break;

                    case 2:
                        PrintArray(matrix);
                        break;

                    case 3:
                        matrix = DeleteEvenRows(matrix);
                        Message("Четные строки удалены.");
                        PrintArray(matrix);
                        break;

                    case 4:
                        if (matrix == null || matrix.Length == 0)
                            throw new InvalidOperationException("Массив пуст.");

                        int count = ReadInt(
                            "Сколько строк удалить с конца: ",
                            0, matrix.GetLength(0));

                        matrix = DeleteLastRows(matrix, count);
                        PrintArray(matrix);
                        break;

                    case 0:
                        return;
                }
            }
            catch (Exception ex) when (
                ex is InvalidOperationException ||
                ex is ArgumentOutOfRangeException)
            {
                Message("Ошибка:", ex.Message);
            }
        }
    }
]
    static void JaggedMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- РВАНЫЙ МАССИВ ---");
            Console.WriteLine("1. Создать массив");
            Console.WriteLine("2. Вывести массив");
            Console.WriteLine("3. Добавить строку в конец");
            Console.WriteLine("0. Назад");

            int choice = ReadInt("Ваш выбор: ", 0, 3);

            switch (choice)
            {
                case 1:
                    int rows = ReadInt("Количество строк: ", 1, 1000);

                    Console.WriteLine("1. Вручную");
                    Console.WriteLine("2. Случайными числами");
                    int mode = ReadInt("Способ заполнения: ", 1, 2);

                    jagged = CreateJagged(rows, mode == 1);
                    Message("Рваный массив создан.");
                    PrintArray(jagged);
                    break;

                case 2:
                    PrintArray(jagged);
                    break;

                case 3:
                    Console.WriteLine(
                        "Введите новую строку массива.");

                    int length = ReadInt("Количество элементов: ", 0, 1000);
                    int[] newRow = new int[length];

                    Console.WriteLine("1. Вручную");
                    Console.WriteLine("2. Случайными числами");
                    int fillMode = ReadInt("Способ заполнения: ", 1, 2);

                    for (int j = 0; j < length; j++)
                    {
                        newRow[j] = fillMode == 1
                            ? ReadInt($"Элемент {j + 1}: ", -100000, 100000)
                            : random.Next(1, 100);
                    }

                    jagged = AddRow(jagged, newRow);
                    Message("Строка добавлена.");
                    PrintArray(jagged);
                    break;

                case 0:
                    return;
            }
        }
    }

    static void StringMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- ОБРАБОТКА СТРОКИ ---");
            Console.WriteLine("1. Ввести строку с клавиатуры");
            Console.WriteLine("2. Выбрать тестовую строку");
            Console.WriteLine("0. Назад");

            int choice = ReadInt("Ваш выбор: ", 0, 2);

            if (choice == 0)
                return;

            string text = choice == 1
                ? ReadString()
                : ReadTestString();

            Console.WriteLine("\nИсходная строка:");
            Console.WriteLine(string.IsNullOrEmpty(text)
                ? "(строка пустая)"
                : text);

            string result = ProcessString(text);

            Console.WriteLine("\nРезультат обработки:");
            Console.WriteLine(string.IsNullOrEmpty(result)
                ? "(строка пустая)"
                : result);
        }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n========== ГЛАВНОЕ МЕНЮ ==========");
            Console.WriteLine("1. Двумерный массив");
            Console.WriteLine("2. Рваный массив");
            Console.WriteLine("3. Обработка строки");
            Console.WriteLine("0. Выход");

            int choice = ReadInt("Ваш выбор: ", 0, 3);

            switch (choice)
            {
                case 1:
                    MatrixMenu();
                    break;

                case 2:
                    JaggedMenu();
                    break;

                case 3:
                    StringMenu();
                    break;

                case 0:
                    Console.WriteLine("Программа завершена.");
                    return;
            }
        }
    }
}
