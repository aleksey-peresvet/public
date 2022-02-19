using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace task3
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

            if (args.Length != 2)
            {
                Console.WriteLine("Некорректный ввод аргументов!");
                return;
            }
            Get_Report(args[0], args[1]);
        }


        private static void Get_Report(string file1, string file2)
        {
            StreamReader sr_tests, sr_values;
            //
            try
            {
                sr_tests = new StreamReader(file1);
            }
            catch
            {
                Console.WriteLine("Файл, указанный в качестве первого аргумента не существует!");
                return;
            }
            //
            try
            {
                sr_values = new StreamReader(file2);
            }
            catch
            {
                Console.WriteLine("Файл, указанный в качестве второго аргумента не существует!");
                return;
            }
            //
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            string temp = sr_tests.ReadLine();
            while (temp != null)
            {
                list1.Add(temp);
                temp = sr_tests.ReadLine();
            }
            temp = sr_values.ReadLine();
            while (temp != null)
            {
                list2.Add(temp);
                temp = sr_values.ReadLine();
            }
            sr_tests.Dispose();
            sr_values.Dispose();
            string[] split_tests;
            string[] split_values;
            for(int i = 0; i < list1.Count; i++)
            {
                if(list1[i].Contains("id"))
                {
                    for (int j = 0; j < list2.Count; j++)
                    {
                        if (Get_ID(list2[j]) == Get_ID(list1[i]))
                        {
                            split_values = list2[j + 1].Split(':');
                            split_tests = list1[i + 2].Split(':');
                            temp = split_tests[0] + ':' + split_values[1];
                            list1.RemoveAt(i + 2);
                            list1.Insert(i + 2, temp);
                            break;
                        }
                    }
                }
            }

            StreamWriter sw = new StreamWriter("report.json");
            for (int i = 0; i < list1.Count; i++)
            {
                sw.WriteLine(list1[i]); 
            }
            sw.Dispose();
        }

        private static int Get_ID(string str)
        {
            string temp = "";
            int result;
            for (int i = 0; i < str.Length; i++)
            {
                if(Char.IsNumber(str, i))
                {
                    temp += str[i];
                }
            }
            try
            {
                result = Convert.ToInt32(temp);
            }
            catch
            {
                return 0;
            }
            return result;
        }

        private static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("-help - выводит этот блок информации.");
            Console.WriteLine("Пример: task.exe test.json values.json");
        }
    }
}