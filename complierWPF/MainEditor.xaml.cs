using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System;
using System.Windows;
using System.Windows.Threading;
using complier;

namespace complierWPF
{
    /// <summary>
    /// MainEditor.xaml 的交互逻辑
    /// </summary>
    public partial class MainEditor : Window
    {
        DispatcherTimer wordCountTimer;
        DisPoseLine dl;
        Grammar grammar;
        public MainEditor()
        {
            InitializeComponent();
            InitEvents();
            
            InitTimer();
        }

        void InitEvents()
        {
            // this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            // this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);
            //Editor.DocumentReady += new RoutedEventHandler(Editor_DocumentReady);
            Dispose.Click += new RoutedEventHandler(Show_Click);
            Editor.ContentText = "integer littlemm = 20；\r\nfloat a = 20.1;\r\nfloat b = a + littlemm; ";
            // GetHtmlButton.Click += new RoutedEventHandler(GetHtmlButton_Click);
            //GetTextButton.Click += new RoutedEventHandler(GetTextButton_Click);
            // BindingTestButton.Click += new RoutedEventHandler(BindingTestButton_Click);
        }

        void InitTimer()
        {
            
            wordCountTimer = new DispatcherTimer();
            wordCountTimer.Interval = TimeSpan.FromMilliseconds(500);
            wordCountTimer.Tick += new EventHandler(wordCountTimer_Tick);
        }

        void wordCountTimer_Tick(object sender, EventArgs e)
        {
            WordCountText.Content = Editor.WordCount;
        }

       

        void Show_Click(object sender, RoutedEventArgs e)
        {
            string str = Editor.ContentText;
            dl = new DisPoseLine(str+" ");
         
            dl.Dispose();
            string show = "";
           
            foreach(var erro in dl.errors)
            {
                show += erro.row + " " + erro.errorContent;
            }
            // grammar = new Grammar(dl);
            //Semantic s = new Semantic(dl);
            //s.Dispose();

            //string show="";
            //string show2 = "";
            //foreach(Token tk in dl.tokens)
            //{
            //    show = show + "< " + tk.type.ToString() + " , " + tk.content + " >" + "\r";
            //}
            //int i = 0;
            //foreach (FourPart f in s.fourparts)
            //{

            //    show2 = show2 + i.ToString() + "<" + f.Op + "," + f.StrLeft + "," + f.StrRight + "," + f.JumpNum + ">" + "\r";
            //    i++;
            //}
            MessageBox.Show(show);
            //MessageBox.Show(show2);

         }
    }
}
