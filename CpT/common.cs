﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Interop;

using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

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


        public static Point point;

       

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
        public static Image GetCaptureImage(Rectangle rect)
        {

            // 指定された範囲と同サイズのBitmapを作成する
            Image img = new Bitmap(
                            rect.Width,
                            rect.Height,
                            Imaging.PixelFormat.Format32bppArgb);

            // Bitmapにデスクトップのイメージを描画する
            using (Graphics g = Graphics.FromImage(img))
            {
                g.CopyFromScreen(
                    rect.X,
                    rect.Y,
                    0,
                    0,
                    rect.Size,
                    CopyPixelOperation.SourceCopy);
            }

            return img;
        }


        //******************************************************************
        public static void AreaChek(double X0, double X1, double Y0, double Y1)
        {

        }

    }

}


//******************************************************************

