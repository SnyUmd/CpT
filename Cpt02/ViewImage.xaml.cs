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
        Bitmap bpmImage;
        public ViewImage(Bitmap bpm)
        {
            InitializeComponent();
            this.Topmost = true;
            bpmImage = bpm;
            this.Width = bpm.Width * 1.05;
            this.Height = bpm.Height + 38;
            IntPtr hBitmap = bpmImage.GetHbitmap();
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
    }
}
