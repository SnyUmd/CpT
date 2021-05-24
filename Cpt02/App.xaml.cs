using System;
using System.Windows;
using Ctrl_Dll;

namespace CpT
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        //public static string strNL = "\r\n";

        //******************************************************************
        App()
        {
            //Init.ReadDir();
            Init.MakeConfigFile();
            Init.ReadConfigValue();
        }




        private void appDeactiv(object sender, EventArgs e)
        {
            //if (!common.flgImageSet && common.CpT_mode == (int)enm_mode.drag) common.AppClose();
        }
    }
}
