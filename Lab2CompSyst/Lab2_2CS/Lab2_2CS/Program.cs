using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2CS
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1, n2;
            Console.WriteLine("number1 ->");
            
            int.TryParse(Console.ReadLine(), out n1);
            Console.WriteLine("number2 ->");
            int.TryParse(Console.ReadLine(), out n2);
            div(n1, n2);
            Console.ReadKey();
        }

        static void div(int n1, int n2)
        {
            int l;
            long divisor, remainder, quot = 0;
            try
            {
                remainder = (long)Math.Abs(n1);
                divisor = ((long)Math.Abs(n2)) << 32;
                Console.WriteLine("remainder -> " + Convert.ToString(n1, 2));
                Console.WriteLine("divisor -> " + Convert.ToString(n2, 2));
            }
            catch
            {
                Console.WriteLine("Error!");
                return;
            }
            for (l = 0; n1 != 0; n1 >>= 1, l++) ;
            remainder <<= (32 - (l + 1));
            for (int i = 0; i < l + 1; i++) 
            {
                remainder <<= 1;
                Console.WriteLine("(<<= 1)remainder - > " + Convert.ToString(remainder, 2));
                remainder -= divisor;
                Console.WriteLine("remainder-divisor -> " + Convert.ToString(remainder, 2));
                quot <<= 1;
                Console.WriteLine("quotient <<= 1");
                if(remainder < 0)
                {
                    remainder += divisor;
                    Console.WriteLine("remainder < 0 -> remainder + divisor -> " + Convert.ToString(remainder, 2));
                }
                else
                {
                    quot++;
                    Console.WriteLine("quotient + 1 -> " + Convert.ToString(quot, 2));
                }
            }
            remainder >>= 32;
            Console.WriteLine("remainder - > " + Convert.ToString(remainder,2) + " or " + remainder);
            Console.WriteLine("quotient - >" + Convert.ToString(quot, 2) + " or " + quot);
        }
        static string conv(string n)
        {
            int c = 64 - n.Length;
            string zeros = "";
            for (int i = 0; i < c; ++i)
                zeros += "0";
            return zeros + n;
        }
    }
}
