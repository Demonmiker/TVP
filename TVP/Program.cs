using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Console;

namespace TVP
{
    class Program
    {
        static void Main(string[] args)
        {
            Lab3();
        }

#region Lab2
        static string letters = "aabbccdfsg";
        static string[] tests = new[] { "ccf", "abg", "aac", "aaa", "abb", "acc", "acb", "acc", "bbb", "bac", "ccc", "cca", "ccb", "bcs", "asd", "dff" };

        static void Lab2()
        {
            Random rnd = new Random();
            string str = "";
            for (int i = 0; i < 50; i++) str += letters[rnd.Next(0, 9)];
            WriteLine($"str:{str}");
            foreach (var test in tests)
                WriteLine($"{test}:{Lab2(str, test)}");
            ReadKey();
        }
        static bool Lab2(string s, string sub) => s.Contains(sub);
        #endregion

        #region Lab3
        static bool IsCorrect(string s)
        {
            return Regex.IsMatch(s, @"^[a-zA-Z0-9\._-]+@[a-zA-Z0-9\._-]+\.[a-zA-Z]{2,4}$");
        }

        static void Lab3()
        {
            Write("Адрес: ");
            string address = ReadLine();
            WriteLine(IsCorrect(address));
            int[,] states = new int[,]
   {
                //s @ .
                {0,0,0},
                {2,0,2},
                {2,3,2},
                {4,0,0},
                {4,0,5},
                {6,0,0},
                {6,0,5}
   };
            int state = 1;
            for (int i = 0; i < address.Length; i++)
            {
                try
                {
                    state = states[state, getIndex(address[i])];
                }
                catch
                {
                    state = 0;
                }

            }
            if (state == 6)
                WriteLine("KA:Корректно");
            else
                WriteLine("KA:Не корректно");
            ReadKey();
        }

        static void Lab3FA()
        {
   
            ReadKey();


        }

        static int getIndex(char c)
        {
            if (Char.IsLetterOrDigit(c) || c == '_' || c == '-')
                return 0;
            else if (c == '@')
                return 1;
            else if (c == '.')
                return 2;
            else
                return -1;
        }

        
        #endregion

    }

    
}
