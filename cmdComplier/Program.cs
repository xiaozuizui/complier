using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using complier;

namespace cmdComplier
{
    class Program
    {
        static void Main(string[] args)
        {
            DisPoseLine dp = new DisPoseLine("littlemm = 20 ");
            dp.Dispose();
        }
    }
}
