﻿using System;
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
        bool flgCtrl = false;
        string NewLine = "\r\n";

        Bitmap bmpImage;

        //******************************************************************
        public ViewImage(System.Windows.Point p_start, System.Windows.Point p_end)
        {
            InitializeComponent();
            this.Topmost = true;
            

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

            bmpImage.Dispose();
        }
        //******************************************************************
        private void FlontSw(bool blFlont)
        {
            if(blFlont)
                this.Background = new SolidColorBrush(Colors.Red);
            else
                this.Background = new SolidColorBrush(Colors.Blue);

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
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == KeySts.Key_Close0 || e.Key == KeySts.Key_Close1)// || e.Key == KeySts.Key_Close2)
                common.AppClose();

            if (e.Key == KeySts.Key_CtrlL || e.Key == KeySts.Key_CtrlR)
                flgCtrl = true;

            if (e.Key == KeySts.Key_AlwaysFlongSW) FlontSw(!this.Topmost);

            if (flgCtrl && e.Key == KeySts.Key_Copy)
            {
                Clipboard.SetData(DataFormats.Bitmap, bmpImage);
                flgCtrl = false;
            }

            else if (flgCtrl && e.Key == KeySts.Key_Save)
            {
                string strFile = "";
                string strFolder = "";

                Init.ReadConfigValue();

                //ファイル保存ダイアログを表示して、ファイル名を取得(Dirリストフォルダ)
                strFile = common.PngFileSave(common.lst_strDir[(int)enmDirNum.Save]);
                //キャンセルを押されたら抜ける
                if (strFile == "") return;

                //フォルダを抽出
                strFolder = common.clsFC.Get_Folder_Name(strFile);
                //フォルダリストにセットconfigファイルにセット
                common.setConfigSaveDir(strFolder);

                try
                {
                    bmpImage.Save(strFile, ImageFormat.Png);
                }
                catch (Exception)
                {
                    MessageBox.Show("ファイルの保存に失敗しました。" );
                }
                flgCtrl = false;
            }

            flgCtrl = false;
        }

        //******************************************************************
        private void MouseDclick(object sender, MouseButtonEventArgs e)
        {
            //common.AppClose();
            this.WindowState = WindowState.Minimized;
        }


    }
}
