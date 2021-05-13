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
//using System.Windows.Forms;
using Ctrl_Dll;


namespace CpT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Capture : Window
    {


        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        //******************************************************************
        public Capture()
        {
            InitializeComponent();
            cls_Etc.getScreenSize(ref common.ScreenX1, ref common.ScreenY1);

            this.WindowState = WindowState.Maximized;
            this.Topmost = true;


        }

        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //******************************************************************
        private void MouseLeftBD(object sender, MouseButtonEventArgs e)
        {
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
            this.Hide();
            

            //common.Bpm = common.GetBitmap(common.PointStart, common.PointEnd);

            //int widht = (int)common.PointEnd.X - (int)common.PointStart.X;
            //int height = (int)common.PointEnd.Y - (int)common.PointStart.Y;
            ViewImage VI = new ViewImage(common.PointStart, common.PointEnd);
            VI.Show();

            
            //PictureBox pb = new PictureBox();
            //pb.Image = common.Bpm;

            //System.Windows.Clipboard.SetImage(pb.Image);
            //MessageBox.Show($"Start : {common.PointStart.X},{common.PointStart.Y}  End : {common.PointEnd.X}, {common.PointEnd.Y}");
        }

        //******************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //******************************************************************
        private void deActiv(object sender, EventArgs e)
        {

        }
    }
}
