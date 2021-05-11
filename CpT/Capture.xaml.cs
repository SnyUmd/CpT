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

            this.Width = common.ScreenX1;
            this.Height = common.ScreenY1;

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
            Point position = Mouse.GetPosition(this);
            common.MdownX = position.X;
            common.MdownY = position.Y;
            MessageBox.Show($"X = {common.MdownX},  Y = {common.MdownY}");
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
            Point position = Mouse.GetPosition(this);
            common.MupX = position.X;
            common.MupY = position.Y;

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
