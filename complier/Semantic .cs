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
      //  public List<symble> symbles = new List<symble>();
        public string error = "";
        int i = 0;
        int ti = 1;
        string tt = "";

        public Semantic(DisPoseLine m)
        {
            tokens = m.tokens;
            //symbles = m.symbles;
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

        private string NewTemp()
        {
            string temp = "T" + ti.ToString();
            ti++;
            //symble s = new symble();
            //s.Name = temp;
            //symbles.Add(s);
            return temp;
        }
 
        private void BackPatch(int addr, int addr2)
        {
           // fps[addr].JumpNum = addr2.ToString();
        }
       
        private void Emit(string op, string strLeft, string strRight, string jumpNum)
        {
            FourPart fp = new FourPart(op, strLeft, strRight, jumpNum);
         //   fps.Add(fp);
        }
    }
}
