using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class ItemDataBase : MonoBehaviour,IGetTetrisInfo
    {
        [SerializeField] List<TetrisScriptableObject> typeTList;
        [SerializeField] List<TetrisScriptableObject> typeIList;
        [SerializeField] List<TetrisScriptableObject> typeLList;
        [SerializeField] List<TetrisScriptableObject> typeJList;
        [SerializeField] List<TetrisScriptableObject> typeZList;
        [SerializeField] List<TetrisScriptableObject> typeSList;
        [SerializeField] List<TetrisScriptableObject> typeOList;
        TetrisTypeEnum typeEnum;
        TetrisAngle angleEnum;

        List<List<TetrisScriptableObject>> tetriminoAllList = new List<List<TetrisScriptableObject>>();

        Dictionary<TetrisTypeEnum, Dictionary<TetrisAngle, List<TetrisScriptableObject>>> tetriminoTypeDic = new Dictionary<TetrisTypeEnum, Dictionary<TetrisAngle, List<TetrisScriptableObject>>>();
        Dictionary<TetrisAngle, List<TetrisScriptableObject>> tetriminoAngleDic = new Dictionary<TetrisAngle, List<TetrisScriptableObject>> ();

        public TetrisScriptableObject GetTetrimino(TetrisTypeEnum type, TetrisAngle angle)
        {
            switch (type)
            {
                case TetrisTypeEnum.Type_T:
                    return typeTList[(int)angle];

                case TetrisTypeEnum.Type_I:
                    return typeIList[(int)angle];

                case TetrisTypeEnum.Type_L:
                    return typeLList[(int)angle];

                case TetrisTypeEnum.Type_J:
                    return typeJList[(int)angle];

                case TetrisTypeEnum.Type_Z:
                    return typeZList[(int)angle];

                case TetrisTypeEnum.Type_S:
                    return typeSList[(int)angle];

                case TetrisTypeEnum.Type_O:
                    return typeOList[(int)angle];

                default:
                    Debug.LogError("そんなテトリミノ普通に考えてないやろ一回寝た方がいいよ");
                    return default;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //tetriminoAllList.Insert(0, typeTList);
            //tetriminoAllList.Insert(1, typeIList);
            //tetriminoAllList.Insert(2, typeLList);
            //tetriminoAllList.Insert(3, typeJList);
            //tetriminoAllList.Insert(4, typeZList);
            //tetriminoAllList.Insert(5, typeSList);
            //tetriminoAllList.Insert(6, typeOList);

            //for (int i = 0; i < tetriminoAllList.Count; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Debug.Log("i = " + i + "  j = " + j );
                    typeEnum = TetrisTypeEnum.Type_L;
                    angleEnum = TetrisAngle.Angle_180;
            //        //tetriminoAngleDic[(TetrisAngle)j].Add(tetriminoAllList[i][j]);
            //        tetriminoAngleDic.Add((TetrisAngle)j, tetriminoAllList[i]);
            //    }
            //    tetriminoTypeDic.Add((TetrisTypeEnum)i, tetriminoAngleDic);
            //}
            GetTetrimino(typeEnum,angleEnum);
        }        
    }
}