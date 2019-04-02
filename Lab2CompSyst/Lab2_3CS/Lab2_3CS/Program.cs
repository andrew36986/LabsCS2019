using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_3CS
{
    class Program
    {
        static void Main(string[] args)
        {
            float n1, n2;
            Console.WriteLine("float1:");
            n1 = float.Parse(Console.ReadLine());
            Console.WriteLine("float2:");
            n2 = float.Parse(Console.ReadLine());
            sum(n1, n2);
            Console.ReadKey();
        }
        static void sum(float n1, float n2)
        {
            bool check = n1 * n2 >= 0;
            if (Math.Abs(n2) > Math.Abs(n1))
                (n1, n2) = (n2, n1);
            Console.WriteLine(n1 + "+" + n2);
            int n1Mark = n1 < 0 ? 1 : 0;
            int n2Mark = n2 < 0 ? 1 : 0;
            (n1, n2) = (Math.Abs(n1), Math.Abs(n2));
            int n1total = (int)n1;
            int n2total = (int)n2;
            n1 -= n1total;
            n2 -= n2total;
            totalBits(n1, out Int32 n1TotalBits);
            totalBits(n2, out Int32 n2TotalBits);
            Console.WriteLine("#1 n1 -> " + toBinStr(n1Mark, 0, n1TotalBits));
            Console.WriteLine("#2 n2 -> " + toBinStr(n2Mark, 0, n2TotalBits));
            Int16 n1Exp = norm(n1total, ref n1TotalBits);
            Int16 n2Exp = norm(n2total, ref n2TotalBits);
            byte expN1 = (byte)(n1Exp + Math.Pow(2, 7) - 1);
            byte expN2 = (byte)(n2Exp + Math.Pow(2, 7) - 1);
            string n1TotBitStr = toBinStr(n1Mark, expN1, n1TotalBits);
            Console.WriteLine("#3 normalize n1 -> " + n1TotBitStr);
            Console.WriteLine("#4 normalize n2 -> " + toBinStr(n2Mark, expN2, n2TotalBits));
            n2TotalBits >>= n1Exp - n2Exp;
            string n2TotBitStr = toBinStr(n2Mark, expN2, n2TotalBits);
            Console.WriteLine("#5 n2 <<= " + (n1Exp - n2Exp) + " -> " + n2TotBitStr);
            Console.WriteLine("#6 " + n1TotBitStr + " + " + n2TotBitStr);
            Int32 answ = check ? n1TotalBits + n2TotalBits : n1TotalBits - n2TotalBits;
            normAnsw(ref answ, ref n1Exp, check);
            expN1 = (byte)(n1Exp + Math.Pow(2, 7) - 1);
            Console.WriteLine("answer:\nin decimal -> {0}\nin binary -> {1}",
                toDec(n1Exp, answ, n1Mark), toBinStr(n1Mark, expN1, answ));
        }
        static void totalBits(float val, out Int32 flBits)
        {
            const int manBits = 23;
            int i = 0;

            flBits = 0;
            while (val != 0 && i < 22)
            {
                val *= 2;
                if (val >= 1)
                {
                    flBits |= 1;
                    val -= 1;
                }
                i++;
                flBits <<= 1;
            }
            flBits <<= manBits - (i + 1);
        }
        static string toBinStr(int markBit, byte exp, Int32 answ)
        {
            string answStr = markBit + "_";
            for (int i = 7; i >= 0; --i) answStr += exp >> i & 1;
            answStr += "_";
            for (int i = 22; i >= 0; --i) answStr += answ >> i & 1;

            return answStr;
        }
        static Int16 norm(int val, ref Int32 flBits)
        {
            Int32 one = 1 << 23;
            Int16 exp = 0;
            if (val > 0)
            {
                while (val > 1)
                {
                    ++exp;
                    flBits >>= 1;
                    flBits |= (val & 1) << 22;
                    val >>= 1;
                }
                flBits |= one;
            }
            else
            {
                if (flBits == 0)
                    exp = (Int16)(-Math.Pow(2, 7) + 1);
                else
                    do
                    {
                        --exp;
                        flBits <<= 1;
                    } while ((flBits & one) != one);
            }

            return exp;
        }
        static void normAnsw(ref Int32 answ, ref Int16 exp, bool check)
        {
            Int32 one = 1 << 23;
            if ((answ & one) == one) return;
            if (check)
            {
                do
                {
                    ++exp;
                    answ >>= 1;
                }
                while ((answ & one) != one);
            }
            else
            {
                do
                {
                    --exp;
                    answ <<= 1;
                }
                while ((answ & one) != one);
            }
        }
        static float toDec(Int16 exp_a, Int32 man, int markBit)
        {
            float answ = 0,
                mult = (float)Math.Pow(2, exp_a);
            for (int i = 23; i >= 0; --i, mult /= 2)
                answ += mult * (man >> i & 1);
            if (markBit == 1)
                answ = -answ;
            return answ;
        }
    }
}
