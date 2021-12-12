#define DEB

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CpT
{

    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class ViewImage : Window
    {
        //string NewLine = "\r\n";

        Bitmap bmpImage;

        //******************************************************************
        public ViewImage(System.Windows.Point p_start, System.Windows.Point p_end)
        {
            InitializeComponent();
            this.Topmost = true;//最前面表示


            //common.Bpm = common.GetBitmap(common.PointStart, common.PointEnd);
            common.Bpm = common.GetBitmap(p_start, p_end);

            bmpImage = common.Bpm;
            int w = bmpImage.Width;
            int h = bmpImage.Height;

            this.Width = w + 2;
            this.Height = h + 2;

            common.SetImgCtrl(bmpImage, img);

            //開いた段階でクリップボードにセット
            Clipboard.SetData(DataFormats.Bitmap, bmpImage);

            //this.Left = 0;
            //this.Top = 0;
            int offSetW = 0;
            int offSetH = 0;
            if (!(p_start.X + this.Width > SystemParameters.WorkArea.Width - 20)) offSetW = 20;
            if (!(p_start.Y + this.Height > SystemParameters.WorkArea.Height - 20)) offSetH = 20;

            this.Left = p_start.X + offSetW;
            this.Top = p_start.Y + offSetH;

            this.Topmost = true;
            if(this.Topmost)
                FlontSw(this.Topmost, false);
            else
                FlontSw(this.Topmost, true);

        }

        //******************************************************************
        private void FlontSw(bool blFlont, bool bl_size_change)
        {
            if (blFlont)
            {
                this.Background = new SolidColorBrush(Colors.Red);
                this.Topmost = false;
                this.MI_Topmost.Header = "最前面表示を無効にする [space]";
                this.WindowStyle = WindowStyle.None;
                /*if (bl_size_change)
                {
                    this.Width -= 16;
                    this.Height -= 39;
                }*/
            }
            else
            {
                this.Background = new SolidColorBrush(Colors.Blue);
                this.Topmost = true;
                this.MI_Topmost.Header = "最前面表示を有効にする [space]";
                this.WindowStyle = WindowStyle.None;
                /*this.WindowStyle = WindowStyle.SingleBorderWindow;
                if (bl_size_change)
                {
                    this.Width += 16;
                    this.Height += 39;
                }*/
            }

            this.Topmost = blFlont;

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
        private void keyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl) flgCtrl = false;
        }

        //******************************************************************
        private void keyDown(object sender, KeyEventArgs e)
        {
            var keyCtrl_L = Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down;
            var keyCtrl_R = Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down;
            //var keyCtrl = Keyboard.Modifiers == ModifierKeys.Shift;

            if (keyCtrl_L == KeyStates.Down || keyCtrl_R == KeyStates.Down)
            {
                if (e.Key == KeySts.Key_Copy)
                    common.ClipBoad(bmpImage, this);

                if (e.Key == KeySts.Key_Save) ImageSave();
            }
            else
            {
                if (e.Key == KeySts.Key_Close0 || e.Key == KeySts.Key_Close1)// || e.Key == KeySts.Key_Close2)
                    common.AppClose();

                else if (e.Key == KeySts.Key_AlwaysFlongSW) FlontSw(!this.Topmost, true);

                else if (e.Key == KeySts.Key_NewApp) AddCpT();
            }

            /*if (flgCtrl)
            {
                flgCtrl = false;
                if (e.Key == KeySts.Key_Copy)
                    Clipboard.SetData(DataFormats.Bitmap, bmpImage);

                if (e.Key == KeySts.Key_Save) ImageSave();
            }
            else
            {
                if (e.Key == KeySts.Key_Close0 || e.Key == KeySts.Key_Close1)// || e.Key == KeySts.Key_Close2)
                    common.AppClose();

                else if (e.Key == KeySts.Key_CtrlL || e.Key == KeySts.Key_CtrlR)
                    flgCtrl = true;

                else if (e.Key == KeySts.Key_AlwaysFlongSW) FlontSw(!this.Topmost, true);

                else if (e.Key == KeySts.Key_NewApp) AddCpT();
            }*/
        }

        private void AddCpT()
        {
            this.WindowState = WindowState.Minimized;
            string buf = common.lst_strDir[(int)enmDirNum.Applli] + @"CpT.exe";
            common.clsFC.Ex_App_Start(buf, false);
        }
        //******************************************************************
        private void ImageSave()
        {
            string strFile = "";
            string strFolder = "";
            string strStartFolder = "";
            //string buf;

            int iFolderNum = (int)enmDirNum.Save;

            Init.ReadConfigValue();

            //フォルダが存在しなければデスクトップにする。
            if (!common.clsFC.Folder_Fined(common.lst_strDir[(int)enmDirNum.Save]))
                iFolderNum = (int)enmDirNum.Desktop;

            strStartFolder = common.lst_strDir[iFolderNum];

            //ファイル保存ダイアログを表示して、ファイル名を取得(Dirリストフォルダ)
            strFile = common.PngFileSave(strStartFolder);
            //キャンセルを押されたら処理中段
            if (strFile == "") return;

            //フォルダを抽出
            strFolder = common.clsFC.Get_Folder_Name(strFile);
            //フォルダリストにセットconfigファイルにセット
            common.setConfigSaveDir(strFolder);

            try
            {
                bmpImage.Save(strFile, ImageFormat.Png);
                common.MsgView("画像を保存しました", this);
                //MsgB msgB = new MsgB("画像を保存しました", this.Left + this.Width / 2, this.Top + this.Height / 2);
                //msgB.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("ファイルの保存に失敗しました。");
            }
        }

        //******************************************************************
        private void MouseDclick(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //******************************************************************
        private void MI_Copy_Click(object sender, RoutedEventArgs e)
        {
            common.ClipBoad(bmpImage, this);
        }

        //******************************************************************
        private void MI_Topmost_Click(object sender, RoutedEventArgs e)
        {
            FlontSw(!this.Topmost, true);
        }

        //******************************************************************
        private void MI_Save_Click(object sender, RoutedEventArgs e)
        {
            ImageSave();
        }

        //******************************************************************
        private void MI_AddCapture_Click(object sender, RoutedEventArgs e)
        {
            AddCpT();
        }
        //******************************************************************
        private void MI_Close_Click(object sender, RoutedEventArgs e)
        {
            common.AppClose();
        }

        //******************************************************************
        private void MI_Debug_Click(object sender, RoutedEventArgs e)
        {
            common.MsgView("Debug Message", this);
        }

        //******************************************************************
        private void ViewWindow_Loaded(object sender, RoutedEventArgs e)
        {
            common.ClipBoad(bmpImage, this);
        }
    }
}
