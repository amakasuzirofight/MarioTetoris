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
            //アイテムをすでにもっているか
            if (ItemBox.ContainsKey(name))
            {
                ItemBox[name] += num;
                ChangeItemBox(ItemBox);

            }
            //無かったら追加
            else
            {
                ItemBox.Add(name, num);
                ChangeItemBox(ItemBox);
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
        /// <summary>
        /// アイテム使用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="num"></param>
        public void UseItem(ItemName name)
        {

        }
        /// <summary>
        /// アイテムを減らす
        /// </summary>
        /// <param name="name"></param>
        /// <param name="num"></param>
        public void RemoveItem(ItemName name, int num)
        {
            if (!ItemBox.ContainsKey(name))
            {
                Debug.LogError("無いものを消そうとした");
                return;
            }
            else
            {
                if (ItemBox[name]<num )
                {
                    Debug.LogError("アイテムマイナスになってるけどいいの?一応消すけどさ。あんたずっとこういうプログラムしてるといつかガタが来るよ？");
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
