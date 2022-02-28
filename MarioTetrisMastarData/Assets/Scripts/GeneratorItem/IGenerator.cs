using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemGenerater
{
    interface IGenerator
    {
        /// <summary>
        /// アイテムを生成する
        /// </summary>
        /// <param name="アイテム名"></param>
        /// <param name="生成位置"></param>
        public void GenerateItem(ItemName name, Vector3 pos);
        /// <summary>
        /// テトリミノを生成
        /// </summary>
        /// <param name="テトリスの形"></param>
        /// <param name="テトリスの角度"></param>
        /// <param name="生成位置"></param>
        public void GenerateItem(Tetris.TetrisTypeEnum tetrisType, Tetris.TetrisAngle tetrisAngle, List<FieldInfo> positions);
    }

}
