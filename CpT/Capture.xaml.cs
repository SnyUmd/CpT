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
        public Capture()
        {
            InitializeComponent();
            cls_Etc.getScreenSize(ref common.ScreenX1, ref common.ScreenY1);

            this.Width = common.ScreenX1;
            this.Height = common.ScreenY1;
        }

        private void MouseLeftBD(object sender, MouseButtonEventArgs e)
        {
        }

        private void Key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
                //this.Close();
        }

        private void Mouse_down(object sender, MouseButtonEventArgs e)
        {
            common.CursorX = 
            MessageBox.Show("Down");
        }

        private void Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Up");

        }
    }
}
