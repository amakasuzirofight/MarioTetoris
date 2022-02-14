using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1StateManager : MonoBehaviour
        {
            // ステート変更する処理

            [SerializeField]
            private Enemy1Core enemy1;

            private Enemy1StateType crrentEnemy1State = Enemy1StateType.COUNT;
            private Dictionary<Enemy1StateType, IEnemy1State> enemyStateDic = new Dictionary<Enemy1StateType, IEnemy1State>((int)Enemy1StateType.COUNT);


            void Start()
            {
                // IPlayerを配列で複数取得しているのはなぜ？
                IEnemy1State[] stateComponents = GetComponents<IEnemy1State>();

                if (stateComponents.Length != (int)Enemy1StateType.COUNT)
                {
                    Debug.LogError("Stateの数が違います");
                }

                // ここでDictionaryの中身を設定してる
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    IEnemy1State state = stateComponents[i];
                    // クラスが配列になってる！？ええ！？
                    // いやクラスの変数ではないのか？

                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == Enemy1StateType.COUNT)
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
                enemyStateDic[crrentEnemy1State].OnUpdate(enemy1);
            }

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemy1State].OnFixedUpdate(enemy1);
            }


            // 引数の中はEnum型にしてもいいっぽい！
            // ステート変更メソッド(event変数に代入するメソッド)
            public void ChangeState(Enemy1StateType playerState)
            {
                if (playerState is Enemy1StateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemy1State}");
                    return;
                }

                enemyStateDic[crrentEnemy1State].OnEnd(playerState, enemy1);

                // 中身を変更
                crrentEnemy1State = playerState;

                enemyStateDic[crrentEnemy1State].OnStart(playerState, enemy1);
            }
        }
    }

}
