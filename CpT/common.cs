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

        //******************************************************************
        public static void AreaChek(double X0, double X1, double Y0, double Y1)
        {

        }

    }

}


//******************************************************************

