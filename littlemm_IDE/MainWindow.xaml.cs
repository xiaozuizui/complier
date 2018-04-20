using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AurelienRibon.Ui.SyntaxHighlightBox;
using complier;
using complier.Base;

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;

namespace littlemm_IDE
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        DisPoseLine disPoseLine;
        Grammar grammar;
        Semantic Semantic;

        public MainWindow()
        {
            InitializeComponent();
            box.CurrentHighlighter = HighlighterManager.Instance.Highlighters["VHDL"];
            //CommandBinding  binding = new CommandBinding()
        }
        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void MouseLeaveArea(object sender, MouseEventArgs e)
        {

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FindAndReplace_Click(object sender, RoutedEventArgs e)
        {

        }
       private void  MouseEnterToolHinsArea(object sender,RoutedEventArgs e)
        {

        }
       private void  menuAutoWrap_Click(object sender,RoutedEventArgs e)
        {

        }
        private void ToolsSpellingHins_Click(object sender,RoutedEventArgs e)
        {

        }
        private void FileExit_Click(object sender,RoutedEventArgs e)
        {

        }
        private void MouseEnterExitArea(object sender,RoutedEventArgs e)
        {

        }

        private void Complie_Click(object sender, RoutedEventArgs e)
        {
            @out.Children.Clear();

            disPoseLine = new DisPoseLine(box.Text+' ');
            disPoseLine.Dispose();

            Semantic = new Semantic(disPoseLine);
            Semantic.Dispose();

            if (Semantic.errors.Count != 0)
            {
                foreach (Error error in Semantic.errors)
                    @out.Children.Add(new TextBlock { Text = error.row + " |" + error.errorContent, Height = 20 });
            }
            else
                @out.Children.Add(new TextBlock { Text = "   无语法错误  " ,Height = 20});
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {

            @out.Children.Clear();
            four.Children.Clear();

            string s = JsonConvert.SerializeObject(Semantic.fourparts);
            string fuhao = JsonConvert.SerializeObject(Semantic.symbles);

            ScriptRuntime pyrun = Python.CreateRuntime();
            dynamic py = pyrun.UseFile("Interpreter.py");
            var re = py.Interprete(s, fuhao);

            @out.Children.Add(new TextBlock { Text = "运行结果：\n" + re,LineHeight = 20 } );

            int i = 0;
            foreach(FourPart f in Semantic.fourparts)
            {
                four.Children.Add(new TextBlock { Text = i.ToString()+ " < " + f.Op + " , " + f.StrLeft + " , " + f.StrRight + " , " + f.JumpNum + " >" });
                i++;
            }
        }

        private void CommandBinding_Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".l";
            ofd.Filter = "langula file|*.l";
            if (ofd.ShowDialog() == true)
            {
                FileStream stream = new FileStream(ofd.FileName, FileMode.Open);// ofd.FileName;
                StreamReader streamReader = new StreamReader(stream);
                box.Text = streamReader.ReadToEnd();
                //此处做你想做的事 ...=ofd.FileName; 
            }
        }
    }
}
