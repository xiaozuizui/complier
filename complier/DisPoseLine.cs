using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using complier.Base;
namespace complier
{
    public class DisPoseLine : BaseFun
    {
        public string Line_Str { get; set; }
        public KeyWord keyword { get; set; }
        public List<Token> tokens { get; set; }
        public List<Error> errors { get; set; }

        private uint testrow = 0;

        private int i;

        public DisPoseLine(string str)
        {
            Line_Str = str;
            i = 0;
            keyword = new KeyWord();
            tokens = new List<Token>();
        }

        public void Dispose()
        {
            while(i<Line_Str.Length)
            {
                if (IsAlpha(Line_Str[i]))
                    RecogId();

                else if (IsDight(Line_Str[i]))
                    RecogCons();

                else if (Line_Str[i] == '\r' && Line_Str[i + 1] == '\n')
                {
                  
                    i++;
                    i++;
                }
                else if (IsDelimiter(Line_Str[i]))
                    RecogSym();

                else if(Line_Str[i] == ' ')
                    i++;
            }
        }

        private void RecogId()
        {
            string str = "";
            uint Typecode;

            do
            {
                str = str + Line_Str[i];
                i++;
            } while (IsAlpha(Line_Str[i]) || IsDight(Line_Str[i]));//判断是不是字母或数字，是的话继续执行  


            Typecode = keyword.FindKeyWord(str);//是否为关键字
            Token token = new Token();//存入token文件  
            token.content = str;
            if (Typecode == 0)//不在关键字中
                token.type = 1; //TAG标识符
            else
                token.type = Typecode;//关键字
            tokens.Add(token);
        }

        private void RecogCons()
        {
            string str = Line_Str[i].ToString();
            bool flag = true;
            bool point = true;
            while (flag)
            {
                i++;
                if (IsDight(Line_Str[i]))
                    str += Line_Str[i];
                else if (Line_Str[i] == '.')
                {
                    if (point)
                    {
                        str += Line_Str[i];
                        point = false;//读取小数点，此后不再读取
                    }
                    else
                    {
                        Error e = new Error(testrow, "出现第二个'.'号");
                        errors.Add(e);
                        flag = false;
                    }
                }
                else if (IsAlpha(Line_Str[i]))
                {
                    i++;
                    if (IsDight(Line_Str[i]))
                    {
                        i--;
                        i--;
                        if (point)
                        {
                            Error e = new Error(testrow, "数字开头的数字、字母串");
                            errors.Add(e);
                        }
                        else
                        {
                            Error e = new Error(testrow, "实数的小数部分出现字母");
                            errors.Add(e);
                        }
                    }
                }
                else
                {
                    flag = false;
                }
            }//while end

            if (point)
            {
                Token token = new Token();

                token.content = str;
                token.type = 2; //nufloat

                tokens.Add(token);
            }
            else
            {
                Token token = new Token();

                token.content = str;
                token.type = 3;//float
                tokens.Add(token);
            }
        }

        private void RecogSym()
        {
            string str = "" + Line_Str[i];
            if (str == "<" || str == ">")
            {
                i++;
                if (Line_Str[i] == '=') //<=||>=
                {
                    str += Line_Str[i];
                }
                else if (Line_Str[i] == '>' && str == "<")//<>
                {
                    str += Line_Str[i];
                }
                else
                {
                    i--;
                }
            }
            for (int j = 15; j <= 24; j++)
            {
                if (str == keyword.Keyword[j])
                {
                    Token token = new Token();
                    token.content = str;
                    token.type = (uint)j;
                    tokens.Add(token);
                    i++;
                }
            }
        }  
    }

}
