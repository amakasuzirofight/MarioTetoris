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
        /// ���z�֐��@�t�B�[���h�������ɌĂ΂��֐��ł�
        /// </summary>
        virtual public void OpenField()
        {
            Debug.LogError("���z�֐�OpenField �͎�������Ă��܂���");
        }

        /// <summary>
        /// ���z�֐��@�t�B�[���h���A�N�e�B�u�Ȋԏ�ɌĂ΂��֐��ł�
        /// </summary>
        virtual public void FieldCheck()
        {
            Debug.LogError("���z�֐�FieldCheck �͎�������Ă��܂���");
        }

        /// <summary>
        /// ���z�֐��@�t�B�[���h��������Ƃ��ɌĂ΂��֐��ł�
        /// </summary>
        virtual public void CloseField()
        {
            Debug.LogError("���z�֐�CloseField �͎�������Ă��܂���");
        }

        /// <summary>
        /// �A�N�Z�X�͈͂����肳�ꂽ�֐��@�ݒ肳�ꂽ������chengers_objects,CreateChengers�𐶐����܂�
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
        /// �A�N�Z�X�͈͂����肳�ꂽ�֐��@chengers_objects�����ׂč폜���܂�
        /// </summary>
        protected void DestroyObjects()
        {
            for (int i = 0; i < chengers_objects.Length; i++)
            {
                Destroy(chengers_objects[i]);
            }
        }

        /// <summary>
        /// �A�N�Z�X�͈͂����肳�ꂽ�֐��@chengers_objects�ƈ����Ŏ󂯎�����I�u�W�F�N�g�����ׂč폜���܂�
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
        /// �A�N�Z�X�͈͂����肳�ꂽ�֐��@createChengers��activeFlg���m�F���܂�
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
            Debug.LogError("���z�֐�SceneChenge �͎�������Ă��܂���");
        }

        virtual public string[] setCharactersGeter
        {
            get
            {
                Debug.LogError("���z�֐�(�v���p�e�B)setCharacterGeter �͎�������Ă��܂���");
                return null;
            }
        }

        virtual public FieldBase[] nextFieldGeter
        {
            get
            {
                Debug.LogError("���z�֐�(�v���p�e�B)setCharacterGeter �͎�������Ă��܂���");
                return null;
            }
        }
        virtual public void CreateBrock(List<FieldInfo> positions)
        {
            Debug.LogError("�ݒ肵���t�B�[���h��CreateBrock�֐��͎g�p����Ă��܂���");
        }
    }
}