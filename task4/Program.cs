using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("-help") && args.Length > 1)
            {
                Console.WriteLine("Указаны несовместимые флаги!");
                return;
            }

            if (args.Length == 0 || args[0] == "-help")
            {
                ShowHelp();
                return;
            }

            if (args.Length != 1)
            {
                Console.WriteLine("Некорректный ввод аргумента!");
                return;
            }

            Lets_calculate(args[0]);
        }

        private static void Lets_calculate(string file)
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(file);
            }
            catch
            {
                Console.WriteLine("Ошибка чтения файла!");
                return;
            }

            List<string> list = new List<string>();
            string temp = sr.ReadLine();
            while(temp!=null)
            {
                list.Add(temp);
                temp = sr.ReadLine();
            }
            sr.Dispose();
            int unit = 0;
            int[] units = new int[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    units[i] = Convert.ToInt32(list[i]);
                    
                }
                catch
                {
                    Console.WriteLine("Ошибка чтения числа массива в строке " + 
                        i.ToString() + "!");
                }
                unit += units[i];
            }
            
            float middle = (float)(unit / list.Count);
            float result = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if(units[i]>middle)
                result += (float)(units[i] - middle);
                else result += (float)(middle - units[i]);
            }
            Console.WriteLine(result);
        }

        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("-help - выводит этот блок информации.");
            Console.WriteLine("Пример: task.exe file");
        }
    }
}
