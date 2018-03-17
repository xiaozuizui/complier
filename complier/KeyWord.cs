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
            Keyword = new string[] {
                "",
                "Function:",
                "TAG",//标识符
                "CONSTANT",//常数
                "FLOAT_CONSTANT",//浮点数
            //4

             "var",//变量定义
             "integer",//整数
             "float",
             "bool",
            //8
            
             "begin",//语句开始
             "end",

             "while",
             "do",

             "true",
             "false",

             "if",
             "then",
             "else",
             "or",
             "not",

             //19
             "+",
             "-",
             "*",
             "/",
            "(",
            ")",
            ".",
            ":",
            ",",
            //28
            ";",
            "=",
            ">",
            "<",
            ":=",
            "<=",
            ">=",
            "==",
            "!=",
            //37
            "and"
        };

            
            
            
        }

        public uint FindKeyWord(string word)
        {
            for(uint i=5;i<18;i++)
            {
                if (word == Keyword[i])
                    return i;
            }
            return 0;
        }

        
    }
}
