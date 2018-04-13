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
        public List<Symble> symbles { get; set; }


        public List<Error> errors { get; set; }


        private uint line = 1;

        private int i;

        public DisPoseLine(string str)
        {
            Line_Str = str;
            i = 0;
            keyword = new KeyWord();
            tokens = new List<Token>();
            symbles = new List<Symble>();
        }

        public void Dispose()
        {
            while(i<Line_Str.Length)
            {
                if (IsAlpha(Line_Str[i]))// 字母识别
                    RecogId();

                else if (IsDight(Line_Str[i]))//数字识别
                    RecogCons();

                else if (Line_Str[i] == '\r' && Line_Str[i + 1] == '\n')//处理换行
                {
                    i++;
                    i++;
                    line++;
                }

                else if (IsDelimiter(Line_Str[i]))//符号处理
                    RecogSym();

                else if(Line_Str[i] == ' '||Line_Str[i]=='\t')//处理空格
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
            token.Line = line;
            if (Typecode == 0)//不在关键字中
            {
                //标识符
                token.type = 2;
                token.symb = symbles.Count;

                Symble sym = new Symble();//存入符号表
                sym.content = str;
                sym.type = 2; //TAG
                sym.token = token.symb;
                symbles.Add(sym);
            }
            else
            {
                token.type = Typecode;//关键字
                token.symb = -1;
            }
               
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
                        Error e = new Error(line, "出现第二个'.'号");
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
                            Error e = new Error(line, "数字开头的数字、字母串");
                            errors.Add(e);
                        }
                        else
                        {
                            Error e = new Error(line, "实数的小数部分出现字母");
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
                token.type = 3; //nufloat
                token.symb = symbles.Count;
                token.Line = line;
                tokens.Add(token);

                Symble sym = new Symble();
                sym.content = str;
                sym.type = 3;
                sym.token = token.symb;

                symbles.Add(sym);
            }
            else
            {
                Token token = new Token();
                token.content = str;
                token.Line = line;
                token.type = 4;//float
                token.symb = symbles.Count;

                tokens.Add(token);

                Symble sym = new Symble();
                sym.content = str;
                sym.type = 4;
                sym.token = token.symb;

                symbles.Add(sym);
            }
        }

        private void RecogSym()
        {
            string str = "" + Line_Str[i];
            if (str == "<" || str == ">"||str =="="||str==":")
            {
                i++;
                if (Line_Str[i] == '=') //<=||>=
                {
                    str += Line_Str[i];
                }
                else if (Line_Str[i] == '=')//:=
                {
                    str += Line_Str[i];
                }
                else
                {
                    i--;
                }
            }

            for (int j = 20; j <= 37; j++) //符号识别
            {
                if (str == keyword.Keyword[j])
                {
                    Token token = new Token();
                    token.content = str;
                    token.Line = line;
                    token.type = (uint)j;
                    token.symb = -1;

                    tokens.Add(token);
                    i++;
                }
            }
        }  
    }

}
