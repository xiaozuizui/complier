using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using complier.Base;

namespace complier
{
    public class KeyWord
    {
        public string [] Keyword { get; set; }

        public KeyWord()
        {
            Keyword = new string[25];
            Keyword[0] = "";
            Keyword[1] = "TAG";
            Keyword[2] = "CONSTANT";
            Keyword[3] = "FLOAT";

            Keyword[4] = "integer";
            Keyword[5] = "true";
            Keyword[6] = "false";
            Keyword[7] = "if";
            Keyword[8] = "else";
            Keyword[9] = "for";
            Keyword[10] = "bool";
            Keyword[11] = "+";
            Keyword[12] = "-";
            Keyword[13] = "*";
            Keyword[14] = "/";
            Keyword[15] = "(";
            Keyword[16] = ")";
            Keyword[17] = ":";
            Keyword[18] = ";";
            Keyword[19] = "=";
            Keyword[20] = ">";
            Keyword[21] = "<";
            Keyword[22] = "<=";
            Keyword[23] = ">=";
            Keyword[24] = "<>";
            
        }

        public uint FindKeyWord(string word)
        {
            for(uint i=0;i<9;i++)
            {
                if (word == Keyword[i])
                    return i;
            }
            return 0;
        }

        
    }
}
