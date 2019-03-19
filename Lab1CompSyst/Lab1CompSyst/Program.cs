using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1CompSyst
{
    class Program
    {
        static void Main(string[] args)
        {
            string f1 = @"file1.txt";
            string f2 = @"file2.txt";
            string f3 = @"file3.txt";
            workWorkWork(f1);
            workWorkWork(f2);
            workWorkWork(f3);
            Console.ReadKey();
        }

        static void workWorkWork(string f)
        {
            firstPart(f);
            Console.WriteLine("{0} -> Base64\n", f);
            string fileText = System.IO.File.ReadAllText(f, Encoding.GetEncoding(1251));
            byte[] array = Encoding.UTF8.GetBytes(fileText);
            base64Encoder encoder = new base64Encoder(array);
            Console.WriteLine(encoder.getEncoded());
            Console.WriteLine("\n------------------------------------------------------\n");
        }

        static void firstPart(string f)
        {
            List<char> chars = new List<char>();
            List<int> num = new List<int>();
            double sumOfsym = 0;
            double frequency = 0;
            double entropy = 0;
            using (StreamReader sr = new StreamReader(f, Encoding.GetEncoding(1251)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int count = 1;
                    foreach (char ch in line)
                    {
                        if (chars.Count == 0)
                        {
                            chars.Add(ch);
                            num.Add(count);
                        }
                        else
                        {
                            for (int i = 0; i < chars.Count; i++)
                            {
                                if (chars[i] == ch)
                                {
                                    num[i] += count;
                                    count *= -1;
                                }
                            }
                            if (count > 0)
                            {
                                chars.Add(ch);
                                num.Add(count);
                            }

                        }
                        count = 1;
                    }
                }
                for (int i = 0; i < chars.Count; i++)
                {
                    sumOfsym += num[i];
                }
                Console.WriteLine(f);
                Console.WriteLine("\nОбщее кол-во символов в тексте -> {0}", sumOfsym);
                for (int i = 0; i < chars.Count; i++)
                {
                    frequency = num[i] / sumOfsym;
                    Console.WriteLine("Относительная частота появления символа '{0}' = {1:f3}%", chars[i], frequency * 100);
                    if (frequency != 0)
                    {
                        entropy += frequency * (Math.Log(1 / frequency, 2));
                    }
                }
                Console.WriteLine("\nСредняя энтропия алфавита в заданом тексте -> {0:f3} бит", entropy);
                Console.WriteLine("Количество информации в тексте -> {0:f3} байт\n", (entropy * sumOfsym) / 8);
            }
        }
    }

    public class base64Encoder
    {
        byte[] source1;
        int length1, length2;
        int block;
        int padding;
        public base64Encoder(byte[] arr)
        {
            source1 = arr;
            length1 = arr.Length;
            if ((length1 % 3) == 0)
            {
                padding = 0;
                block = length1 / 3;
            }
            else
            {
                padding = 3 - (length1 % 3);
                block = (length1 + padding) / 3;
            }
            length2 = length1 + padding;
        }

        public char[] getEncoded()
        {
            byte[] source2;
            source2 = new byte[length2];
            for (int x = 0; x < length2; x++)
            {
                if (x < length1)
                {
                    source2[x] = source1[x];
                }
                else
                {
                    source2[x] = 0;
                }
            }

            byte byte1, byte2, byte3;
            byte temp0, temp1, temp2, temp3, temp4;
            byte[] stack = new byte[block * 4];
            char[] result = new char[block * 4];
            for (int x = 0; x < block; x++)
            {
                byte1 = source2[x * 3];
                byte2 = source2[x * 3 + 1];
                byte3 = source2[x * 3 + 2];

                temp1 = (byte)((byte1 & 252) >> 2);

                temp0 = (byte)((byte1 & 3) << 4);
                temp2 = (byte)((byte2 & 240) >> 4);
                temp2 += temp0;

                temp0 = (byte)((byte2 & 15) << 2);
                temp3 = (byte)((byte3 & 192) >> 6);
                temp3 += temp0;

                temp4 = (byte)(byte3 & 63);

                stack[x * 4] = temp1;
                stack[x * 4 + 1] = temp2;
                stack[x * 4 + 2] = temp3;
                stack[x * 4 + 3] = temp4;

            }

            for (int x = 0; x < block * 4; x++)
            {
                result[x] = toResult(stack[x]);
            }

            switch (padding)
            {
                case 0: break;
                case 1: result[block * 4 - 1] = '='; break;
                case 2:
                    result[block * 4 - 1] = '=';
                    result[block * 4 - 2] = '=';
                    break;
                default: break;
            }
            return result;
        }

        private char toResult(byte b)
        {
            char[] s = new char[64]
                {  'A','B','C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9','+','/'};

            if ((b >= 0) && (b <= 63))
            {
                return s[(int)b];
            }
            else
            {
                return ' ';
            }
        }
    }
}
