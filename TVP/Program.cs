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
            Lab7();
            ReadKey();
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

        #region Lab5
        static void Lab5()
        {
            string program = ReadLine() + '\0';
            StateType State = StateType.oD;
            int i = 0;
            char symbol;
            try
            {
                while ((symbol = program[i++]) != '\0')
                {
                    WriteLine($"ТС: {State}");
                    switch (State)
                    {
                        case StateType.oD: switch (symbol) {
                                case 'c': State = StateType.cD; break;
                                default: throw new Exception();
                            } break;
                        case StateType.cD: switch (symbol) {
                                case 'o': State = StateType.oD; break;
                                case 'U': State = StateType.cU; break;
                                default: throw new Exception();
                            } break;
                        case StateType.cU: switch (symbol) {
                                case 'D': State = StateType.cD; break;
                                case 'o': State = StateType.oU; break;
                                default: throw new Exception();
                            }
                            break;
                        case StateType.oU: switch (symbol) {
                                case 'c': State = StateType.cU; break;
                                default: throw new Exception();
                            } break;   
                    }
                }
                WriteLine($"ТС: {State}");
            }
            catch(Exception)
            {
                Console.WriteLine("Недопустимый переход");
                ReadKey();
            }
            ReadKey();
        }
        #endregion

        #region Lab7
        static void Lab7()
        {
            var ta = new TA(new ((char, char), string)[] {
                //oD
                (('0','c'),"1 +"),
                (('0',' '),"f ."),
                //cD
                (('1','o'),"0 +"),
                (('1','U'),"2 +"),
                (('1',' '),"f ."),
                //cU
                (('2','D'),"1 +"),
                (('2','o'),"3 +"),
                (('2',' '),"f ."),
                //oU
                (('3','c'),"2 +"),
                (('3',' '),"f ."),
            });
            WriteLine(ta.Solve("cUocсD", "f"));
                


        }
        #endregion
    }

    enum StateType {oD,cD,cU,oU};

    public class TA
    {
        Dictionary<(char, char), string> rules = new Dictionary<(char, char), string>();//q,a => q,b,+

        public TA(((char, char) qa, string r)[] arr)
        {
            foreach (var item in arr)
            {
                rules[item.qa] = item.r;
            }
        }
        public bool Solve(string s,string f)
        {
            char state = '0';
            StringBuilder sb = new StringBuilder(new string(' ',50)+s+ new string(' ', 50));
            int i = 50;
            while(true)
            {
                var buf = sb.ToString();
                WriteLine($"({state}) ...{buf.Substring(i - 10, 10)}[{buf[i]}]{buf.Substring(i + 1, 10)}...");
                if(rules.ContainsKey((state,sb[i])))
                {
                    var a = rules[(state, sb[i])];
                    state = a[0];
                    sb[i] = a[1];
                    if (a[2] == '+')
                        i++;
                    else if(a[2]=='-')
                        i--;
                }
                else
                {
                    return f.Contains(state);

                }
            }
        }
    }

    
}
