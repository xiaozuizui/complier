using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace complier.Base
{
    public class Error
    {
        public string errorContent;
        public uint row;

        public Error(uint row,string content)
        {
            this.row = row;
            errorContent = content;
        }
    }
}
