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
            //アイテムをすでにもっているか
            if (ItemBox.ContainsKey(name))
            {
                ItemBox[name] += num;
                ChangeItemBoxValue(ItemBox);

            }
            //無かったら追加
            else
            {
                ItemBox.Add(name, num);
                ChangeItemBoxValue(ItemBox);
            }
        }
        /// <summary>
        /// アイテムの名前を入れるといくつ持ってるか返す。無い場合は-1を返す
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
            //すでにあるかどうか確認
            if (ItemBox.ContainsKey(name))
            {
                ItemBox[name] += num;
                ChangeItemBoxValue(ItemBox);
            }
            //無い場合新しく登録
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
                Debug.LogError("無いものを消そうとした");
                return;
            }
            else
            {
                if (ItemBox[name] < num)
                {
                    Debug.LogError("アイテムマイナスになってるけどいいの?一応消すけどさ。あんたずっとこういうプログラムしてるといつかガタが来るよ？");
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
