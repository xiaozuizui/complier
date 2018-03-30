using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using complier.Base;
using complier;

namespace Pytest
{
    class Program
    {
        static void Main(string[] args)
        {

            DisPoseLine disPoseLine = new DisPoseLine("Function : littlemm\r\nvar\r\na,b,n: integer;\r\n \r\nbegin\r\nif a>b\r\nthen begin a:=10;b:=11; end\r\nelse begin a:=20;b:=a+20; end\r\n;\r\nwhile a>b\r\ndo begin a:=a-1; end\r\n;\r\nend ");
            disPoseLine.Dispose();
            Semantic semantic = new Semantic(disPoseLine);
            semantic.Dispose();

            string s =  JsonConvert.SerializeObject(semantic.fourparts);

            ScriptRuntime pyrun = Python.CreateRuntime();
            dynamic py = pyrun.UseFile("../../test.py");
            var path = py.path();
          //  var v = py.v();
            var pys = py.String(s);

        }
    }
}
