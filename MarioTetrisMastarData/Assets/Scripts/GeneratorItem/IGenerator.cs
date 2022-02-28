using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemGenerater
{
    interface IGenerator
    {
        /// <summary>
        /// �A�C�e���𐶐�����
        /// </summary>
        /// <param name="�A�C�e����"></param>
        /// <param name="�����ʒu"></param>
        public void GenerateItem(ItemName name, Vector3 pos);
        /// <summary>
        /// �e�g���~�m�𐶐�
        /// </summary>
        /// <param name="�e�g���X�̌`"></param>
        /// <param name="�e�g���X�̊p�x"></param>
        /// <param name="�����ʒu"></param>
        public void GenerateItem(Tetris.TetrisTypeEnum tetrisType, Tetris.TetrisAngle tetrisAngle, List<FieldInfo> positions);
    }

}
