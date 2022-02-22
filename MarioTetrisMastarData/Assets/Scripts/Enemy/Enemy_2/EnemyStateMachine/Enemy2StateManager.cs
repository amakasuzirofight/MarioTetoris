using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2StateManager : MonoBehaviour
        {
            // ステート変更する処理

            [SerializeField]
            private Enemy2Core enemy2;

            private Enemy2StateType crrentEnemy2State = Enemy2StateType.START;
            private Dictionary<Enemy2StateType, IEnemy2State> enemyStateDic = new Dictionary<Enemy2StateType, IEnemy2State>((int)Enemy2StateType.COUNT);


            void Start()
            {
                // IPlayerを配列で複数取得しているのはなぜ？
                IEnemy2State[] stateComponents = GetComponents<IEnemy2State>();

                if (stateComponents.Length != (int)Enemy2StateType.COUNT)
                {
                    Debug.LogError("Stateの数が違います");
                }

                // ここでDictionaryの中身を設定してる
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    IEnemy2State state = stateComponents[i];
                    // クラスが配列になってる！？ええ！？
                    // いやクラスの変数ではないのか？

                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == Enemy2StateType.COUNT)
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
                enemyStateDic[crrentEnemy2State].OnUpdate(enemy2);
            }

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemy2State].OnFixedUpdate(enemy2);
            }


            // 引数の中はEnum型にしてもいいっぽい！
            // ステート変更メソッド(event変数に代入するメソッド)
            public void ChangeState(Enemy2StateType enemyState)
            {
                if (enemyState is Enemy2StateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemy2State}");
                    return;
                }

                enemyStateDic[crrentEnemy2State].OnEnd(enemyState, enemy2);

                // 中身を変更
                crrentEnemy2State = enemyState;

                enemyStateDic[crrentEnemy2State].OnStart(enemyState, enemy2);
            }
        }
    }

}
