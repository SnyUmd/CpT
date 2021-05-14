using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
//using Point = System.Windows.Point;

//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Windows.Threading;

namespace CpT
{
    public static class common
    {
        public static bool flgImageSet = false;
        public static bool flgDrug = false;
        public static bool flgMoutDown = false;

        public static int ScreenX0 = 0;
        public static int ScreenX1 = 0;
        public static int ScreenY0 = 0;
        public static int ScreenY1 = 0;

        public static int CursorX = 0;
        public static int CursorY = 0;

        //public static double MdownX = 0;
        //public static double MdownY = 0;
        //public static double MupX = 0;
        //public static double MupY = 0;

        public static System.Windows.Point Pdown, Pup;

        public static System.Windows.Point PointStart = new System.Windows.Point(0, 0);
        public static System.Windows.Point PointEnd = new System.Windows.Point(0, 0);

        public static System.Windows.Point point;

        public static Bitmap Bpm;
       

        //******************************************************************
        public static void AppInit(Window w)
        {
            point = Mouse.GetPosition(w);
        }

        //******************************************************************
        public static void AppClose()
        {
            System.Windows.Application.Current.Shutdown();

        }

        //public static void PointSet(double X0, double X1, double Y0, double Y1)
        public static void PointSet(System.Windows.Point p0, System.Windows.Point p1)
        {
            if (p0.X > p1.X)
            {
                PointStart.X = p1.X - 7;
                PointEnd.X = p0.X - 7;
            }
            else
            {
                PointStart.X = p0.X - 7;
                PointEnd.X = p1.X - 7;
            }

            //if (Y0 > Y1)
            if (p0.Y > p1.Y)
            {
                PointStart.Y = p1.Y - 7;
                PointEnd.Y = p0.Y - 7;
            }
            else
            {
                PointStart.Y = p0.Y - 7;
                PointEnd.Y = p1.Y - 7;
            }


        }

        //******************************************************************
        public static void FlgReset()
        {
            flgImageSet = false;
            flgDrug = false;
        }


        //******************************************************************
        public static Bitmap GetBitmap(System.Windows.Point p_start, System.Windows.Point p_end)
        {
            System.Drawing.Point dStartPoint = new System.Drawing.Point(0, 0);
            System.Drawing.Point dEndPoint = new System.Drawing.Point(0, 0);

            dStartPoint.X = (int)p_start.X;
            dStartPoint.Y = (int)p_start.Y;
            dEndPoint.X = (int)dEndPoint.X;
            dEndPoint.Y = (int)dEndPoint.Y;

            int width = (int)p_end.X - (int)p_start.X;
            int height = (int)p_end.Y - (int)p_start.Y;


            //Bitmapの作成
            Bitmap bmp = new Bitmap(width, height);

            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //画面全体をコピーする
            g.CopyFromScreen(dStartPoint, dEndPoint, bmp.Size);
            //解放
            g.Dispose();

            //表示
            return bmp;
        }

        //******************************************************************
        public static bool SetImgCtrl(Bitmap bmp, System.Windows.Controls.Image img )
        {
            IntPtr hBitmap = bmp.GetHbitmap();

            try
            {
                img.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }


        //******************************************************************
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            var callback = new DispatcherOperationCallback(obj =>
            {
                ((DispatcherFrame)obj).Continue = false;
                return null;
            });
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            Dispatcher.PushFrame(frame);
        }

        //******************************************************************
        public static void AreaChek(double X0, double X1, double Y0, double Y1)
        {

        }

        //******************************************************************
        public static void ViewWindow(Window win, bool appear)
        {
            if(appear)
            {
                for (double i = 1; i < 30; i++)
                {
                    win.Opacity = i * 0.01;
                    common.DoEvents();
                    System.Threading.Thread.Sleep(3);
                }
            }
            else
            {
                for (double i = 30; i > 0; i--)
                {
                    win.Opacity = i * 0.01;
                    common.DoEvents();
                    System.Threading.Thread.Sleep(3);
                }
            }
        }

    }

}


//******************************************************************

