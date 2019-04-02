using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_1CS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("number1 ->");
            string n1 = Console.ReadLine();
            Console.WriteLine("number2 ->");
            string n2 = Console.ReadLine();
            mult(n1, n2);
            Console.ReadKey();
        }

        static void mult(string n1, string n2)
        {
            Int64 multiplier, multiplicand, product = 0;
            try
            {
                multiplier = Int32.Parse(n1);
                multiplicand = Int32.Parse(n2);
            }
            catch
            {
                Console.WriteLine("Error!");
                return;
            }
            for(int i = 1; i <= 32; i++)
            {
                Console.WriteLine("#{0}", i);
                Console.WriteLine("multiplier -> " + conv(Convert.ToString(multiplier, 2)));
                Console.WriteLine("multiplicand -> " + conv(Convert.ToString(multiplicand, 2)));
                short lsb = (short)(multiplier & 1);
                if (lsb == 1)
                {
                    Console.WriteLine("lsb = 1 -> product + multiplicand");
                    product += multiplicand;
                }
                multiplicand <<= 1;
                multiplier >>= 1;
            }
            Console.WriteLine("End calc!\ntoBase2 -> " + conv(Convert.ToString(product, 2)));
            Console.WriteLine("toBase10 -> " + Convert.ToString(product, 10));

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
