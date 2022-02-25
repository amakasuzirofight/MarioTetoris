using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemGenerater
{
    interface IGenerator
    {
        public void GenerateItem(ItemName name, Vector3 pos);
        public void GenerateItem(Tetris.TetrisTypeEnum tetrisType, Tetris.TetrisAngle tetrisAngle, Vector3 pos);
    }

}
