using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CpT
{
    class Init
    {

        public static string strNL = "\r\n";
        //public static string strConfigFileName = "CpT.config";
        public static int left = 100, top = 100, width = 250, height = 150;

        //******************************************************************
        public static void GetScreenSize()
        {
            common.ScreenW = (int)SystemParameters.WorkArea.Width;
            common.ScreenH = (int)SystemParameters.WorkArea.Height;
        }

        //******************************************************************
        /// <summary>
        /// コンフィグファイルが無ければ作成
        /// </summary>
        //******************************************************************
        public static void MakeConfigFile()
        {
            string fileName = common.lst_strDir[(int)enmDirNum.Applli] + common.strConfigFileName;
            //string SetValue = "";
            string buf;

            if (!common.clsFC.File_Fined(fileName))
            {
                common.clsFC.File_Create(fileName);

                buf = common.clsTC.mEnctyption(common.clsFC.MyPicture_Directory(), common.aryEncryptionKey);

                common.configValue = $"{common.DicKey_Left},{left}{strNL}" +
                     $"{common.DicKey_Top},{top}{strNL}" +
                     $"{common.DicKey_Width},{width}{strNL}" +
                     $"{common.DicKey_Height},{height}{strNL}" +
                     $"{common.DicKey_Save},{buf}";
                //★ここにチェックボックス情報を追加

                common.clsFC.Txt_File_Write(fileName, common.configValue, true);
            }
        }

        //******************************************************************
        /// <summary>
        /// コンフィグファイルの中身を取得
        /// </summary>
        //******************************************************************
        public static void ReadConfigValue()
        {
            string buf;
            string fileName = common.lst_strDir[(int)enmDirNum.Applli] + common.strConfigFileName;
            string[] rn = { "\r\n" };
            string[] aryValue;
            string[] Line;

            common.DicFreamLocation.Clear();
            common.configValue = common.clsFC.Txt_File_Read(fileName);

            aryValue = common.configValue.Split(rn, StringSplitOptions.None);

            for (int i = 0; i < aryValue.Length; i++)
            {
                Line = aryValue[i].Split(',');

                if (i < aryValue.Length - 1)
                    common.DicFreamLocation.Add(Line[0], int.Parse(Line[1]));
                else
                {
                    buf = common.clsTC.mRestoration(Line[1], common.aryEncryptionKey);

                    buf = (common.clsFC.Folder_Fined(buf)) ? buf : common.lst_strDir[(int)enmDirNum.Desktop];
                    
                    common.lst_strDir[(int)enmDirNum.Save] = buf;
                    /*if (common.clsFC.Folder_Fined(buf))
                        common.lst_strDir[(int)enmDirNum.Save] = buf;
                    else
                        common.lst_strDir[(int)enmDirNum.Save] = common.lst_strDir[(int)enmDirNum.Desktop];*/
                    //★ここにチェックボックス情報を追加
                }
            }
        }
    }
}
