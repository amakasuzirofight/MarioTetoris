using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Tetris;
using Robot;

namespace ItemGenerater
{
    public class ItemGenarate : MonoBehaviour,IGenerator
    {
        [SerializeField] GameObject ItemBase;
        Field.FieldBase fieldBase;
        IGetPositionToInfo getPositionToInfo;
       
        private void Awake()
        {
            Utility.Locator<IGenerator>.Bind(this);
        }
        void Start()
        {
            fieldBase = Utility.Locator<Field.FieldBase>.GetT();
            getPositionToInfo = Utility.Locator<IGetPositionToInfo>.GetT();
        }

        void Update()
        {

        }
        public void GenerateItem(ItemName name, Vector3 pos)
        {

        }

        public void GenerateItem(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, Vector3 pos)
        {
            var temp = getPositionToInfo.GetPositionToList();

            //fieldBase.CreateBrock();
            InstanceTetris();
            throw new System.NotImplementedException();
        }
        void InstanceTetris()
        {
            Debug.Log("生成！！");
        }

        public void GenerateItem(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, List<FieldInfo> positions)
        {
            //もらった値からスクリプタブルを持ってきてデータに従って生成

        }
    }
}

