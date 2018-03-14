using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace complier.Base
{
    abstract public class BaseFun
    {
        protected bool IsAlpha(char c)
        {
            if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
                return true;
            else
                return false;
        }

        protected bool IsDight(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            else
                return false;
        }

        protected bool IsDelimiter(char c)
        {

            switch (c)
            {
                case '(': return true;
                case ')': return true;
                case '+': return true;
                case '-': return true;
                case '*': return true;
                case '/': return true;
                case '.': return true;
                case ',': return true;
                case ':': return true;
                case ';': return true;
                case '=': return true;
                case '<': return true;
                case '>': return true;
                default: return false;
            }
        }

    }
}
