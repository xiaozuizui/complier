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
            DisPoseLine dp = new DisPoseLine("Function: littlemm\r\nvar\r\na1, a2, n, sum, const1: integer;\r\n\r\nbegin\r\na1:= 1;\r\na2:= 1;\r\nn:= 3;\r\n\r\nsum:= 0;\r\nconst1:= 1;\r\n\r\nif n = const1 then\r\nsum:= 1\r\nelse\r\nsum:= 1;\r\nbegin \r\n\twhile n> const1\r\n\tdo \r\n\tbegin sum:= sum + a2; temp:= a1; a1:= a2; a2:= a2 + temp; n:= n - 1 end\r\nend\r\nend  ");
            dp.Dispose();

            foreach(Token t in dp.tokens)
            {
                Console.WriteLine(t.content + "  " + t.type);
            }
        }
    }
}
