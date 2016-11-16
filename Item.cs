using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Colects
{
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
            for (int i = 0; i < factors.Length; i++)
            {
                BigInteger temp = getFactor(energy);
                if (temp != 1)
                {
                    factors[i] = temp;
                    do { energy /= temp; } while (energy % temp == 0);
                }
                else break;
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
