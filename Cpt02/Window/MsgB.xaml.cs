using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CpT
{
    /// <summary>
    /// MsgB.xaml の相互作用ロジック
    /// </summary>
    public partial class MsgB : Window
    {
        public MsgB(string msg, double px, double py)
        {
            InitializeComponent();

            msgVal.Content = msg;
            this.Topmost = true;

            this.Left = px - this.Width / 2;
            this.Top = py - this.Height / 2;

            
        }

        private void MsgBoxLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MsgBoxActivated(object sender, EventArgs e)
        {
            common.DoEvents();

            MsgView(true);
            Thread.Sleep(400);
            MsgView(false);

            this.Close();
        }

        void MsgView(bool blopen)
        {
            double op = blopen ? 0 : 1;
            this.Opacity = op;

            for (int i = 1; i <= 100; i++)
            {
                op = blopen ? (op + 0.01) : (op - 0.01);
                this.Opacity = op;
                common.DoEvents();
                Thread.Sleep(2);
            }
        }
    }
}
