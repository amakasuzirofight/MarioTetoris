using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tetris;


public class TetrisCreator : EditorWindow
{
    [MenuItem("Window/TetrisCreator")] //Window��\������ꏊ�A�\������閼�O
    static void WindowOpen() //�E�B���h�E��\������ׂ̊֐�
    {
        EditorWindow.GetWindow<TetrisCreator>("TetrisCreator"); //�\������
    }

    static int rangeSize = 4;                                                           //�z��̑傫��
    TetrisScriptableObject[] tetrisDataArray = new TetrisScriptableObject[rangeSize];

    bool[,] selectRange = new bool[rangeSize, rangeSize];                               //���ƂȂ�2�����z��

    bool[,] showRange_0 = new bool[rangeSize, rangeSize];                               //0�x��]��\������2�����z��
    bool[,] showRange_90 = new bool[rangeSize, rangeSize];                              //90�x��]��\������2�����z��
    bool[,] showRange_180 = new bool[rangeSize, rangeSize];                             //180�x��]��\������2�����z��
    bool[,] showRange_270 = new bool[rangeSize, rangeSize];                             //270�x��]��\������2�����z��

    bool[] keepArray_0 = new bool[rangeSize * rangeSize];                               //0�x��]��2�����z���ۑ�����1�����z��
    bool[] keepArray_90 = new bool[rangeSize * rangeSize];                              //90�x��]��2�����z���ۑ�����1�����z��
    bool[] keepArray_180 = new bool[rangeSize * rangeSize];                             //180�x��]��2�����z���ۑ�����1�����z��
    bool[] keepArray_270 = new bool[rangeSize * rangeSize];                             //270�x��]��2�����z���ۑ�����1�����z��

    bool initialize = true;                                                             // �������̂��߂̕ϐ�
    bool noTetriminoFlg;                                                                //�e�g���~�m���ł���Ă��邩�ǂ����̕ϐ�

    const int LOOP_NUM = 4;                                                             //���[�v��
    int fx = rangeSize, fy = rangeSize;                                                 //2�����z��̏c(y)�Ɖ�(x)�̒���

    int fallSpeed;                                                                      //�����X�s�[�h
    string assetName = "";                                                              //�����asset�̖��O
    TetrisTypeEnum tetrisTypeEnum;
    TetrisAngle tetrisAngle;

    enum CreateAssetEnum
    {
        tetrimino_0 = 0,
        tetrimino_90 = 1,
        tetrimino_180 = 2,
        tetrimono_270 = 3
    }

    private void OnGUI()�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@//�����ɏ����������\�������
    {
        //�Ăяo���ꂽ�Ƃ���1�x�����s������
        if (initialize)
        {
            // �X�L���f�[�^��ۑ�����ScriptableObject�̍쐬
            for (int i = 0; i < rangeSize; ++i)
            {
                tetrisDataArray[i] = ScriptableObject.CreateInstance<TetrisScriptableObject>(); //������new���邱��
            }
            initialize = false;
        }
        //���O�����߂�e�L�X�g�{�b�N�X��\��
        assetName = EditorGUILayout.TextField("�f�[�^��", assetName);

        //�����X�s�[�h�����߂�e�L�X�g�{�b�N�X��\��
        fallSpeed = EditorGUILayout.IntField("�����X�s�[�h", fallSpeed);

        //�e�g���~�m�̎�ނ����߂�v���_�E����\��
        tetrisTypeEnum = (TetrisTypeEnum)EditorGUILayout.EnumPopup("�e�g���X�̌`", tetrisTypeEnum);

        //���ۂ̌`�����߂�g�O���{�^����\��
        EditorGUILayout.LabelField("�e�g���X�̌`���쐬(������l�߂ō쐬��)");
        for (int i = 0; i < LOOP_NUM; ++i) //�c,y
        {
            EditorGUILayout.BeginHorizontal(); // �����\�������邽�߁A����\���̐ݒ������

            for (int j = 0; j < LOOP_NUM; ++j) //��,x
            {
                //�{�^����\������Atrue���ǂ������ۑ����Ă���
                selectRange[i, j] = EditorGUILayout.Toggle("", selectRange[i, j], GUI.skin.button, GUILayout.Width(50), GUILayout.Height(50));
            }
            EditorGUILayout.EndHorizontal();
        }

        //�`�����肷��{�^����\��
        if (GUILayout.Button("�e�g���~�m�m��{�^��"))
        {
            //�s�폜for��
            for (int i = fy - 1; 0 <= i; --i) //�c,y
            {
                for (int j = fx - 1; 0 <= j; --j) //��,x
                {
                    //�󔒂��ǂ���
                    noTetriminoFlg = selectRange[i, j];

                    //�󔒂ł͂Ȃ��̂Ȃ甲���o��
                    if (noTetriminoFlg)
                    {
                        break;
                    }
                }

                //�󔒂ł͂Ȃ��̂Ȃ�
                if (!noTetriminoFlg)
                {
                    //�s�����炷
                    fy--;
                }
                else break;
            }

            //��폜for��
            for (int j = fx - 1; 0 <= j; --j) //��,x
            {
                for (int i = fy - 1; 0 <= i; --i) //�c,y
                {
                    noTetriminoFlg = selectRange[i, j];

                    if (noTetriminoFlg)
                    {
                        break;
                    }
                }

                if (!noTetriminoFlg)
                {
                    //������炷
                    fx--;
                }
                else break;
            }

            //selectRange��\��
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_0[i, j] = selectRange[i, j];
                }
            }
            //selectRange��90�x��]�����\��
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_90[j, fy - 1 - i] = selectRange[i, j];
                }
            }

            //selectRange��180�x��]�����\��
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_180[fy - 1 - i, fx - 1 - j] = selectRange[i, j];
                }
            }
            //selectRange��270�x��]�����\��
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_270[fx - 1 - j, i] = selectRange[i, j];
                }
            }
        }

        //��]�����z���\������g�O���{�^����\��
        EditorGUILayout.LabelField("�쐬�����e�g���X�̌`��\��(���蒼���ȊO�ŐG�邱�Ɣ񐄏���)");
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("0�x��]");
                for (int i = 0; i < LOOP_NUM; ++i) //�c,y
                {
                    EditorGUILayout.BeginHorizontal(); // �����\�������邽�߁A����\���̐ݒ������
                    for (int j = 0; j < LOOP_NUM; ++j) //��,x
                    {
                        //�{�^����\������Atrue���ǂ������ۑ����Ă���
                        showRange_0[i, j] = EditorGUILayout.Toggle("", showRange_0[i, j], GUI.skin.button, GUILayout.Width(15), GUILayout.Height(15));
                        //2�����z���1�����z��ɕύX���ĉ��ۑ�
                        keepArray_0[i * rangeSize + j] = showRange_0[i, j];
                    }
                    EditorGUILayout.EndHorizontal();
                }
                //�ۑ�
                tetrisDataArray[(int)CreateAssetEnum.tetrimino_0].tetriminoArray = keepArray_0;

                tetrisDataArray[(int)CreateAssetEnum.tetrimino_0].tetrisAngle = (int)CreateAssetEnum.tetrimino_0;
                
            }
            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("90�x��]");
                for (int i = 0; i < LOOP_NUM; ++i) //�c,y
                {
                    EditorGUILayout.BeginHorizontal(); // �����\�������邽�߁A����\���̐ݒ������

                    for (int j = 0; j < LOOP_NUM; ++j) //��,x
                    {
                        //�{�^����\������Atrue���ǂ������ۑ����Ă���
                        showRange_90[i, j] = EditorGUILayout.Toggle("", showRange_90[i, j], GUI.skin.button, GUILayout.Width(15), GUILayout.Height(15));
                        keepArray_90[i * rangeSize + j] = showRange_90[i, j];
                    }
                    EditorGUILayout.EndHorizontal();
                }
                tetrisDataArray[(int)CreateAssetEnum.tetrimino_90].tetriminoArray = keepArray_90;
                tetrisDataArray[(int)CreateAssetEnum.tetrimino_90].tetrisAngle = (TetrisAngle)(int)CreateAssetEnum.tetrimino_90;
            }

            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("180�x��]");
                for (int i = 0; i < LOOP_NUM; ++i) //�c,y
                {
                    EditorGUILayout.BeginHorizontal(); // �����\�������邽�߁A����\���̐ݒ������

                    for (int j = 0; j < LOOP_NUM; ++j) //��,x
                    {
                        //�{�^����\������Atrue���ǂ������ۑ����Ă���
                        showRange_180[i, j] = EditorGUILayout.Toggle("", showRange_180[i, j], GUI.skin.button, GUILayout.Width(15), GUILayout.Height(15));
                        keepArray_180[i * rangeSize + j] = showRange_180[i, j];
                    }
                    EditorGUILayout.EndHorizontal();
                }
                tetrisDataArray[(int)CreateAssetEnum.tetrimino_180].tetriminoArray = keepArray_180;
                tetrisDataArray[(int)CreateAssetEnum.tetrimino_180].tetrisAngle = (TetrisAngle)(int)CreateAssetEnum.tetrimino_180;
            }

            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("270�x��]");
                for (int i = 0; i < LOOP_NUM; ++i) //�c,y
                {
                    EditorGUILayout.BeginHorizontal(); // �����\�������邽�߁A����\���̐ݒ������

                    for (int j = 0; j < LOOP_NUM; ++j) //��,x
                    {
                        //�{�^����\������Atrue���ǂ������ۑ����Ă���
                        showRange_270[i, j] = EditorGUILayout.Toggle("", showRange_270[i, j], GUI.skin.button, GUILayout.Width(15), GUILayout.Height(15));
                        keepArray_270[i * rangeSize + j] = showRange_270[i, j];
                    }
                    EditorGUILayout.EndHorizontal();
                }
                tetrisDataArray[(int)CreateAssetEnum.tetrimono_270].tetriminoArray = keepArray_270;
                tetrisDataArray[(int)CreateAssetEnum.tetrimono_270].tetrisAngle = (TetrisAngle)(int)CreateAssetEnum.tetrimono_270;
            }
        }

        //�ۑ�����{�^��(���͘R��m�F)
        if (GUILayout.Button("�f�[�^�ۑ�"))
        {
            // �f�[�^�������͂���Ă��Ȃ��ꍇ
            if (assetName == "")
            {
                EditorUtility.DisplayDialog("Error!", string.Format("�f�[�^�������͂���Ă��܂���B"), "OK");
                return;
            }

            if (fallSpeed == 0)
            {
                EditorUtility.DisplayDialog("Error!", string.Format("�����X�s�[�h�����͂���Ă��܂���B"), "OK");
                return;
            }
            // �e�g���X�̌`��1�}�X�����݂��Ȃ��ꍇ
            bool rangeError = false;
            foreach (bool flg in selectRange)
            {
                rangeError = flg;
                if (rangeError) break;
            }
            if (!rangeError)
            {
                EditorUtility.DisplayDialog("Error!", string.Format("�e�g���X�̌`���ݒ肳��Ă��܂���B"), "OK");
                return;
            }

            // �ۑ��m�F
            if (!EditorUtility.DisplayDialog("�e�g���X�f�[�^�ۑ��m�F", string.Format("�e�g���X�f�[�^��ۑ����܂����H"), "OK", "CANCEL")) return;

            CreateSkillScriptableObject();
        }

        if (GUILayout.Button("���Z�b�g"))
        {
            if (EditorUtility.DisplayDialog("���Z�b�g�m�F", string.Format("���͂����f�[�^�����Z�b�g���܂����H"), "OK", "cancel")) Reset();
        }
    }
    //�f�[�^�ۑ��֐�
    public void CreateSkillScriptableObject()
    {

        for (int i = 0; i < LOOP_NUM; i++)
        {
            tetrisDataArray[i].ActivateScriptableObject(assetName, fallSpeed, tetrisTypeEnum);

            const string PATH = "Assets/ScriptableObjects/Tetrimino/";
            string path = PATH + tetrisDataArray[i].assetName + i + ".asset";

            AssetDatabase.CreateAsset(tetrisDataArray[i], path);
            EditorUtility.SetDirty(tetrisDataArray[i]);
            AssetDatabase.SaveAssets();
        }
        Reset();
    }

    private void Reset()
    {
        fx = rangeSize;
        fy = rangeSize;
        assetName = "";
        fallSpeed = 0;
        for (int i = 0; i < rangeSize; ++i)
        {
            for (int j = 0; j < rangeSize; ++j)
            {
                selectRange[i, j] = false;
                showRange_0[i, j] = false;
                showRange_90[i, j] = false;
                showRange_180[i, j] = false;
                showRange_270[i, j] = false;
            }
        }

        for(int i = 0;i < rangeSize * rangeSize;i++)
        {
            keepArray_0[i] = false;
            keepArray_90[i] = false;
            keepArray_180[i] = false;
            keepArray_270[i] = false;
        }
    }
}

