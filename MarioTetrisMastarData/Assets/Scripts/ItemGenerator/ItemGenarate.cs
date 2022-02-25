using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Tetris;

namespace ItemGenerater
{
    public class ItemGenarate : MonoBehaviour,IGenerator
    {
        [SerializeField] GameObject ItemBase;
        public void GenerateItem(ItemName name, Vector3 pos)
        {
            
        }

        public void GenerateItem(TetrisTypeEnum tetrisType, TetrisAngle tetrisAngle, Vector3 pos)
        {
            //もらった値からスクリプタブルを持ってきてデータに従って生成
            InstanceTetris();
            throw new System.NotImplementedException();
        }

        void Start()
        {

        }

        void Update()
        {

        }
        void InstanceTetris()
        {
            Debug.Log("生成！！");
        }
    }
}

