using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class StageChenger : FieldChenger
    {
        public bool clearFlg = false;

        public Way[] ways;

        public override GameObject Create()
        {
            if (clearFlg)
            {
                for (int i = 0; i < ways.Length; i++)
                {
                    ways[i].Create();
                }
            }

            return Instantiate(gameObject);
        }
    }
}