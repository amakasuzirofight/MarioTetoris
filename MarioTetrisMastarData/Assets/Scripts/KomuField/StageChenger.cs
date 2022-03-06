using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class StageChenger : FieldChenger
    {
        private bool clearFlg_Stage = false;

        public Way[] ways;

        public override GameObject Create()
        {
            clearFlg = false;
            clearFlg_Stage = false;
            if (Utility_.stageFlgList[nextField.stageNumber]) clearFlg_Stage = true;
            if (clearFlg_Stage)
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