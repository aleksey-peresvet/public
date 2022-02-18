using System;
using System.Linq;

namespace task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 0, m = 0;
            if (args.Contains("-help") && args.Length > 1)
            {
                Console.WriteLine("Указаны несовместимые флаги!");
                return;
            }
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-help")
                {
                    ShowHelp();
                    return;
                }
                if (args[i] == "-n")
                {
                    try
                    {
                        n = int.Parse(args[i + 1]);
                    }
                    catch
                    {
                        Console.WriteLine("Указана некорректная длина периода массива!");
                        return;
                    }
                }

                if (args[i] == "-m")
                {
                    try
                    {
                        m = int.Parse(args[i + 1]);
                    }
                    catch
                    {
                        Console.WriteLine("Указана некорректная длина интервала!");
                        return;
                    }
                }
            }
            Console.WriteLine(GetResult(n, m));
        }

        private static string GetResult(int n, int m)
        {
            string str_n = " периода массива!", str_m = " интервала!";
            string error_zero_value = "Не указано значение длины";
            string error_negative_value = "Указана отрицательная величина";
            if (n == 0) return error_zero_value + str_n;
            if (m == 0) return error_zero_value + str_m;
            if (n < 0) return error_negative_value + str_n;
            if (m < 0) return error_negative_value + str_m;
            if (m == 1 || n == 1) return "1";
            str_n = Get_Next_Period(n, m, 1);
            string the_way = "1";
            while (str_n[m - 1] != '1')
            {
                the_way += str_n[m - 1].ToString();
                str_n = Get_Next_Period(n, m, int.Parse(str_n[m - 1].ToString()));
            }
            return the_way;
        }

        private static string Get_Next_Period(int period, int interval, int start)
        {
            string str = "";
            int count;
            if (period > interval) count = period;
            else count = interval;
            int temp = 1;
            for (int i = 0; i < count; i++)
            {
                if (i + start <= period) str += (i + start).ToString();
                else
                {
                    str += (i + start - period * temp).ToString();
                    temp = (i + start) / period;
                }
            }
            return str;
        }

        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("-n - флаг задает длину периода кругового массива, принимает целые положительные числа.");
            Console.WriteLine("-m - флаг задает длину интервала для обработки кругового массива, принимает целые " +
                '\n' + "положительные числа.");
            Console.WriteLine("-help - выводит этот блок информации.");
            Console.WriteLine("Пример: task.exe -n 6 -m 5");
        }

    }
}
