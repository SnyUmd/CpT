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
            common.winFream = this;

            Init.ReadConfigValue();

            if (!common.CheckScreenSize())
            {
                Point mouseP = new Point();
                mouseP = Mouse.GetPosition(common.winDrug);
                mouseP = common.winDrug.PointToScreen(mouseP);

                this.Left = mouseP.X - 100;
                this.Top = mouseP.Y - 100;
                this.Width = Init.width;
                this.Height = Init.height;
            }
            else
            {
                this.Left = common.DicFreamLocation[common.DicKey_Left];
                this.Top = common.DicFreamLocation[common.DicKey_Top];
                this.Width = common.DicFreamLocation[common.DicKey_Width];
                this.Height = common.DicFreamLocation[common.DicKey_Height];
            }

            common.setConfigFreamLocation(this);
        }
        //******************************************************************
        private void CaptureRun()
        {
            Point startP = new Point();
            Point endP = new Point();
            startP.X = this.Left;
            startP.Y = this.Top;
            endP.X = this.Left + this.Width;
            endP.Y = this.Top + this.Height;

            common.setConfigFreamLocation(this);

            common.ViewWindow(this, false);

            common.winDrug.Width = 10;
            common.winDrug.Height = 10;

            this.Close();
            ViewImage Vr = new ViewImage(startP, endP);
            Vr.Show();
        }

        //******************************************************************
        private void mouseL_Bdown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }

        //******************************************************************
        private void Fr_keyDown(object sender, KeyEventArgs e)
        {
            //if (common.blModeChange) return;
            if (e.Key == KeySts.Key_Capture) CaptureRun();

            else if (e.Key == KeySts.Key_ModeDrug)
            {
                //common.blModeChange = true;
                common.setConfigFreamLocation(this);
                common.CpT_mode = (int)enm_mode.drag;
                common.ViewWindow(this, false);
                common.winFream = this;
                this.Close();
                common.ChangeMode(common.CpT_mode);
            }
            else if (e.Key == KeySts.Key_Close0 || e.Key == KeySts.Key_Close1) common.AppClose();
            else if (e.Key == KeySts.Key_DefaultMove) FreamDefault();

            else if (this.Left > 0 && e.Key == KeySts.Key_Left) this.Left -= 1;
            else if (this.Left + this.Width < SystemParameters.WorkArea.Width && e.Key == KeySts.Key_Right) this.Left += 1;
            else if (this.Top > 0 && e.Key == KeySts.Key_Up) this.Top -= 1;
            else if (this.Top + this.Height < SystemParameters.WorkArea.Height && e.Key == KeySts.Key_Down) this.Top += 1;
        }

        //******************************************************************
        private void FreamDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CaptureRun();
        }

        private void FreamDefault()
        {
            this.Left = Init.left;
            this.Top = Init.top;

            this.Width = Init.width;
            this.Height = Init.height;

            common.setConfigFreamLocation(this);

        }
    }
}
