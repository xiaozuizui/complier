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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AurelienRibon.Ui.SyntaxHighlightBox;

namespace littlemm_IDE
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            box.CurrentHighlighter = HighlighterManager.Instance.Highlighters["VHDL"];
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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
