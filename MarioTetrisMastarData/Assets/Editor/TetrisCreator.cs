using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tetris;


public class TetrisCreator : EditorWindow
{
    [MenuItem("Window/TetrisCreator")] //Windowを表示する場所、表示される名前
    static void WindowOpen() //ウィンドウを表示する為の関数
    {
        EditorWindow.GetWindow<TetrisCreator>("TetrisCreator"); //表示処理
    }

    static int rangeSize = 4;                                                           //配列の大きさ
    TetrisScriptableObject[] tetrisDataArray = new TetrisScriptableObject[rangeSize];

    bool[,] selectRange = new bool[rangeSize, rangeSize];                               //元となる2次元配列

    bool[,] showRange_0 = new bool[rangeSize, rangeSize];                               //0度回転を表示する2次元配列
    bool[,] showRange_90 = new bool[rangeSize, rangeSize];                              //90度回転を表示する2次元配列
    bool[,] showRange_180 = new bool[rangeSize, rangeSize];                             //180度回転を表示する2次元配列
    bool[,] showRange_270 = new bool[rangeSize, rangeSize];                             //270度回転を表示する2次元配列

    bool[] keepArray_0 = new bool[rangeSize * rangeSize];                               //0度回転の2次元配列を保存する1次元配列
    bool[] keepArray_90 = new bool[rangeSize * rangeSize];                              //90度回転の2次元配列を保存する1次元配列
    bool[] keepArray_180 = new bool[rangeSize * rangeSize];                             //180度回転の2次元配列を保存する1次元配列
    bool[] keepArray_270 = new bool[rangeSize * rangeSize];                             //270度回転の2次元配列を保存する1次元配列

    bool initialize = true;                                                             // 初期化のための変数
    bool noTetriminoFlg;                                                                //テトリミノが打たれているかどうかの変数

    const int LOOP_NUM = 4;                                                             //ループ回数
    int fx = rangeSize, fy = rangeSize;                                                 //2次元配列の縦(y)と横(x)の長さ

    int fallSpeed;                                                                      //落下スピード
    string assetName = "";                                                              //作られるassetの名前
    TetrisTypeEnum tetrisTypeEnum;
    TetrisAngle tetrisAngle;

    enum CreateAssetEnum
    {
        tetrimino_0 = 0,
        tetrimino_90 = 1,
        tetrimino_180 = 2,
        tetrimono_270 = 3
    }

    private void OnGUI()　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　//ここに書く処理が表示される
    {
        //呼び出されたときに1度だけ行う処理
        if (initialize)
        {
            // スキルデータを保存するScriptableObjectの作成
            for (int i = 0; i < rangeSize; ++i)
            {
                tetrisDataArray[i] = ScriptableObject.CreateInstance<TetrisScriptableObject>(); //いわゆるnewすること
            }
            initialize = false;
        }
        //名前を決めるテキストボックスを表示
        assetName = EditorGUILayout.TextField("データ名", assetName);

        //落下スピードを決めるテキストボックスを表示
        fallSpeed = EditorGUILayout.IntField("落下スピード", fallSpeed);

        //テトリミノの種類を決めるプルダウンを表示
        tetrisTypeEnum = (TetrisTypeEnum)EditorGUILayout.EnumPopup("テトリスの形", tetrisTypeEnum);

        //実際の形を決めるトグルボタンを表示
        EditorGUILayout.LabelField("テトリスの形を作成(※左上詰めで作成※)");
        for (int i = 0; i < LOOP_NUM; ++i) //縦,y
        {
            EditorGUILayout.BeginHorizontal(); // 横一列表示をするため、並列表示の設定をする

            for (int j = 0; j < LOOP_NUM; ++j) //横,x
            {
                //ボタンを表示する、trueかどうかも保存している
                selectRange[i, j] = EditorGUILayout.Toggle("", selectRange[i, j], GUI.skin.button, GUILayout.Width(50), GUILayout.Height(50));
            }
            EditorGUILayout.EndHorizontal();
        }

        //形を決定するボタンを表示
        if (GUILayout.Button("テトリミノ確定ボタン"))
        {
            //行削除for文
            for (int i = fy - 1; 0 <= i; --i) //縦,y
            {
                for (int j = fx - 1; 0 <= j; --j) //横,x
                {
                    //空白かどうか
                    noTetriminoFlg = selectRange[i, j];

                    //空白ではないのなら抜け出す
                    if (noTetriminoFlg)
                    {
                        break;
                    }
                }

                //空白ではないのなら
                if (!noTetriminoFlg)
                {
                    //行を減らす
                    fy--;
                }
                else break;
            }

            //列削除for文
            for (int j = fx - 1; 0 <= j; --j) //横,x
            {
                for (int i = fy - 1; 0 <= i; --i) //縦,y
                {
                    noTetriminoFlg = selectRange[i, j];

                    if (noTetriminoFlg)
                    {
                        break;
                    }
                }

                if (!noTetriminoFlg)
                {
                    //列を減らす
                    fx--;
                }
                else break;
            }

            //selectRangeを表示
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_0[i, j] = selectRange[i, j];
                }
            }
            //selectRangeを90度回転させ表示
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_90[j, fy - 1 - i] = selectRange[i, j];
                }
            }

            //selectRangeを180度回転させ表示
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_180[fy - 1 - i, fx - 1 - j] = selectRange[i, j];
                }
            }
            //selectRangeを270度回転させ表示
            for (int i = 0; i < fy; ++i)
            {
                for (int j = 0; j < fx; ++j)
                {
                    showRange_270[fx - 1 - j, i] = selectRange[i, j];
                }
            }
        }

        //回転した配列を表示するトグルボタンを表示
        EditorGUILayout.LabelField("作成したテトリスの形を表示(※手直し以外で触ること非推奨※)");
        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("0度回転");
                for (int i = 0; i < LOOP_NUM; ++i) //縦,y
                {
                    EditorGUILayout.BeginHorizontal(); // 横一列表示をするため、並列表示の設定をする
                    for (int j = 0; j < LOOP_NUM; ++j) //横,x
                    {
                        //ボタンを表示する、trueかどうかも保存している
                        showRange_0[i, j] = EditorGUILayout.Toggle("", showRange_0[i, j], GUI.skin.button, GUILayout.Width(15), GUILayout.Height(15));
                        //2次元配列を1次元配列に変更して仮保存
                        keepArray_0[i * rangeSize + j] = showRange_0[i, j];
                    }
                    EditorGUILayout.EndHorizontal();
                }
                //保存
                tetrisDataArray[(int)CreateAssetEnum.tetrimino_0].tetriminoArray = keepArray_0;

                tetrisDataArray[(int)CreateAssetEnum.tetrimino_0].tetrisAngle = (int)CreateAssetEnum.tetrimino_0;
                
            }
            using (new GUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField("90度回転");
                for (int i = 0; i < LOOP_NUM; ++i) //縦,y
                {
                    EditorGUILayout.BeginHorizontal(); // 横一列表示をするため、並列表示の設定をする

                    for (int j = 0; j < LOOP_NUM; ++j) //横,x
                    {
                        //ボタンを表示する、trueかどうかも保存している
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
                EditorGUILayout.LabelField("180度回転");
                for (int i = 0; i < LOOP_NUM; ++i) //縦,y
                {
                    EditorGUILayout.BeginHorizontal(); // 横一列表示をするため、並列表示の設定をする

                    for (int j = 0; j < LOOP_NUM; ++j) //横,x
                    {
                        //ボタンを表示する、trueかどうかも保存している
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
                EditorGUILayout.LabelField("270度回転");
                for (int i = 0; i < LOOP_NUM; ++i) //縦,y
                {
                    EditorGUILayout.BeginHorizontal(); // 横一列表示をするため、並列表示の設定をする

                    for (int j = 0; j < LOOP_NUM; ++j) //横,x
                    {
                        //ボタンを表示する、trueかどうかも保存している
                        showRange_270[i, j] = EditorGUILayout.Toggle("", showRange_270[i, j], GUI.skin.button, GUILayout.Width(15), GUILayout.Height(15));
                        keepArray_270[i * rangeSize + j] = showRange_270[i, j];
                    }
                    EditorGUILayout.EndHorizontal();
                }
                tetrisDataArray[(int)CreateAssetEnum.tetrimono_270].tetriminoArray = keepArray_270;
                tetrisDataArray[(int)CreateAssetEnum.tetrimono_270].tetrisAngle = (TetrisAngle)(int)CreateAssetEnum.tetrimono_270;
            }
        }

        //保存するボタン(入力漏れ確認)
        if (GUILayout.Button("データ保存"))
        {
            // データ名が入力されていない場合
            if (assetName == "")
            {
                EditorUtility.DisplayDialog("Error!", string.Format("データ名が入力されていません。"), "OK");
                return;
            }

            if (fallSpeed == 0)
            {
                EditorUtility.DisplayDialog("Error!", string.Format("落下スピードが入力されていません。"), "OK");
                return;
            }
            // テトリスの形が1マスも存在しない場合
            bool rangeError = false;
            foreach (bool flg in selectRange)
            {
                rangeError = flg;
                if (rangeError) break;
            }
            if (!rangeError)
            {
                EditorUtility.DisplayDialog("Error!", string.Format("テトリスの形が設定されていません。"), "OK");
                return;
            }

            // 保存確認
            if (!EditorUtility.DisplayDialog("テトリスデータ保存確認", string.Format("テトリスデータを保存しますか？"), "OK", "CANCEL")) return;

            CreateSkillScriptableObject();
        }

        if (GUILayout.Button("リセット"))
        {
            if (EditorUtility.DisplayDialog("リセット確認", string.Format("入力したデータをリセットしますか？"), "OK", "cancel")) Reset();
        }
    }
    //データ保存関数
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

