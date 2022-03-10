using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public enum ItemNameEnum
    {
        NormalPortion,
        Stone,
        Bom
    }
    public class ItemBase : MonoBehaviour
    {
        /// <summary>
        /// �v���C���[�ɓ����������ɖ��O��Ԃ�
        /// </summary>
        /// <returns></returns>
        
        protected float fallSpeed;
        [SerializeField] protected Sprite itemSprite;
        [SerializeField] protected ItemNameEnum itemNameEnum;

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
