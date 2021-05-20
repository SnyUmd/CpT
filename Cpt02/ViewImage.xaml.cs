using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Input;


namespace CpT
{

    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class ViewImage : Window
    {
        bool flgCtrl = false;

        Bitmap bmpImage;
        //******************************************************************
        public ViewImage(System.Windows.Point p_start, System.Windows.Point p_end)
        {
            InitializeComponent();
            this.Topmost = true;
            

            //common.Bpm = common.GetBitmap(common.PointStart, common.PointEnd);
            common.Bpm = common.GetBitmap(p_start, p_end);

            bmpImage = common.Bpm;
            int w = bmpImage.Width;
            int h = bmpImage.Height;

            this.Width = w + 2;
            this.Height = h + 2;

            common.SetImgCtrl(bmpImage, img);

            this.Left = 0;
            this.Top = 0;
        }
                

        //******************************************************************
        private void close(object sender, EventArgs e)
        {
            common.AppClose();
        }

        //******************************************************************
        private void mouseL_Bdown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //******************************************************************
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Delete || e.Key == Key.Back)
                common.AppClose();

            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                flgCtrl = true;

            if (flgCtrl && e.Key == Key.C)
            {
                Clipboard.SetData(DataFormats.Bitmap, bmpImage);
                flgCtrl = false;
            }

            else if (flgCtrl && e.Key == Key.S)
            {
                string strFile = "";

                strFile = common.PngFileSave(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\");
                if (strFile == "") return;

                bmpImage.Save(strFile, ImageFormat.Png);
                flgCtrl = false;
            }
        }

        //******************************************************************
        private void MouseDclick(object sender, MouseButtonEventArgs e)
        {
            //common.AppClose();
            this.WindowState = WindowState.Minimized;
        }


    }
}
