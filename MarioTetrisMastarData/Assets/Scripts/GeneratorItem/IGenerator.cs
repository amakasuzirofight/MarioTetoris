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
        public void GenerateItem(Tetris.TetrisTypeEnum tetrisType, Tetris.TetrisAngle tetrisAngle, FieldInfo positions);
        /// <summary>
        /// ��������ʒu�ɂ��łɃu���b�N���Ȃ����m�F
        /// </summary>
        /// <param name="tetrisType"></param>
        /// <param name="tetrisAngle"></param>
        /// <param name="positions"></param>
        /// <returns></returns>
        public bool CanGenerateTetris(Tetris.TetrisTypeEnum tetrisType, Tetris.TetrisAngle tetrisAngle, FieldInfo positions);
    }

}
