using System.ComponentModel;

namespace Seminar_6;

internal class Program
{
    private static void Main(string[] args)
    {
        ICalc calculator = new Calculator();

        calculator.GotResult += Calculator_GotResult!;
        Console.WriteLine("Оставьте поле пустым");
        bool exit = true;
        while (exit)
        {
            Console.WriteLine("Введите операцию вида: операция число или \"Отмена\" для отмены последней операции");
            string input = Console.ReadLine()!;
            string[] parts = input.Split(' ');
            if (parts.Length != 2)
            {
                if (input == "")
                {
                    Console.WriteLine("Спасибо за использование калькулятора!");
                    exit = false;
                    break;
                }
                if (parts[0] != "Отмена")
                {
                    Console.WriteLine("Неверное количество аргументов.");
                    continue;
                }
            }
            double num = default;
            if (parts.Count() > 1)
            {
                try
                {
                    num = double.Parse(parts[1]);
          
                } catch (Exception e)
                {
                    Console.WriteLine("Неверное значение.");
                    continue;
                }
            }
            Action action = parts[0] switch
            {
                "+" => () => calculator.Add(num),
                "-" => () => calculator.Sub(num),
                "*" => () => calculator.Mul(num),
                "/" => () => calculator.Div(num),
                "Отмена" => () => calculator.CancelLast(),
                _ => () => Console.WriteLine("Неверная операция.")
            };
            try { action(); } catch (Exception e) { Console.WriteLine(e); }
        }

    }
   /* private static bool ParseNumber(string input,out double doubleValue)
    {
        doubleValue = 0.0;


        if (double.TryParse(input, out doubleValue))
        {
            return true;
        }

        throw new InvalidOperationException("Аргументом должно быть число!");
    }*/
    static void Calculator_GotResult(object sender, EventArgs e)
    {
        Console.WriteLine(((Calculator)sender).Result);
    }
}