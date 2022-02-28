using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemBase : MonoBehaviour
    {
        /// <summary>
        /// �v���C���[�ɓ����������ɖ��O��Ԃ�
        /// </summary>
        /// <returns></returns>
        
        protected float fallSpeed;
        [SerializeField] protected Sprite itemSprite;

        protected enum ItemNameEnum
        {
            NormalPortion,
            HighPortion,
            GreatPortion,
            Stone,
            Bom
        }

        virtual public void FallDown()
        {

        }

        virtual public void Hit()
        {

        }
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
