using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace complier
{
    public class Token
    {
        public uint type { get; set; }//类型
        public string content { get; set; }//单词
        public int symb { get; set; }//对应的符号表，没有用-1表示
        public uint Line;//所在行
    }


    public class Symble
    {
        public uint type { get; set; }
        public string content { get; set; }
        public int token { get; set; }
    }
}
