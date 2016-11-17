using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Colet
{
    public class Table
    {
        public string name{get; set;}
        public int life{get; set;}
        public BigInteger energy{get; set;}

        public Table(string name="", int life=1, int energy=0) { }

        public void Print()
        {
            Console.WriteLine("name={0},energy={1},life={2}", name, energy, life);
        }

        public BigInteger getFactor(BigInteger bi)
        {
            BigInteger result = bi;
            for (BigInteger i = 2; i <= bi; i++)
            {
                if (i > Sqrt(bi)) break;
                if (bi % i == 0) { result = i; break; }
            }
            return result;
        }

        public string toHex(BigInteger bi)
        {
            string sb ="";
            string result ="";
            do
            {
                BigInteger temp = bi % 16;
                switch (temp.ToString())
                {
                    case "10": { sb = "a"; break; }
                    case "11": { sb = "b"; break; }
                    case "12": { sb = "c"; break; }
                    case "13": { sb = "d"; break; }
                    case "14": { sb = "e"; break; }
                    case "15": { sb = "f"; break; }
                    default: { sb = temp.ToString(); break; }
                }
                result = sb +result;
                bi = bi / 16;
            } while (bi > 0);

            return result;
        }

        public string Sqrt(BigInteger bi)
        {
            string biNum = bi.ToString();
            int length = biNum.Length;
            string[] splits;
            if (length % 2 == 0)
            {
                splits = new string[length / 2];
                for (int i = 0; i < splits.Length; i ++)
                {
                    splits[i] = biNum.Substring(i*2, 2);
                }
            }
            else
            {
                splits = new string[length / 2 + 1];
                splits[0] = biNum.Substring(0,1);
                for (int i = 1; i < splits.Length; i ++)
                {
                    splits[i] = biNum.Substring(i*2-1, 2);
                }
            }

            BigInteger quotient;
            BigInteger remainder;
            BigInteger tryquot;
            BigInteger flag;
            BigInteger result =BigInteger.Parse("0");
            BigInteger last =BigInteger.Parse("0");

            for (int i = 0; i < splits.Length; i++)
            {
                remainder = BigInteger.Parse(splits[i]) + last * 100;
                for (int j = 0; j <= 9; j++)
                {
                    tryquot = BigInteger.Parse(j.ToString());
                    quotient = result * BigInteger.Parse("20") + tryquot;
                    last = remainder - quotient * tryquot;
                    flag = remainder - (quotient + BigInteger.Parse("1")) * (tryquot + BigInteger.Parse("1"));
                    if (last >= 0 && flag < 0)
                    {
                        result = result * 10 + tryquot;
                        Console.WriteLine(last.ToString());
                        Console.WriteLine(result.ToString() + "C");
                        break;
                    }
                }
            }

            return result.ToString();
        }
    }
}
