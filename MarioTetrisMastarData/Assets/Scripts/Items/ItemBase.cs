using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemBase : MonoBehaviour, IItemHit
    {
        public ItemName GetItemName()
        {
            return name;
        }
        public ItemName name;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
