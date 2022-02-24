using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mario;
namespace RobotItem
{
    public class RobotItemsCore : MonoBehaviour
    {
        IaddAtems addItems;
        void Start()
        {
            addItems = Utility.Locator<IaddAtems>.GetT();
            addItems.GetItemEvent += AddItem;
        }
        
        // Update is called once per frame
        void Update()
        {

        }
        void AddItem(ItemName name)
        {

        }
    }

}
