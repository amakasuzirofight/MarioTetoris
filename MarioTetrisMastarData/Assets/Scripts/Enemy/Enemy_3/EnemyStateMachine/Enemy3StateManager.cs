using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3StateManager : MonoBehaviour
        {
            // ステート変更する処理

            [SerializeField]
            private Enemy3Core enemy3;

            private Enemy3StateType crrentEnemy3State = Enemy3StateType.START;
            private Dictionary<Enemy3StateType, IEnemy3State> enemyStateDic = new Dictionary<Enemy3StateType, IEnemy3State>((int)Enemy3StateType.COUNT);


            void Start()
            {
                // IPlayerを配列で複数取得しているのはなぜ？
                IEnemy3State[] stateComponents = GetComponents<IEnemy3State>();

                if (stateComponents.Length != (int)Enemy3StateType.COUNT)
                {
                    Debug.LogError("Stateの数が違います");
                }

                // ここでDictionaryの中身を設定してる
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    IEnemy3State state = stateComponents[i];
                    // クラスが配列になってる！？ええ！？
                    // いやクラスの変数ではないのか？

                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == Enemy3StateType.COUNT)
                    {
                        Debug.LogError("無効なEnumです");
                        return;
                    }
                    if (enemyStateDic.ContainsKey(state.StateType))
                    {
                        Debug.LogError("Stateが重複しています");
                        return;
                    }

                    // 最後にここでPlayerのステートを代入
                    enemyStateDic[state.StateType] = state;
                    //Debug.Log(state.StateType);

                    // playerStateDicってのが現在のPlayerのステートを保持してる
                    // Dictionaryの[Key]にvalueを代入している
                    // Key(名前みたいなもん？)をPlayerのステートEnumにしている
                    // Valueは現在のPlayerのステートを入れてる

                    // playerStateDic[添え字] = 値をしている 
                }
            }

            void Update()
            {
                enemyStateDic[crrentEnemy3State].OnUpdate(enemy3);
            }

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemy3State].OnFixedUpdate(enemy3);
            }


            // 引数の中はEnum型にしてもいいっぽい！
            // ステート変更メソッド(event変数に代入するメソッド)
            public void ChangeState(Enemy3StateType enemyState)
            {
                if (enemyState is Enemy3StateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemy3State}");
                    return;
                }

                enemyStateDic[crrentEnemy3State].OnEnd(enemyState, enemy3);

                // 中身を変更
                crrentEnemy3State = enemyState;

                enemyStateDic[crrentEnemy3State].OnStart(enemyState, enemy3);
            }
        }
    }

}
