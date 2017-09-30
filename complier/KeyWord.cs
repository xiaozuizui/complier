using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace complier
{
    public class KeyWord
    {
        public string [] Keyword { get; set; }

        public KeyWord()
        {
            Keyword[0] = "";
            Keyword[1] = "integer";
            Keyword[2] = "true";
            Keyword[3] = "false";
            Keyword[4] = "if";
            Keyword[5] = "else";
            Keyword[6] = "for";
            Keyword[7] = "bool";
            Keyword[8] = "+";
        }
    }
}
