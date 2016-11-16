using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Colects
{
    public class Table
    {
        public string name;
        public int life;
        public BigInteger energy;

        public Table(string estr, string name = "", int life = 1) { energy=BigInteger.Parse(estr); }

        public void Print()
        {
            Console.WriteLine("name={0},energy={1},life={2}", name, energy, life);
        }

        public BigInteger Sqrt(BigInteger bi)
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
            BigInteger last;
            BigInteger result =BigInteger.Parse("0");

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
                        break;
                    }
                }
            }

            return result;
        }
    }

    public class Item : Table
    {
        public BigInteger [] factors;

        public Item(string estr)
            : base(estr)
        {
            int length = energy.ToString().Length + 3;  //3 blacks is enough
            factors = new BigInteger[length];
        }

        public void getFactors()
        {
            int length = energy.ToString().Length * 2;
            BigInteger[] temp = new BigInteger[length];
            if (energy % 2 == 0) temp[0] = 2;       //排除偶数
            for (BigInteger i = 3; i <= energy; i += 2)
            {
                //若 energy 是素数，尽早退出
                if (i > Sqrt(energy) && temp[0] == 0) { temp[0] = energy; break; }
                
                for (int j = 0; j < length; j++)
                {
                    if (temp[j] == 0)
                    {
                        if (energy % i == 0)
                        {
                            temp[j] = i;
                            do { energy = energy / i; } while (energy % i == 0);    //对相同质因数，彻底排除
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < temp.Length && temp[i] != 0; i++)
            {
                factors[i] = temp[i];
            }
        }

        public void Paint()
        {
            string temp="";
            for (int i = 0; i < factors.Length; i++)
            {
                temp += factors[i].ToString() + " ";
            }
            Console.WriteLine( temp);
        }
    }
}
