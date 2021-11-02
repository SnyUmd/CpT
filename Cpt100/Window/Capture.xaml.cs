using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;


namespace CpT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Capture : Window
    {

        private System.Windows.Shapes.Rectangle currentRect = null;

        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        //******************************************************************
        public Capture()
        {
            InitializeComponent();
            common.winDrug = this;
            //描画先とするImageオブジェクトを作成する
            getScreenSize(ref common.ScreenX1, ref common.ScreenY1);
            this.WindowState = WindowState.Maximized;
            this.Topmost = true;
            common.ViewWindow(this, true);

            common.CpT_mode = (int)enm_mode.drag;

        }

        //******************************************************************
        public static void getScreenSize(ref int x, ref int y)
        {
            x = Screen.PrimaryScreen.Bounds.Width;
            y = Screen.PrimaryScreen.Bounds.Height;
        }


        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //******************************************************************
        private void Key_down(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == KeySts.Key_Close0 || e.Key == KeySts.Key_Close1)
                    common.AppClose();

            if(!common.flgDrug && e.Key == KeySts.Key_ModeFream)
            {
                common.CpT_mode = (int)enm_mode.fream;
                common.ViewWindow(this, false);
                this.Visibility = Visibility.Hidden;
                //this.Width = 10;
                //this.Height = 10;

                Init.ReadConfigValue();

                System.Drawing.Point dp = new System.Drawing.Point();
                dp.X = common.DicFreamLocation[common.DicKey_Left];
                dp.Y = common.DicFreamLocation[common.DicKey_Top];

                double left, top, width, height;

                if (common.CheckArea(dp))
                {
                    left = common.DicFreamLocation[common.DicKey_Left];
                    top = common.DicFreamLocation[common.DicKey_Top];
                    width = common.DicFreamLocation[common.DicKey_Width];
                    height = common.DicFreamLocation[common.DicKey_Height];
                }
                else
                {
                    dp = new System.Drawing.Point();
                    dp = common.GetMousePoint();
                    left = dp.X - 50;
                    top = dp.Y - 50;
                    width = common.DicFreamLocation[common.DicKey_Width];
                    height = common.DicFreamLocation[common.DicKey_Height];
                    common.setConfigFreamLocation(this);
                }

                common.winDrug = this;
                Fream Fr = new Fream(left, top, width, height);
                common.ChangeMode(common.CpT_mode);
            }
        }

        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //******************************************************************
        private void Mouse_down(object sender, MouseButtonEventArgs e)
        {
            common.Pdown =  Mouse.GetPosition(this);
            //common.Pdown = this.PointToScreen(Mouse.GetPosition(this));

            common.flgDrug = true;


            // 描画オブジェクトの生成
            this.currentRect = new System.Windows.Shapes.Rectangle
            { 
                Stroke = System.Windows.Media.Brushes.Red,
                StrokeThickness = 1
            };
            //this.currentRect.Opacity = 0.75;
            Canvas.SetLeft(this.currentRect, common.Pdown.X);
            Canvas.SetTop(this.currentRect, common.Pdown.Y);

            // オブジェクトをキャンバスに追加
            this.dCanvas.Children.Add(this.currentRect);


        }

        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //******************************************************************
        private void Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            this.dCanvas.Children.Clear();
            common.DoEvents();//画面を更新

            common.Pup = Mouse.GetPosition(this);
            //common.Pup = this.PointToScreen(Mouse.GetPosition(this));

            common.PointSet(this.PointToScreen(common.Pdown), this.PointToScreen(common.Pup));

            double differenceX = common.PointEnd.X - common.PointStart.X;
            double differenceY = common.PointEnd.Y - common.PointStart.Y;

            if (differenceX <= 15 || differenceY <= 15)
            {
                dCanvas.Children.Remove(currentRect);
                common.flgDrug = false;
                return;
            }

            common.flgImageSet = true;
            common.flgDrug = false;
            this.currentRect = null;

            common.ViewWindow(this, false);
            this.Hide();

            common.winDrug.Width = 10;
            common.winDrug.Height = 10;

            

            //Ctrlボタン状態を取得
            var keyCtrl_L = Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down;
            var keyCtrl_R = Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down;

            if (keyCtrl_L == KeyStates.Down || keyCtrl_R == KeyStates.Down)
            {
                double left = common.PointStart.X;
                double top = common.PointStart.Y;
                double width = common.PointEnd.X - common.PointStart.X;
                double height = common.PointEnd.Y - common.PointStart.Y;

                Fream frm_FR = new Fream(left, top, width, height);
                this.Visibility = Visibility.Hidden;
                common.ChangeMode((int)enm_mode.fream);
            }
            else
            {
                ViewImage VI = new ViewImage(common.PointStart, common.PointEnd);
                VI.Show();
            }
        }


        //******************************************************************
        private void CanvasMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!common.flgDrug) return;

            System.Windows.Point pMouse = new System.Windows.Point();

            pMouse = Mouse.GetPosition(dCanvas);
            //pMouse = this.PointToScreen(Mouse.GetPosition(dCanvas));


            double x = Math.Min(pMouse.X, common.Pdown.X);
            double y = Math.Min(pMouse.Y, common.Pdown.Y);
            double width = Math.Max(pMouse.X, common.Pdown.X) - x;
            double height = Math.Max(pMouse.Y, common.Pdown.Y) - y;

            // 描画中オブジェクトの情報を更新
            this.currentRect.Width = width;
            this.currentRect.Height = height;

            Canvas cnb = new Canvas();
            
            Canvas.SetLeft(this.currentRect, x);
            Canvas.SetTop(this.currentRect, y);
        }


    }
}
