using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class ItemDataBase : MonoBehaviour, IGetTetrisInfo
    {
        [SerializeField] List<TetrisScriptableObject> typeIList;
        [SerializeField] List<TetrisScriptableObject> typeJList;
        [SerializeField] List<TetrisScriptableObject> typeLList;
        [SerializeField] List<TetrisScriptableObject> typeOList;
        [SerializeField] List<TetrisScriptableObject> typeSList;
        [SerializeField] List<TetrisScriptableObject> typeTList;
        [SerializeField] List<TetrisScriptableObject> typeZList;

        //List<List<TetrisScriptableObject>> tetriminoAllList = new List<List<TetrisScriptableObject>>();

        //Dictionary<TetrisTypeEnum, Dictionary<TetrisAngle, List<TetrisScriptableObject>>> tetriminoTypeDic = new Dictionary<TetrisTypeEnum, Dictionary<TetrisAngle, List<TetrisScriptableObject>>>();
        //Dictionary<TetrisAngle, List<TetrisScriptableObject>> tetriminoAngleDic = new Dictionary<TetrisAngle, List<TetrisScriptableObject>> ();

        private void Awake()
        {
            Utility.Locator<IGetTetrisInfo>.Bind(this);
        }
        public TetrisScriptableObject GetTetrimino(TetrisTypeEnum type, TetrisAngle angle)
        {
            switch (type)
            {
                case TetrisTypeEnum.Type_I:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            typeIList[(int)angle].tetriminoArrays[i, j] = typeIList[(int)angle].tetriminoArray[i * 4 + j];
                        }

                    }
                    return typeIList[(int)angle];

                case TetrisTypeEnum.Type_J:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            typeJList[(int)angle].tetriminoArrays[i, j] = typeJList[(int)angle].tetriminoArray[i * 4 + j];
                        }

                    }
                    return typeJList[(int)angle];

                case TetrisTypeEnum.Type_L:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            typeLList[(int)angle].tetriminoArrays[i, j] = typeLList[(int)angle].tetriminoArray[i * 4 + j];
                        }

                    }
                    return typeLList[(int)angle];

                case TetrisTypeEnum.Type_O:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            typeOList[(int)angle].tetriminoArrays[i, j] = typeOList[(int)angle].tetriminoArray[i * 4 + j];
                        }

                    }
                    return typeOList[(int)angle];

                case TetrisTypeEnum.Type_S:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            typeSList[(int)angle].tetriminoArrays[i, j] = typeSList[(int)angle].tetriminoArray[i * 4 + j];
                        }

                    }
                    return typeSList[(int)angle];

                case TetrisTypeEnum.Type_T:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            typeTList[(int)angle].tetriminoArrays[i, j] = typeTList[(int)angle].tetriminoArray[i * 4 + j];
                        }

                    }
                    return typeTList[(int)angle];

                case TetrisTypeEnum.Type_Z:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            typeZList[(int)angle].tetriminoArrays[i, j] = typeZList[(int)angle].tetriminoArray[i * 4 + j];
                        }

                    }
                    return typeZList[(int)angle];

                default:
                    Debug.LogError("‚»‚ñ‚ÈƒeƒgƒŠƒ~ƒm•’Ê‚Él‚¦‚Ä‚È‚¢‚â‚ëˆê‰ñQ‚½•û‚ª‚¢‚¢‚æ");
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
            //        tetriminoAngleDic[(TetrisAngle)j].Add(tetriminoAllList[i][j]);
            //        tetriminoAngleDic.Add((TetrisAngle)j, tetriminoAllList[i]);
            //    }
            //    tetriminoTypeDic.Add((TetrisTypeEnum)i, tetriminoAngleDic);
            //}
            //GetTetrimino(typeEnum,angleEnum);
        }
    }
}