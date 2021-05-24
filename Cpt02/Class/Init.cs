﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpT
{
    class Init
    {

        public static string strNL = "\r\n";
        //public static string strConfigFileName = "CpT.config";
        public static int left = 100, top = 100, width = 250, height = 150;

        //******************************************************************
        /// <summary>
        /// ディレクトリ情報を取得
        /// </summary>
        //******************************************************************
        /*public static void ReadDir()
        {
            for (int i = 0; i < sizeof(enmDirNum); i++)
            {
                switch (i)
                {
                    case (int)enmDirNum.Applli:
                        common.lst_strDir.Add(common.clsFC.App_Directory_Acquisition());
                        break;
                    case (int)enmDirNum.Desktop:
                        common.lst_strDir.Add(common.clsFC.Desk_Top_Directory());
                        break;
                    case (int)enmDirNum.Document:
                        common.lst_strDir.Add(common.clsFC.Mydocument_Directory());
                        break;
                    case (int)enmDirNum.Save:
                        common.lst_strDir.Add("");
                        break;
                }
            }
        }*/

        //******************************************************************
        /// <summary>
        /// コンフィグファイルが無ければ作成
        /// </summary>
        //******************************************************************
        public static void MakeConfigFile()
        {
            string fileName = common.lst_strDir[(int)enmDirNum.Applli] + common.strConfigFileName;
            //string SetValue = "";

            if (!common.clsFC.File_Fined(fileName))
            {
                common.clsFC.File_Create(fileName);

                common.configValue = $"{common.DicKey_Left},{left}{strNL}" +
                     $"{common.DicKey_Top},{top}{strNL}" +
                     $"{common.DicKey_Width},{width}{strNL}" +
                     $"{common.DicKey_Height},{height}{strNL}" +
                     $"{common.DicKey_Save},{common.clsFC.MyPicture_Directory()}";

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
                    common.lst_strDir[(int)enmDirNum.Save] = Line[1];
            }
        }
    }
}