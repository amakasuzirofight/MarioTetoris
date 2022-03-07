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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GenerateItem(TetrisTypeEnum.Type_L, TetrisAngle.Angle_270,fieldKari);
            }



        }
        public void GenerateItem(ItemName name, Vector3 pos)
        {
            Debug.LogError("�A�C�e���o�����ǂ܂������ĂȂ�");
        }

        public void GenerateItem(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, FieldInfo info)
        {
            fieldBase = Utility.Locator<Field.FieldBase>.GetT();
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            TetrisScriptableObject tetrisScriptable = getTetrisInfo.GetTetrimino(tetrisType, tetrisAngle);
            for (int i = 3; i  >= 0; i--/*int i = 0; i < 4; i++*/)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tetrisScriptable.tetriminoArrays[i, j])
                    {
                        //�f�[�^��ǂ�Ŕz�����ѕς���
                        FieldInfo infomation;
                        infomation.width = j + info.width;
                        infomation.height = i + info.height;

                        fieldInfos.Add(infomation);
                    }
                }
            }

            //Debug.Log(fieldBase == null);

            //for (int i = 0; i < fieldInfos.Count; i++)
            //{
            //    Debug.Log($"infos[{i}] height = {fieldInfos[i].height} width = {fieldInfos[i].width}");
            //}

            fieldBase.CreateBrock(fieldInfos);
            InstanceTetris();
        }
        void InstanceTetris()
        {
            Debug.Log("�����I�I");
        }

        public void GenerateItem(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, List<FieldInfo> positions)
        {
            //��������l����X�N���v�^�u���������Ă��ăf�[�^�ɏ]���Đ���

        }
    }
}

