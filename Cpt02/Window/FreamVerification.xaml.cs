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
using System.Windows.Shapes;

namespace CpT
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class FreamVerification : Window
    {
        public FreamVerification()
        {
            InitializeComponent();
        }

        private void BtnClk(object sender, RoutedEventArgs e)
        {
            if((bool)Ckb.IsChecked)
            {
                //★ここに警告イネーブル情報を、configへ書込み
            }
            this.Close();
        }
    }
}
