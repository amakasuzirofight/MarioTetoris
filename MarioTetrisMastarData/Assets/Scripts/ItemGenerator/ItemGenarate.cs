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
            //��������l����X�N���v�^�u���������Ă��ăf�[�^�ɏ]���Đ���
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
            Debug.Log("�����I�I");
        }
    }
}

