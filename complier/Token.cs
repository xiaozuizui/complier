using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace complier
{
    public class Token
    {
        public uint type { get; set; }
        public string content { get; set; }
        public int symb { get; set; }
        public uint Line;
    }


    public class Symble
    {
        public uint type { get; set; }
        public string content { get; set; }
        public int token { get; set; }
    }
}
