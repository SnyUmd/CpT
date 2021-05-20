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
            if (e.Key == Key.Escape)
                common.AppClose();

            if(!common.flgDrug && e.Key == Key.A)
            {
                common.CpT_mode = (int)enm_mode.fream;
                common.ViewWindow(this, false);
                this.Visibility = Visibility.Hidden;

                Fream Fr = new Fream();
                common.ChangeMode(common.CpT_mode, Fr);
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
            
            ViewImage VI = new ViewImage(common.PointStart, common.PointEnd);
            VI.Show();
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
