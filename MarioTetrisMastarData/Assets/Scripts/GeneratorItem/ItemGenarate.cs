using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Tetris;
using Robot;
namespace ItemGenerater
{
    public class ItemGenarate : MonoBehaviour, IGenerator
    {
        [SerializeField] List<GameObject> ItemPrefs = new List<GameObject>();
        Field.FieldBase fieldBase;
        IGetPositionToInfo getPositionToInfo;
        IGetTetrisInfo getTetrisInfo;
        private void Awake()
        {
            Utility.Locator<IGenerator>.Bind(this);
        }
        void Start()
        {
            getPositionToInfo = Utility.Locator<IGetPositionToInfo>.GetT();
            getTetrisInfo = Utility.Locator<IGetTetrisInfo>.GetT();
        }

        void Update()
        {
            FieldInfo fieldKari;
            fieldKari.width = 0;
            fieldKari.height = 0;
            //if (Input.GetKeyDown(KeyCode.Return))
            //{
            //    GenerateItem(TetrisTypeEnum.Type_L, TetrisAngle.Angle_270, fieldKari);
            //}



        }
        public void GenerateItem(ItemName name, Vector3 pos)
        {
            Debug.LogError("アイテム出すけどまだ書いてない");
        }

        public bool CanGenerateTetris(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, FieldInfo info)
        {
            TetrisScriptableObject tetrisScriptable = getTetrisInfo.GetTetrimino(tetrisType, tetrisAngle);
            List<int[]> fieldList = Utility_.FieldData;
            bool flg = true;
            for (int i = 3; i >= 0; i--/*int i = 0; i < 4; i++*/)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tetrisScriptable.tetriminoArrays[i, j])
                    {
                        FieldInfo infomation;
                        infomation.width = j + info.width;
                        infomation.height = i + info.height;
                        if (fieldList[infomation.height][infomation.width] != 0)
                        {
                            flg = false;
                        }
                       
                    }
                }
            }
            return flg;
        }
        public void GenerateItem(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, FieldInfo info)
        {
            fieldBase = Utility.Locator<Field.FieldBase>.GetT();
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            TetrisScriptableObject tetrisScriptable = getTetrisInfo.GetTetrimino(tetrisType, tetrisAngle);
            for (int i = 3; i >= 0; i--/*int i = 0; i < 4; i++*/)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tetrisScriptable.tetriminoArrays[i, j])
                    {
                        //データを読んで配列を並び変える
                        FieldInfo infomation;
                        infomation.width = j + info.width;
                        infomation.height = i + info.height;

                        fieldInfos.Add(infomation);
                    }
                }
            }
            fieldBase.CreateBrock(fieldInfos,(int)tetrisType);
           
        }
   

        public void GenerateItem(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, List<FieldInfo> positions)
        {
            //もらった値からスクリプタブルを持ってきてデータに従って生成

        }

    }
}

