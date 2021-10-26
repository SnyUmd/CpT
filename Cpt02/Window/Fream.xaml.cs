//#define DEB

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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


        //private System.Drawing.Point dpMouse = new System.Drawing.Point();

        //******************************************************************
        public Fream()
        {
            InitializeComponent();
            this.Topmost = true;
            common.winFream = this;

            Init.ReadConfigValue();

            System.Drawing.Point dp = new System.Drawing.Point();
            dp.X = common.DicFreamLocation[common.DicKey_Left];
            dp.Y = common.DicFreamLocation[common.DicKey_Top];

            if (common.CheckArea(dp))
            {
                this.Left = common.DicFreamLocation[common.DicKey_Left];
                this.Top = common.DicFreamLocation[common.DicKey_Top];
                this.Width = common.DicFreamLocation[common.DicKey_Width];
                this.Height = common.DicFreamLocation[common.DicKey_Height];
            }
            else FreamPointDefault();

            common.setConfigFreamLocation(this);


        }

        //******************************************************************
        private void FreamLoaded(object sender, RoutedEventArgs e)
        {
            //FreamVerification wFV = new FreamVerification();
            //★if(ここにCoonfigファイル内の警告表示イネーブルを判定)
            //wFV.ShowDialog();

#if DEB
            System.Drawing.Point dp0 = new System.Drawing.Point();
            System.Drawing.Point dp1 = new System.Drawing.Point();

            //dp0.X = (int)SystemParameters.PrimaryScreenWidth;
            //dp0.Y = (int)SystemParameters.PrimaryScreenHeight;

            //dp1 = System.Windows.Forms.Cursor.Position;
            //dp1.X = (int)SystemParameters.WorkArea;
            //dp1.Y = (int)SystemParameters.WorkArea.Y;

            //MessageBox.Show($"PrimaryScreen W = {dp0.X} / PrimaryScreen H = {dp0.Y}");
            //MessageBox.Show($"WorkAria W = {dp1.X} / WorkAria H = {dp1.Y}");

            /*Point pTest = new Point();
            pTest.X = dp.X;
            pTest.Y = dp.Y;
            pTest = (pTest);

            MessageBox.Show($"PrimaryScreen W(変換後) = {pTest.X} / PrimaryScreen H(変換後) = {pTest.Y}");*/
            Point pTest = new Point();
            pTest.X = 0;
            pTest.Y = 0;
            
            pTest = PointFromScreen(pTest);
            this.Left = pTest.X;
            this.Top = pTest.Y;

            System.Drawing.Point pDebug = new System.Drawing.Point();
            pDebug = System.Windows.Forms.Cursor.Position;
            MessageBox.Show($"Mouse Point X = {pDebug.X}, Y = {pDebug.Y}");

            //------------------
            int testDebug = System.Windows.Forms.Screen.AllScreens.Length;
            MessageBox.Show($"Screen num = {testDebug}");
            //------------------

            if (common.CheckArea(pDebug))
                MessageBox.Show("OK");
            else
                MessageBox.Show("NG");
#endif
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
            var keyCtrl_L = Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down;
            var keyCtrl_R = Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down;


            const Key ky = Key.Enter;
            //KeySts key_sts = new KeySts();

            switch(e.Key)
            {
                case KeySts.Key_Capture:
                    CaptureRun();
                    break;

                case KeySts.Key_ModeDrug:
                    break;

                case KeySts.Key_Close0:
                case KeySts.Key_Close1:
                    common.AppClose();
                    break;

                case KeySts.Key_DefaultMove:
                    FreamPointDefault();
                    break;

                default:
                    bool blLR = (e.Key == KeySts.Key_Left || e.Key == KeySts.Key_Right) ? true : false;
                    bool blUD = (e.Key == KeySts.Key_Up || e.Key == KeySts.Key_Down) ? true : false;

                    if (keyCtrl_L == KeyStates.Down || keyCtrl_R == KeyStates.Down)
                    {
                        this.Width = blLR ? (e.Key == KeySts.Key_Left)
                                                    ? this.Width - 1
                                                    : this.Width + 1
                                            : this.Width;
                        this.Height = blUD ? (e.Key == KeySts.Key_Up)
                                                    ? this.Height - 1
                                                    : this.Height + 1
                                            : this.Height;
                    }
                    else
                    {
                        this.Left = blLR ? (e.Key == KeySts.Key_Left)
                                                    ? this.Left - 1
                                                    : this.Left + 1
                                            : this.Left;
                        this.Top = blUD ? (e.Key == KeySts.Key_Up)
                                                    ? this.Top - 1
                                                    : this.Top + 1
                                            : this.Top;
                    }
                    break;
            }

            /*if (e.Key == KeySts.Key_Capture) CaptureRun();

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
            else if (e.Key == KeySts.Key_DefaultMove) FreamPointDefault();
            else
            {
                bool blLR = (e.Key == KeySts.Key_Left || e.Key == KeySts.Key_Right) ? true : false;
                bool blUD = (e.Key == KeySts.Key_Up || e.Key == KeySts.Key_Down) ? true : false;

                if (keyCtrl_L == KeyStates.Down || keyCtrl_R == KeyStates.Down)
                {
                    this.Width = blLR   ? (e.Key == KeySts.Key_Left)
                                                ? this.Width - 1
                                                : this.Width + 1 
                                        : this.Width;
                    this.Height = blUD ? (e.Key == KeySts.Key_Up)
                                                ? this.Height - 1
                                                : this.Height + 1
                                        : this.Height;
                }
                else
                {
                    this.Left = blLR ? (e.Key == KeySts.Key_Left)
                                                ? this.Left - 1
                                                : this.Left + 1
                                        : this.Left;
                    this.Top = blUD ? (e.Key == KeySts.Key_Up)
                                                ? this.Top - 1
                                                : this.Top + 1
                                        : this.Top;
                }*/

                //if (keyCtrl_L == KeyStates.Down || keyCtrl_R == KeyStates.Down)
                //{
                //    if (this.Left > 0 && e.Key == KeySts.Key_Left) this.Width -= 1;
                //    else if (this.Left + this.Width < SystemParameters.WorkArea.Width && e.Key == KeySts.Key_Right) this.Width += 1;
                //    else if (this.Top > 0 && e.Key == KeySts.Key_Up) this.Height -= 1;
                //    else if (this.Top + this.Height < SystemParameters.WorkArea.Height && e.Key == KeySts.Key_Down) this.Height += 1;
                //}
                //else
                //{
                //    if (this.Left > 0 && e.Key == KeySts.Key_Left) this.Left -= 1;
                //    else if (this.Left + this.Width < SystemParameters.WorkArea.Width && e.Key == KeySts.Key_Right) this.Left += 1;
                //    else if (this.Top > 0 && e.Key == KeySts.Key_Up) this.Top -= 1;
                //    else if (this.Top + this.Height < SystemParameters.WorkArea.Height && e.Key == KeySts.Key_Down) this.Top += 1;
                //}
            //}
        }

        //******************************************************************
        private void FreamDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CaptureRun();
        }

        //******************************************************************
        private void FreamPointDefault()
        {
            /*
            this.Left = Init.left;
            this.Top = Init.top;

            this.Width = Init.width;
            this.Height = Init.height;

            */

            System.Drawing.Point dp = new System.Drawing.Point();
            dp = common.GetMousePoint();
            this.Left = dp.X - 50;
            this.Top = dp.Y - 50;
            this.Width = common.DicFreamLocation[common.DicKey_Width];
            this.Height = common.DicFreamLocation[common.DicKey_Height];
            common.setConfigFreamLocation(this);

        }


    }
}
