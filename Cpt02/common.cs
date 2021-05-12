using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace CpT
{
    public static class common
    {
        public static bool flgIdle = true;

        public static int ScreenX0 = 0;
        public static int ScreenX1 = 0;
        public static int ScreenY0 = 0;
        public static int ScreenY1 = 0;

        public static int CursorX = 0;
        public static int CursorY = 0;

        public static double MdownX = 0;
        public static double MdownY = 0;
        public static double MupX = 0;
        public static double MupY = 0;

        public static double StartPointX = 0;
        public static double StartPointY = 0;
        public static double EndPointX = 0;
        public static double EndPointY = 0;


        public static System.Windows.Point point;

       

        //******************************************************************
        public static void AppInit(Window w)
        {
            point = Mouse.GetPosition(w);
        }

        //******************************************************************
        public static void AppClose()
        {
            Application.Current.Shutdown();

        }

        public static void PointSet(double X0, double X1, double Y0, double Y1)
        {
            if (X0 > X1)
            {
                StartPointX = X1;
                EndPointX = X0;
            }
            else
            {
                StartPointX = X0;
                EndPointX = X1;
            }

            if (Y0 > Y1)
            {
                StartPointY = Y1;
                EndPointY = Y0;
            }
            else
            {
                StartPointY = Y0;
                EndPointY = Y1;
            }


        }




        //******************************************************************
        //private static System.Drawing.Image GetCaptureImage(Rectangle rect)
        //private static System.Drawing.Image GetCaptureImage(int x_start, int x_end, int y_start, int y_end)
        //{

        //    var rectangle = new Rectangle(x_start, y_start, x_end - x_start, y_end - y_start);
        //    var bitmap = new Bitmap(rectangle.Width, rectangle.Height);
        //    var graphics = Graphics.FromImage(bitmap);
        //    graphics.CopyFromScreen(new System.Drawing.Point(rectangle.X, rectangle.Y), new System.Drawing.Point(0, 0), bitmap.Size);
        //    // グラフィックスの解放
        //    graphics.Dispose();

        //    // 画像の表示
        //    using (var stream = new MemoryStream())
        //    {
        //        bitmap.Save(stream, ImageFormat.Png);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        System.Drawing.Image.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        //    }
        //}


        //******************************************************************
        public static void AreaChek(double X0, double X1, double Y0, double Y1)
        {

        }

    }

}


//******************************************************************

