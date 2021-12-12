using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace CpT
{
    enum enm_mode
    {
        drag = 0,
        fream,
        View
    }

    enum enmDirNum
    {
        Applli,
        Desktop,
        Document,
        Save
    }

    enum enmConfigValue
    {
        left,
        top,
        width,
        heitht,
        saveDir,
        ここにチェックボックスフラグ情報を追加
    }

    public static class common
    {
        private static string nl = "\r\n";

        public static int ScreenW = 0;
        public static int ScreenH = 0;

        public static int[] aryEncryptionKey = { 1, 3, 5, 2, 4, 6 };

        public static string DicKey_Left = "Left";
        public static string DicKey_Top = "Top";
        public static string DicKey_Width = "Width";
        public static string DicKey_Height = "Height";
        public static string DicKey_Save = "SaveDir";
        //★ここにキーを追加

        public static string strConfigFileName = "CpT.config";

        public static Ctrl_Dll.cls_FileCtrl clsFC = new Ctrl_Dll.cls_FileCtrl();
        public static Ctrl_Dll.cls_TextCtrl clsTC = new Ctrl_Dll.cls_TextCtrl();

        public static List<string> lst_strDir = new List<string>()
        {
            clsFC.App_Directory_Acquisition(),
            clsFC.Desk_Top_Directory(),
            clsFC.Mydocument_Directory(),
            ""
        };
        public static Dictionary<string, int> DicFreamLocation = new Dictionary<string, int>();

        public static string configValue = "";

        public static Window winDrug;
        public static Window winFream;
        public static Window winView;

        public static int CpT_mode = 0;

        public static bool flgImageSet = false;
        public static bool flgDrug = false;
        public static bool flgMoutDown = false;
        public static bool blModeChange = false;

        public static int ScreenX0 = 0;
        public static int ScreenX1 = 0;
        public static int ScreenY0 = 0;
        public static int ScreenY1 = 0;

        public static int CursorX = 0;
        public static int CursorY = 0;

        public static System.Windows.Point Pdown, Pup;

        public static System.Windows.Point PointStart = new System.Windows.Point(0, 0);
        public static System.Windows.Point PointEnd = new System.Windows.Point(0, 0);

        public static System.Windows.Point point;

        public static Bitmap Bpm;


        //******************************************************************
        /// <summary>
        /// configファイルのフレーム位置がスクリーン内に収まっているか判定
        /// </summary>
        /// <returns></returns>
        //******************************************************************
        public static bool CheckScreenSize()
        {
            int ScreenW = (int)SystemParameters.WorkArea.Width;
            int ScreenH = (int)SystemParameters.WorkArea.Height;

            if (0 > common.DicFreamLocation[common.DicKey_Left] ||
                0 > common.DicFreamLocation[common.DicKey_Top] ||
                ScreenW < common.DicFreamLocation[common.DicKey_Left] ||
                ScreenH < common.DicFreamLocation[common.DicKey_Top]) return false;

            else return true;
        }

        //******************************************************************
        public static void ResetLocation()
        {

        }

        //******************************************************************
        /// <summary>
        /// フレームの位置情報をConfigファイルに書込み
        /// </summary>
        /// <param name="win"></param>
        //******************************************************************
        public static void setConfigFreamLocation(Window win)
        {
            //フォルダ名を暗号化
            string buf = common.clsTC.mEnctyption(lst_strDir[(int)enmDirNum.Save], common.aryEncryptionKey);

            configValue = $"{DicKey_Left},{win.Left}{nl}" +
                          $"{DicKey_Top},{win.Top}{nl}" +
                          $"{DicKey_Width},{win.Width}{nl}" +
                          $"{DicKey_Height},{win.Height}{nl}" +
                          $"{DicKey_Save},{buf}";
            //★ここにチェックボックス情報を追加

            //コンフィグファイルに情報を書込み
            clsFC.Txt_File_Write(common.lst_strDir[(int)enmDirNum.Applli] + strConfigFileName, configValue, true);
        }

        //******************************************************************
        /// <summary>
        /// セーブフォルダをセット
        /// </summary>
        /// <param name="setDir">新規フォルダ</param>
        //******************************************************************
        public static void setConfigSaveDir(string setDir)
        {
            //フォルダ名を暗号化
            string buf = ""; //common.clsTC.mEnctyption(lst_strDir[(int)enmDirNum.Save], common.aryEncryptionKey);
            //string buf = "";
            int iBuf;

            //コンフィグ情報からフォルダ名を削除
            //configValue = configValue.Replace(buf, "");
            iBuf = configValue.IndexOf(DicKey_Save) + DicKey_Save.Length + 1;
            configValue = configValue.Remove(iBuf, configValue.Length - iBuf);

            //フォルダリストにセーブフォルダをセット
            lst_strDir[(int)enmDirNum.Save] = setDir;

            //新規フォルダを暗号化
            //setDir = common.clsTC.mEnctyption(setDir, common.aryEncryptionKey);
            buf = common.clsTC.mEnctyption(setDir, common.aryEncryptionKey);
            //コンフィグ情報へ新規フォルダをセット
            //configValue += setDir;
            configValue += buf;
            //コンフィグファイルに情報を書込み
            clsFC.Txt_File_Write(common.lst_strDir[(int)enmDirNum.Applli] + strConfigFileName, configValue, true);
        }


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
        //******************************************************************
        public static void PointSet(System.Windows.Point p0, System.Windows.Point p1)
        {
            if (p0.X > p1.X)
            {
                PointStart.X = p1.X;
                PointEnd.X = p0.X;
            }
            else
            {
                PointStart.X = p0.X;
                PointEnd.X = p1.X;
            }

            //if (Y0 > Y1)
            if (p0.Y > p1.Y)
            {
                PointStart.Y = p1.Y;
                PointEnd.Y = p0.Y;
            }
            else
            {
                PointStart.Y = p0.Y;
                PointEnd.Y = p1.Y;
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
        public static bool SetImgCtrl(Bitmap bmp, System.Windows.Controls.Image img)
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
            catch (Exception)
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
        public static void ViewWindow(Window win, bool appear)
        {
            if (appear) win.Opacity = 0.4;
            else win.Opacity = 0;

            /*
            if (appear)
            {
                for (double i = 1; i < 10; i++)
                {
                    win.Opacity = i * 0.04;
                    common.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }
            }
            else
            {
                for (double i = 10; i > 0; i--)
                {
                    win.Opacity = i * 0.04;
                    common.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }
            }
            */

        }
        //******************************************************************
        public static string PngFileSave(string str_top_dir)
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();
            //はじめに「ファイル名」で表示される文字列を指定する
            sfd.FileName = "NewFile";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = str_top_dir;

            //[ファイルの種類]に表示される選択肢を指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            sfd.Filter = "Pngファイル(*.Png)|*.Png";

            //[ファイルの種類]ではじめに選択されるものを指定する
            sfd.FilterIndex = 1;


            //タイトルを設定する
            sfd.Title = "Please select a save destination file";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                return sfd.FileName;
            else
                return "";
        }

        //******************************************************************
        public static void ChangeMode(int mode_num)
        {
            Window win = new Window();
            switch (mode_num)
            {

                case (int)enm_mode.drag:
                    win = common.winDrug;
                    common.winDrug.Visibility = Visibility.Visible;
                    break;
                case (int)enm_mode.fream:
                    win = common.winFream;
                    win.Show();
                    break;
                case (int)enm_mode.View:
                    win = common.winView;
                    win.Show();
                    break;
                default:
                    return;
            }

            ViewWindow(win, true);

        }

        //******************************************************************
        public static System.Drawing.Point GetMousePoint()
        {
            System.Drawing.Point pResult = new System.Drawing.Point();
            pResult = System.Windows.Forms.Cursor.Position;
            return pResult;
        }

        //******************************************************************
        public static bool CheckArea(System.Drawing.Point f_point)
        {
            bool blResult = false;
            bool flPrimary = (f_point.X < 0 || f_point.Y < 0) ? false : true;
            var screen = System.Windows.Forms.Screen.AllScreens;

            System.Drawing.Point CriteriaPoint = new System.Drawing.Point();
            int CriteriaW = 0, CriteriaH = 0;
            int CriteriaLeft = 0, CriteriaTop = 0;
            int CriteriaRight= 0, CriteriaBottom = 0;

            foreach (Screen s in screen)
            {
                if (s.Primary && !flPrimary) continue;
                else
                {
                    CriteriaPoint = s.Bounds.Location;
                    CriteriaW = s.Bounds.Width;
                    CriteriaH = s.Bounds.Height;
                    CriteriaLeft = s.Bounds.Left;
                    CriteriaTop = s.Bounds.Top;
                    CriteriaRight = s.Bounds.Right;
                    CriteriaBottom = s.Bounds.Bottom;
                    /*System.Windows.MessageBox.Show($"CriteriaW = {CriteriaW}, CriteriaH = {CriteriaH}\r\n" +
                                                   $"CriteriaL = {CriteriaLeft}, CriteriaT = {CriteriaTop}\r\n" +
                                                   $"CriteriaR = {CriteriaRight}, CriteriaB = {CriteriaBottom}");*/

                    if (CriteriaLeft < f_point.X &&
                       CriteriaRight > f_point.X &&
                       CriteriaTop < f_point.Y &&
                       CriteriaBottom > f_point.Y)
                    {
                        blResult = true;
                        break;
                    }

                }
            }
            return blResult;
        }

        //public static void MsgView(string ms, double x, double y)
        public static void MsgView(string ms, Window win)
        {
            MsgB msgB = new MsgB(ms, win.Left + win.Width / 2, win.Top + win.Height / 2);
            msgB.Show();
        }


        public static void ClipBoad(Bitmap bi, Window win)
        {
            System.Windows.Clipboard.SetData(System.Windows.DataFormats.Bitmap, bi);
            MsgView("画像をクリップボードに保管しました。", win);
        }

    }

}


//******************************************************************

