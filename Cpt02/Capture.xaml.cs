using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ctrl_Dll;


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

            //描画先とするImageオブジェクトを作成する
            cls_Etc.getScreenSize(ref common.ScreenX1, ref common.ScreenY1);

            this.WindowState = WindowState.Maximized;
            this.Topmost = true;

            common.ViewWindow(this, true);
        }


        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //******************************************************************
        private void Key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                common.AppClose();
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
            common.Pdown = Mouse.GetPosition(this);
            common.flgDrug = true;


            // 描画オブジェクトの生成
            this.currentRect = new System.Windows.Shapes.Rectangle
            {
                Stroke = System.Windows.Media.Brushes.Green,
                StrokeThickness = 1
            };
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
            common.PointSet(common.Pdown, common.Pup);

            common.flgImageSet = true;
            common.flgDrug = false;
            this.currentRect = null;

            common.ViewWindow(this, false);
            this.Hide();
            
            ViewImage VI = new ViewImage(common.PointStart, common.PointEnd);
            VI.Show();

        }


        //******************************************************************
        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (!common.flgDrug) return;

            System.Windows.Point pMouse = new System.Windows.Point();

            pMouse = Mouse.GetPosition(dCanvas);


            double x = Math.Min(pMouse.X, common.Pdown.X);
            double y = Math.Min(pMouse.Y, common.Pdown.Y);
            double width = Math.Max(pMouse.X, common.Pdown.X) - x;
            double height = Math.Max(pMouse.Y, common.Pdown.Y) - y;

            // 描画中オブジェクトの情報を更新
            this.currentRect.Width = width;
            this.currentRect.Height = height;
            Canvas.SetLeft(this.currentRect, x);
            Canvas.SetTop(this.currentRect, y);
        }
    }
}
