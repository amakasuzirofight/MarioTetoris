using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Field
{
    [Serializable]
    public class FieldBase : MonoBehaviour
    {
        protected string[] setCharacters;
        protected FieldBase[] nextField;
        public Action fieldcomplete;
        [SerializeField] protected FieldChenger[] fieldChengers;
        protected FieldChenger[] createChengers;
        [HideInInspector] protected GameObject[] chengers_objects;
        [HideInInspector] public FieldChenger activeChenger;

        /// <summary>
        /// 仮想関数　フィールド生成時に呼ばれる関数です
        /// </summary>
        virtual public void OpenField()
        {
            Debug.LogError("仮想関数OpenField は実装されていません");
        }

        /// <summary>
        /// 仮想関数　フィールドがアクティブな間常に呼ばれる関数です
        /// </summary>
        virtual public void FieldCheck()
        {
            Debug.LogError("仮想関数FieldCheck は実装されていません");
        }

        /// <summary>
        /// 仮想関数　フィールドが閉じられるときに呼ばれる関数です
        /// </summary>
        virtual public void CloseField()
        {
            Debug.LogError("仮想関数CloseField は実装されていません");
        }

        /// <summary>
        /// アクセス範囲が限定された関数　設定された数だけchengers_objects,CreateChengersを生成します
        /// </summary>
        protected void CreateCharacters()
        {
            chengers_objects = new GameObject[fieldChengers.Length];
            createChengers = new FieldChenger[fieldChengers.Length];
            for (int i = 0; i < fieldChengers.Length; i++)
            {
                // chengers_objects[i] = Instantiate(fieldChengers[i].gameObject);
                chengers_objects[i] = fieldChengers[i].Create();
                createChengers[i] = chengers_objects[i].GetComponent<FieldChenger>();
            }
        }

        /// <summary>
        /// アクセス範囲が限定された関数　chengers_objectsをすべて削除します
        /// </summary>
        protected void DestroyObjects()
        {
            for (int i = 0; i < chengers_objects.Length; i++)
            {
                Destroy(chengers_objects[i]);
            }
        }

        /// <summary>
        /// アクセス範囲が限定された関数　chengers_objectsと引数で受け取ったオブジェクトをすべて削除します
        /// </summary>
        protected void DestroyObjects(List<GameObject> objects)
        {
            for (int i = 0; i < chengers_objects.Length; i++)
            {
                Destroy(chengers_objects[i]);
            }

            for (int i = 0;i < objects.Count;i++)
            {
                Destroy(objects[i]);
            }
        }

        /// <summary>
        /// アクセス範囲が限定された関数　createChengersのactiveFlgを確認します
        /// </summary>
        protected void ChengerCheck()
        {
            for (int i = 0; i < createChengers.Length; i++)
            {
                if (createChengers[i].activeFlg)
                {
                    activeChenger = createChengers[i];
                    fieldcomplete();
                    break;
                }
            }
        }

        protected List<GameObject> CreateField(List<int[]> csvData)
        {
            List<GameObject> objects = new List<GameObject>();

            for (int i = 0; i < csvData.Count; i++)
            {
                for (int j = 0; j < csvData[i].Length; j++)
                {
                    if (csvData[i][j] != 0)
                    {
                        GameObject obj = Instantiate(Utility_.objectGeter[csvData[i][j]]);
                        obj.transform.position = new Vector3(j, i * -1, 0);
                        objects.Add(obj);
                    }
                }
            }

            return objects;
        }
        virtual public void SceneChenge()
        {
            Debug.LogError("仮想関数SceneChenge は実装されていません");
        }

        virtual public string[] setCharactersGeter
        {
            get
            {
                Debug.LogError("仮想関数(プロパティ)setCharacterGeter は実装されていません");
                return null;
            }
        }

        virtual public FieldBase[] nextFieldGeter
        {
            get
            {
                Debug.LogError("仮想関数(プロパティ)setCharacterGeter は実装されていません");
                return null;
            }
        }
        virtual public void CreateBrock(List<FieldInfo> positions)
        {
            Debug.LogError("設定したフィールドにCreateBrock関数は使用されていません");
        }
    }
}