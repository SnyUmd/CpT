using System;
using System.Windows;

namespace CpT
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        //******************************************************************
        private void appDeactiv(object sender, EventArgs e)
        {
            if (!common.flgImageSet) common.AppClose();
        }
    }
}
