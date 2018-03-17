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
        List<Symble> symbles;
      //  List<symble> symbles;

        public string error = "";
        int i = 0;
        public Grammar(DisPoseLine m) //词法分析结果作为输入
        {
            tokens = m.tokens;
            symbles = m.symbles;
        //    symbles = m.symbles;
            Dispose();
        }

       // 判断是不是标识符表
        private bool IsIdlist()
        {
            if (tokens[i].type == 2)
            {
                Next();
                if (tokens[i].type == 28)//识别,号
                {
                    Next();
                    return IsIdlist();
                }
                else
                {
                    Before();
                    return true;
                }
            }
            else
            {
                return false;
            }
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
            if (tokens[i].type == 1)// function 程序入口 
            {
                Next();
                if (tokens[i].type == 2)//是标识符  
                {
                    //执行程序体  
                    Next();
                    MainBody();
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

        private void MainBody()
        {
            if (tokens[i].type == 5)//变量定义  var
            {
                Next();
                VarDef();
            }
            else if (tokens[i].type == 9)//判断begin  复合句
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
            if (IsIdlist())//标识符识别完成
            {
                Next();
                if (tokens[i].type == 27)// 冒号识别
                {
                    Next();
                    if (tokens[i].type == 6 || tokens[i].type == 7 || tokens[i].type == 8)//integer,float,bool  
                    {
                     //   int j = i;
                     //   j = j - 2;
                     // //  symbles[tokens[j].Addr].Type = tokens[i].type;
                     //   j--;
                     ////   while (tokens[j].type == 28)
                     //   {
                     //       j--;
                     ////       symbles[tokens[j].Addr].Type = tokens[i].type;
                     //   }
                        Next();
                        if (tokens[i].type == 29)//;号识别
                        {
                            Next();
                            if (tokens[i].type == 9)//复合句
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

        private void ComSent()
        {
            SentList();

            if (error == "")
            {
                if (tokens[i].type == 10)//复合句结束
                {
                    return;
                }
                else
                {
                    error = "复合句末尾缺少end";
                }
            }
        }

        private void SentList()
        {
            ExecSent(); //执行句
            if (error == "")
            {
                Next();
                if (tokens[i].type == 29)//识别;
                {
                    Next();
                    SentList();
                }
            }
        }

        private void ExecSent()
        {
            if (tokens[i].type == 2)
            {
                Next();
                AssiSent(); //简单句
            }
            else if (tokens[i].type == 9 || tokens[i].type == 15 || tokens[i].type == 11)//begin if while
            {
                StructSent(); //结构ju
            }
            else
            {
                Before();
            }
        }

        private void AssiSent()//赋值句
        {
            if (tokens[i].type == 33)//:=  
            {
                Next();
                Expression();
            }
            else
            {
                error = "赋值句变量后缺少：=";
            }
        }

        /// <summary>
        /// !!!
        /// </summary>
        private void Expression()
        {
            if (tokens[i].type == 7 || tokens[i].type == 15 || (tokens[i].Addr != -1 && symbles[tokens[i].Addr].Type == 3))
            {
                BoolExp();
            }
            else
            {
                AritExp();
            }
        }

        private void BoolExp() //bool表达式
        {
            BoolItem();
            if (error == "")
            {
                Next();
                if (tokens[i].type == 18)
                {
                    Next();
                    BoolExp();
                }
                else
                {
                    Before();
                }
            }
            else
            {
                return;
            }
        }

        private void BoolItem()//boolx
        {
            BoolFactor();
            if (error == "")
            {
                Next();
                if (tokens[i].type == 38)
                {
                    Next();
                    BoolItem();
                }
                else
                {
                    Before();
                }
            }
        }

        private void BoolFactor()
        {
            if (tokens[i].type == 19)
            {
                Next();
                BoolFactor();
            }
            else
            {
                BoolValue();
            }
        }
 
        private void BoolValue()
        {
            if (tokens[i].type == 13 || tokens[i].type == 14)//true or false
            {
                return;
            }
            else if (tokens[i].type == 2)
            {
                Next();
                if (tokens[i].type == 32 || tokens[i].type == 34 || tokens[i].type == 30 || tokens[i].type == 35 || tokens[i].type == 31 )
                {
                    Next();
                    if (tokens[i].type == 18)
                    {
                        //zheng que
                    }
                    else
                    {
                        error = "关系运算符后缺少标识符";
                    }
                }
                else
                {
                    Before();
                }
            }
            else if (tokens[i].type == 24)//kuo hao
            {
                BoolExp();
                //?  
                if (tokens[i].type == 25)
                {
                    return;
                }
                else
                {
                    error = "布尔量中的布尔表达式缺少一个）";
                }
            }
            else
            {
                error = "布尔量出错";
            }
        }

        private void AritExp()
        {
            Item();
            if (error == "")
            {
                Next();
                if (tokens[i].type == 20 || tokens[i].type == 21)//+-
                {
                    Next();
                    AritExp();
                }
                else
                {
                    Before();
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void Item()
        {
            Factor();
            if (error == "")
            {
                Next();
                if (tokens[i].type == 22 || tokens[i].type == 23)//*/
                {
                    Next();
                    Item();
                }
                else
                {
                    Before();
                    return;
                }
            }
            else
            {
                return;
            }
        }
      
        private void Factor()
        {
            if (tokens[i].type == 24) //括号
            {
                Next();
                AritExp();
                Next();
                if (tokens[i].type == 25)
                {
                    return;
                }
                else
                {
                    error = "因子中算数表达式缺少）";
                }
            }
            else
            {
                CalQua();
            }
        }
      
        private void CalQua()
        {
            if (tokens[i].type == 2 || tokens[i].type == 3 || tokens[i].type == 4)//标识符，整数，shi'shu
            {
                return;
            }
            else
            {
                error = "算数量出错";
            }
        }
       
        private void StructSent()
        {
            if (tokens[i].type == 9)//begin
            {
                Next();
                ComSent();
            }
            else if (tokens[i].type == 15)//if
            {
                Next();
                IfSent();
            }
            else if (tokens[i].type == 11)//while
            {
                Next();
                WhileSent();
            }
        }
       
        private void IfSent()
        {
            BoolExp();
            if (error == "")
            {
                Next();
                if (tokens[i].type == 14)
                {
                    Next();
                    ExecSent();
                    Next();
                    if (tokens[i].type == 5)
                    {
                        Next();
                        ExecSent();
                    }
                    else
                    {
                        Before();
                        return;
                    }
                }
                else
                {
                    error = "if...then语句缺少then";
                }
            }
            else
            {
                error = "if语句布尔表达式出错";
            }
        }
        #endregion

        #region while语句  
        private void WhileSent()
        {
            BoolExp();
            if (error == "")
            {
                Next();
                if (tokens[i].type == 4)
                {
                    Next();
                    ExecSent();
                }
                else
                {
                    error = "while语句缺少do";
                }
            }
        }
        #endregion
    }
}
}
