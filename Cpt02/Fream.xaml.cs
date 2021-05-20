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

namespace CpT
{
    /// <summary>
    /// Fream.xaml の相互作用ロジック
    /// </summary>
    public partial class Fream : Window
    {
        //******************************************************************
        public Fream()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        //******************************************************************
        private void mouseL_Bdown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }

        //******************************************************************
        private void Fr_keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Point startP = new Point();
                Point endP = new Point();
                startP.X = this.Left;
                startP.Y = this.Top;
                endP.X = this.Left + this.Width;
                endP.Y = this.Top + this.Height;
                //ViewImage Vr = new ViewImage(this.PointToScreen(startP), this.PointToScreen(endP));
                common.ViewWindow(this, false);
                this.Close();
                ViewImage Vr = new ViewImage(startP, endP);
                Vr.Show();
            }
        }
    }
}
