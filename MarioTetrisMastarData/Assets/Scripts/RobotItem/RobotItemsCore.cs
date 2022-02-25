using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Mario;
using System;

namespace RobotItem
{
    public class RobotItemsCore : MonoBehaviour,IItemDataChange
    {
        IAddAtems addItems;
        Dictionary<ItemName, int> ItemBox;

        public event Action<Dictionary<ItemName, int>> ChangeItemBox;


        void Start()
        {
            addItems = Utility.Locator<IAddAtems>.GetT();
            addItems.GetItemEvent += AddItem;
            ItemBox = new Dictionary<ItemName, int>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        void AddItem(ItemName name, int num)
        {
            //�A�C�e�������łɂ����Ă��邩
            if (ItemBox.ContainsKey(name))
            {
                ItemBox[name] += num;
                ChangeItemBox(ItemBox);

            }
            //����������ǉ�
            else
            {
                ItemBox.Add(name, num);
                ChangeItemBox(ItemBox);
            }
        }
        /// <summary>
        /// �A�C�e���̖��O������Ƃ��������Ă邩�Ԃ��B�����ꍇ��-1��Ԃ�
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetItemBox(ItemName name)
        {
            if (!ItemBox.ContainsKey(name))
            {
                return -1;
            }
            else
            {
                return ItemBox[name];
            }
        }
        /// <summary>
        /// �A�C�e���g�p
        /// </summary>
        /// <param name="name"></param>
        /// <param name="num"></param>
        public void UseItem(ItemName name)
        {

        }
        /// <summary>
        /// �A�C�e�������炷
        /// </summary>
        /// <param name="name"></param>
        /// <param name="num"></param>
        public void RemoveItem(ItemName name, int num)
        {
            if (!ItemBox.ContainsKey(name))
            {
                Debug.LogError("�������̂��������Ƃ���");
                return;
            }
            else
            {
                if (ItemBox[name]<num )
                {
                    Debug.LogError("�A�C�e���}�C�i�X�ɂȂ��Ă邯�ǂ�����?�ꉞ�������ǂ��B���񂽂����Ƃ��������v���O�������Ă�Ƃ����K�^�������H");
                }
                else if(ItemBox[name]==num)
                {
                    ItemBox.Remove(name);
                    ChangeItemBox(ItemBox);
                }
                else
                {
                    ItemBox[name] -= num;
                    ChangeItemBox(ItemBox);
                }
            }

        }
    }

}
