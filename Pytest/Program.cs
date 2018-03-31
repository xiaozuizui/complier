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
            DisPoseLine disPoseLine = new DisPoseLine("\r\nFunction : littlemm\r\nvar\r\nsum,b,temp: integer;\r\n \r\nbegin\r\nsum:=1;\r\nb:=4;\r\ntemp:=0;\r\nwhile b>temp\r\ndo begin sum:=sum*b;b:=b-1 end\r\n;\r\nend ");
          //  DisPoseLine disPoseLine = new DisPoseLine("Function : littlemm\r\nvar\r\na,b,n: integer;\r\n \r\nbegin\r\na:=10;\r\nb:=20;\r\nif a>b\r\nthen begin a:=10;b:=11; end\r\nelse begin a:=20;b:=a+20; end\r\n;\r\nwhile a>b\r\ndo begin a:=a-1; end\r\n;\r\nend ");
            disPoseLine.Dispose();
            Semantic semantic = new Semantic(disPoseLine);
            semantic.Dispose();

            string s =  JsonConvert.SerializeObject(semantic.fourparts);
            string fuhao = JsonConvert.SerializeObject(semantic.symbles);

            ScriptRuntime pyrun = Python.CreateRuntime();
            dynamic py = pyrun.UseFile("../../Interpreter.py");
            var re = py.Interprete(s,fuhao);
            //var path = py.path();
            //  var v = py.v();
            //var pys = py.String(s);

        }
    }
}
