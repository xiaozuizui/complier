using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace complier.Base
{
    public class Sentence
    {
        public List<int> next;
        public Sentence()
        {
            next = new List<int>();
        }
    }

    public class Express
    {
        public List<int> t;
        public List<int> f;
        public Express()
        {
            t = new List<int>();
            f = new List<int>();
        }
    }
}
