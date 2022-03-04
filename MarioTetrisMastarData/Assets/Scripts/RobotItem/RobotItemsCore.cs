using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mario;
using System;

namespace RobotItem
{
    public class RobotItemsCore : MonoBehaviour, IItemDataChange, IGetItemBox, IAddTetrisPiece, IAddItemPiece, IRemoveItems
    {
        Dictionary<ItemName, int> ItemBox
        {
            get
            {
                return ItemBox2D;
            }
            set
            {
                ItemBox2D = value;
                ChangeItemBoxValue(ItemBox2D);
            }
        }
        Dictionary<ItemName, int> ItemBox2D = new Dictionary<ItemName, int>();
        int tetrisPiecenum;
        int tetrisPiece
        {
            get
            {
                return tetrisPiecenum;
            }
            set
            {
                tetrisPiecenum = value;
                Debug.Log(ChangeTetPieceValue == null);
                ChangeTetPieceValue(tetrisPiecenum);
            }
        }

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

            //初期化
            for (int i = 0; i < (int)ItemName.Count; i++)
            {
                ItemBox.Add((ItemName)i, 0);
            }
            //初期値入れる
            ItemBox[ItemName.Stone] += 5;

            //デバッグ用最初にテトリス入れる
            tetrisPiecenum += 59;
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
            //ChangeTetPieceValue(tetrisPiece);
        }

        public void AddItemPiece(ItemName name, int num)
        {
            //すでにあるかどうか確認
            if (ItemBox.ContainsKey(name))
            {
                ItemBox[name] += num;
            }
            //無い場合新しく登録
            else
            {
                ItemBox.Add(name, num);
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
            //ChangeTetPieceValue(tetrisPiece);
        }
    }

}
