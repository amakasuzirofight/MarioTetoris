using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mario;
using System;

namespace RobotItem
{
    public class RobotItemsCore : MonoBehaviour, IItemDataChange, IGetItemBox, IAddTetrisPiece, IAddItemPiece, IRemoveItems
    {
        Dictionary<ItemName, int> ItemBox;
        int tetrisPiece;
        public event Action<Dictionary<ItemName, int>> ChangeItemBoxValue;
        public event Action<int> ChangeTetPieceValue;


        private void Awake()
        {
            Utility.Locator<IAddTetrisPiece>.Bind(this);
            Utility.Locator<IAddItemPiece>.Bind(this);
            Utility.Locator<IGetItemBox>.Bind(this);
            Utility.Locator<IItemDataChange>.Bind(this);
            Utility.Locator<IRemoveItems>.Bind(this);
        }
        void Start()
        {
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
                ChangeItemBoxValue(ItemBox);

            }
            //����������ǉ�
            else
            {
                ItemBox.Add(name, num);
                ChangeItemBoxValue(ItemBox);
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


        public Dictionary<ItemName, int> GetItemBox()
        {
            return ItemBox;
        }

        public int GetTetris()
        {
            return tetrisPiece;
        }

        public void AddTetris(int value)
        {
            tetrisPiece += value;
            ChangeTetPieceValue(tetrisPiece);
        }

        public void AddItemPiece(ItemName name, int num)
        {
            //���łɂ��邩�ǂ����m�F
            if (ItemBox.ContainsKey(name))
            {
                ItemBox[name] += num;
                ChangeItemBoxValue(ItemBox);
            }
            //�����ꍇ�V�����o�^
            else
            {
                ItemBox.Add(name, num);
                ChangeItemBoxValue(ItemBox);
            }
        }

        public void RemoveItems(ItemName name, int num)
        {
            if (!ItemBox.ContainsKey(name))
            {
                Debug.LogError("�������̂��������Ƃ���");
                return;
            }
            else
            {
                if (ItemBox[name] < num)
                {
                    Debug.LogError("�A�C�e���}�C�i�X�ɂȂ��Ă邯�ǂ�����?�ꉞ�������ǂ��B���񂽂����Ƃ��������v���O�������Ă�Ƃ����K�^�������H");
                }
                else if (ItemBox[name] == num)
                {
                    ItemBox.Remove(name);
                    ChangeItemBoxValue(ItemBox);
                }
                else
                {
                    ItemBox[name] -= num;
                    ChangeItemBoxValue(ItemBox);
                }
            }
        }

        public void RemoveTetrisPiece(int num)
        {
            tetrisPiece -= num;
            ChangeTetPieceValue(tetrisPiece);
        }
    }

}
