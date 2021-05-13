using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


using System.IO;

using Ctrl_Dll;
using System.Drawing.Imaging;

namespace CpT
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class ViewImage : Window
    {
        Bitmap bmpImage;
        public ViewImage(System.Windows.Point p_start, System.Windows.Point p_end)
        {
            InitializeComponent();
            this.Topmost = true;


            common.Bpm = common.GetBitmap(common.PointStart, common.PointEnd);

            bmpImage = common.Bpm;
            this.Width = bmpImage.Width;
            this.Height = bmpImage.Height;

            IntPtr hBitmap = bmpImage.GetHbitmap();
            try
            {
                img.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
            }
        }

        private void close(object sender, EventArgs e)
        {
            common.AppClose();
        }

        private void mouseL_Bdown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl) && e.Key == Key.C)
                ;
        }
    }
}
