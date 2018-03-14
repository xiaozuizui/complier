using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using complier;

namespace complier
{
    class Grammar
    {
        List<Token> tokens;
      //  List<symble> symbles;

        public string error = "";
        int i = 0;
        public Grammar(DisPoseLine m) //词法分析结果作为输入
        {
            tokens = m.tokens;
        //    symbles = m.symbles;
            Dispose();
        }

        private void Next()
        {
            if (i < tokens.Count - 1)
            {
                i++;
            }
        }

        private void Before()
        {
            if (i > 0)
            {
                i--;
            }
        }

        private void Dispose()
        {
            if (tokens[i].type == 0)// function 程序入口 
            {
                Next();
                if (tokens[i].type == 1)//是标识符  
                {
                    //执行程序体  
                    Next();
                    ProBody();
                }
                else
                {
                    error = "该程序program缺少方法名";
                }
            }
            else
            {
                error = "缺少程序入口";
            }
        }

        private void ProBody()
        {
            if (tokens[i].type == 4)//变量定义  
            {
                Next();
                VarDef();
            }
            else if (tokens[i].type == 8)//判断begin  
            {
                Next();
                ComSent();
            }
            else
            {
                error = "程序体缺少var或begin";
            }
        }

        private void VarDef() //定义变量
        {
            if (IsIdlist())
            {
                Next();
                if (tokens[i].Code == 29)//：  
                {
                    Next();
                    if (tokens[i].type == 9 || tokens[i].Code == 3 || tokens[i].Code == 13)//integer,bool,real  
                    {
                        int j = i;
                        j = j - 2;
                        symbles[tokens[j].Addr].Type = tokens[i].Code;
                        j--;
                        while (tokens[j].Code == 28)
                        {
                            j--;
                            symbles[tokens[j].Addr].Type = tokens[i].Code;
                        }
                        Next();
                        if (tokens[i].Code == 30)
                        {
                            Next();
                            if (tokens[i].Code == 2)
                            {
                                Next();
                                ComSent();
                            }
                            else
                            {
                                VarDef();
                            }
                        }
                        else
                        {
                            error = "变量定义后面缺少；";
                        }
                    }
                    else
                    {
                        error = "变量定义缺少类型或类型定义错误";
                        return;
                    }
                }
                else
                {
                    error = "var后面缺少冒号";
                }
            }
            else
            {
                error = "变量定义标识符出错";
            }
        }
    }
}
