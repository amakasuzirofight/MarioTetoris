using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputer;
namespace Select
{
    public class SelectController : MonoBehaviour,ISelectedItem
    {
        [SerializeField] GameObject InputObj;

        ISelectInput selectInput;

        SelectState selectState;
        // Start is called before the first frame update
        void Start()
        {
            selectInput = InputObj.GetComponent<ISelectInput>();
        }
        void Update()
        {
            switch (selectState)
            {
                case SelectState.FirstSelect:
                    break;
                case SelectState.Spin:
                    break;
                default:
                    break;
            }
        }
        public ItemName ISelectedItem()
        {
            throw new System.NotImplementedException();
        }
    void SelectItemCursle()
        {

        }
        /// <summary>
        /// 選択用ステータス
        /// </summary>
        private enum SelectState
        {
            FirstSelect,Spin
        }
    }
}

