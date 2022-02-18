using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace task2
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
            
            if (args.Length == 0 || args[0]=="-help")
            {
                ShowHelp();
                return;
            }
            
            if (args.Length != 2)
            {
                Console.WriteLine("Некорректный ввод аргументов!");
                return;
            }
            Get_Result(args[0], args[1]);
            
        }

        private static void Get_Result(string file1, string file2)
        {
            FileStream fs1, fs2;
            //
            try
            {
                fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                Console.WriteLine("Файл, указанный в качестве первого аргумента не существует!");
                return;
            }
            //
            try
            {
                fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                Console.WriteLine("Файл, указанный в качестве второго аргумента не существует!");
                return;
            }
            fs1.Dispose();
            fs2.Dispose();
            //
            StreamReader sr1 = new StreamReader(file1);
            StreamReader sr2 = new StreamReader(file2);
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            string temp = sr1.ReadLine();
            while (temp != null)
            {
                list1.Add(temp);
                temp = sr1.ReadLine();
            }
            temp = sr2.ReadLine();
            while (temp != null)
            {
                list2.Add(temp);
                temp=sr2.ReadLine();
            }
            sr1.Dispose();
            sr2.Dispose();
            float cx = 0, cy = 0, r = 0;
            try
            {
                cx = float.Parse((list1[0].Split(' '))[0]);
            }
            catch
            {
                Console.WriteLine("Ошибка чтения координаты Х центра окружности!");
            }
            try
            {
                cy = float.Parse((list1[0].Split(' '))[1]);
            }
            catch
            {
                Console.WriteLine("Ошибка  чтения координаты Y центра окружности!");
            }
            try
            {
                r = float.Parse(list1[1]);
            }
            catch
            {
                Console.WriteLine("Ошибка чтения значения радиуса окружности");
            }
            float px = 0, py = 0;
            Console.WriteLine("Радиус окружности = " + r.ToString());
            Console.WriteLine("Координаты центра окружности = " + cx.ToString() + "," + cy.ToString()); 
            Console.WriteLine("Координаты точек и результаты вычисления:");
            for (int i = 0; i < list2.Count; i++)
            {
                try
                {
                    px = float.Parse((list2[i].Split(' '))[0]);
                }
                catch
                {
                    Console.WriteLine("Ошибка чтения координаты точки по оси Х!");
                }
                try
                {
                    py = float.Parse((list2[i].Split(' '))[1]);
                }
                catch
                {
                    Console.WriteLine("Ошибка чтения координаты точки по оси Y!");
                }
                Console.WriteLine(Formatting_result_string(px.ToString() +
                    "," + py.ToString() + " " + Analize(cx, cy, r, px, py)));
            }

        }

        private static string Formatting_result_string(string str)
        {
            int num = 20 - str.Length;
            if (num < 0) num = 0;
            string temp = str.Substring(str.Length - 1, 1);
            str = str.Substring(0, str.Length-2) + Get_Spaces(num) + temp;
            return str;
        }

        private static string Get_Spaces(int num)
        {
            string str = "";
            for(int i = 0; i < num; i++)
            {
                str+=" ";
            }
            return str;
        }

        private static string Analize(float centr_x, float centr_y, float radius, float point_x, float point_y)
        {
            float temp = (point_x - centr_x) * (point_x - centr_x) +
                         (point_y - centr_y) * (point_y - centr_y);
            temp = (float)Math.Pow(temp, 0.5);
            if (temp < radius) return "1";
            if (temp > radius) return "2";
            if (temp == radius) return "0";
            return "!";
        }
        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("1 аргумент - файл с координатами и радиусом окружности");
            Console.WriteLine("2 аргумент - файл с координатами точек");
            Console.WriteLine("-help - выводит этот блок информации.");
            Console.WriteLine("Пример: task.exe file1 file2");
        }
    }
}
