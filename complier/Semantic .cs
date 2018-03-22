using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using complier;
using complier.Base;

namespace complier
{
    public class Semantic
    {
        public List<FourPart> fourparts = new List<FourPart>();
        public List<Error> errors = new List<Error>();
        List<Token> tokens;

        public List<Symble> symbles = new List<Symble>();

        public string error = "";
        int i = 0;
        int ti = 1;
        string tt = "";

        public Semantic(DisPoseLine m)
        {
            tokens = m.tokens;
            symbles = m.symbles;
            //Dispose();
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

        private string NewTemp() //创建临时变量
        {
            string temp = "T" + ti.ToString();
            ti++;
            Symble s = new Symble();
            s.content = temp;
            symbles.Add(s);
            return temp;
        }
 
        private void BackPatch(int addr, int addr2)//回填函数
        {
            fourparts[addr].JumpNum = addr2.ToString();
        }
       
        private void Emit(string op, string strLeft, string strRight, string jumpNum)//四元式
        {
            FourPart fp = new FourPart(op, strLeft, strRight, jumpNum);
             fourparts.Add(fp);
        }

        public void Dispose()
        {
            if (tokens[i].type == 1)//含有Function 
            {
                Next();
                if(tokens[i].type==27)//识别：
                {
                    Next();
                    if (tokens[i].type == 2)//是标识符  
                    {
                        Emit("program", tokens[i].content, "_", "_");
                        //执行程序体  
                        Next();
                        Body();
                    }
                    else
                    {
                        error = "该程序program缺少方法名";
                    }
                }
            }
            else
            {
                error = "该程序缺少关键字：program";
            }
        }

        private void Body()
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
                        int j = i;
                        j = j - 2; //移动到变量的位置
                        symbles[tokens[j].symb].type = tokens[i].type;
                        j--;
                        while (tokens[j].type == 28) //标记当前变量
                        {
                            j--;
                            symbles[tokens[j].symb].type = tokens[i].type;
                        }
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
                    Emit("sys", "_", "_", "_");
                }
                else
                {
                    error = "复合句末尾缺少end";
                }
            }
        }

        private void SentList()
        {

            Sentence s = new Sentence();

            ExecSent(ref s); //执行句
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

        private void ExecSent(ref Sentence s)
        {
            if (tokens[i].type == 2)
            {
                Next();
                AssiSent(); // 简单句(赋值句)
            }
            else if (tokens[i].type == 9 || tokens[i].type == 15 || tokens[i].type == 11)//begin if while
            {
                StructSent(ref s); //结构句
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
                string temp = tokens[i - 1].content;
                Next();
                
                Expression();
                Emit(":=", tt, "_", temp);
            }
            else
            {
                error = "赋值句变量后缺少：=";
            }
        }

        private void Expression()
        {
            if (tokens[i].type == 13 || tokens[i].type == 14 || (tokens[i].symb != -1 && symbles[tokens[i].symb].type == 8))
            {
                Express e = new Express();
                BoolExp(ref e);
            }
            else
            {
               AritExp();
            }
        }

        private void BoolExp(ref Express e) //bool表达式
        {
            Express temp_e = new Express();
            BoolItem(ref temp_e);

            if (error == "")
            {
                Next();
                if (tokens[i].type == 18)//or
                {
                    int num = fourparts.Count;
                    Express temp_e2 = new Express();

                    Next();
                    BoolExp(ref temp_e2);
                    e.t.Concat(temp_e.t);
                    e.t.Concat(temp_e2.t);
                    
                    e.f = temp_e2.f;
                    foreach(int k in e.t)//回填
                    {
                        BackPatch(k, num);
                    }
                }
                else
                {
                    e = temp_e;
                    Before();
                }
            }
            else
            {
                e = temp_e;
                //return;
            }
        }

        private void BoolItem(ref Express e)//boolx
        {
            Express temp = new Express();
            BoolFactor(ref temp);
            if (error == "")
            {
                Next();
                if (tokens[i].type == 38)//and
                {
                    Next();
                    int num = fourparts.Count;
                    Express temp_2 = new Express();
                    BoolItem(ref temp_2);
                    e.t = temp_2.t;
                    e.f.Concat(temp.f);
                    e.f.Concat(temp_2.f);
                    foreach(int k in e.t)
                    {
                        BackPatch(k, num);
                    }
                    
                }
                else
                {
                    e = temp;
                    Before();
                }
            }
        }


        private void BoolFactor(ref Express e)
        {
            if (tokens[i].type == 19) //not
            {
                Next();
                Express temp = new Express();

                BoolFactor(ref temp);
                e.t = temp.f;
                e.f = temp.t;

            }
            else
            {
                Express temp = new Express();

                BoolValue(ref temp);
                e = temp;

            }
        }

        private void BoolValue(ref Express e)
        {
            if (tokens[i].type == 13 || tokens[i].type == 14)//true or false
            {
                e.t.Add(fourparts.Count);
                e.f.Add(fourparts.Count + 1);
                tt = tokens[i].content;
                
            }
            else if (tokens[i].type == 2)
            {
                Next();
                if (tokens[i].type == 32 || tokens[i].type == 34 || tokens[i].type == 30 || tokens[i].type == 35 || tokens[i].type == 31)
                {
                    Next();
                    if (tokens[i].type == 2)
                    {
                        e.t.Add(fourparts.Count);
                        e.f.Add(fourparts.Count+1);

                        Emit("j" + tokens[i - 1].content, tokens[i - 2].content, tokens[i].content, "-1");
                        Emit("j", "_", "_", "-1");

                        //zheng que
                    }
                    else
                    {
                        error = "关系运算符后缺少标识符";
                        Before();
                    }
                }
                else
                {
                    Before();
                    e.t.Add(fourparts.Count);
                    e.f.Add(fourparts.Count + 1);
                    Emit("jnz", tokens[i].content, "_", "-1");
                    Emit("j", "_", "_", "-1");
                    Next();
                    
                }
            }
            else if (tokens[i].type == 24)//kuo hao
            {
                Express temp = new Express();
                BoolExp(ref temp);
                e.t = temp.t;
                e.f = temp.f;
                //BoolExp();
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
                    string[] temp = { tokens[i - 1].content, tokens[i].content };
                    if (tokens[i - 1].type == 25) //")"
                    {
                        temp[0] = tt;
                    }
                    Next();
                    AritExp();

                    Emit(temp[1], temp[0], tt, NewTemp());
                    tt = "T" + (ti - 1).ToString();
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
                    string[] temp = { tokens[i - 1].content, tokens[i].content };
                    if (tokens[i - 1].type == 25)//")"
                    {
                        temp[0] = tt;
                    }

                    Next();
                    Item();
                    Emit(temp[1], temp[0], tt, NewTemp());
                    tt = "T" + (ti - 1).ToString();
                }
                else
                {
                    Before();
                    //return;
                }
            }
            else
            {
                //return;
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
                   // return;
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
                tt = tokens[i].content;
                return;
            }
            else
            {
                error = "算数量出错";
                //return;
            }
        }

        private void StructSent(ref Sentence s)
        {
            if (tokens[i].type == 9)//begin
            {
                Next();
                ComSent();
            }
            else if (tokens[i].type == 15)//if
            {
                Next();
                IfSent(ref s);
            }
            else if (tokens[i].type == 11)//while
            {
                Next();
                WhileSent(ref s);
            }
        }

        private void IfSent(ref Sentence s)
        {
            Express temp = new Express();
            BoolExp(ref temp);
            if (error == "")
            {
                Next();
                if (tokens[i].type == 16)//then
                {
                    int num = fourparts.Count;
                    Sentence temp_2 = new Sentence();

                    Next();
                    ExecSent(ref temp_2);

                    Next();
                    if (tokens[i].type == 17)//else
                    {
                        Sentence temp_3 = new Sentence();
                        temp_3.next.Add(fourparts.Count);
                        Emit("j", "_", "_", "-1");
                        Sentence temp_4 = new Sentence();
                        int num2 = fourparts.Count;


                        Next();
                        ExecSent(ref temp_4);

                        s.next = temp_2.next;
                        s.next.Concat(temp_3.next);
                        s.next.Concat(temp_4.next);

                        foreach (int k in temp.t)
                        {
                            BackPatch(k, num);
                        }
                        foreach (int k in temp.f)
                        {
                            BackPatch(k, num2);
                        }
                    }
                    else
                    {
                        s.next = temp.f;
                        s.next.Concat(temp_2.next);
                        foreach( int k in temp.t)
                        {
                            BackPatch(k, num);
                        }
                        Before();
                    }
                }
                else
                {
                    error = "if...then语句缺少then";
                    return;
                }
            }
            else
            {
                error = "if语句布尔表达式出错";
                return;
            }
        }

        private void WhileSent(ref Sentence s)
        {
            int num = fourparts.Count;
            Express temp = new Express();
            BoolExp(ref temp);

            if (error == "")
            {
                Next();
                if (tokens[i].type == 12)//do
                {
                    int num2 = fourparts.Count;
                    Sentence temp_1 = new Sentence();

                    Next();
                    ExecSent(ref temp_1);

                    s.next = temp.f;

                    Emit("j", "_", "_", num.ToString());

                    foreach (int k in temp.t)
                    {
                        BackPatch(k, num2);
                    }
                    foreach (int k in temp_1.next)
                    {
                        BackPatch(k, num);
                    }

                }
                else
                {
                    error = "while语句缺少do";
                    return;
                }
            }
        }
    }


}
