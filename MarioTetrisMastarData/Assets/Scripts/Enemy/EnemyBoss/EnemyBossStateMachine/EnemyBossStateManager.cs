using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossStateManager : MonoBehaviour,IEnemyUpdateSendable
        {
            // ステート変更する処理

            [SerializeField] private EnemyBossCore enemyBoss;


            [SerializeField] private EnemyBossStateType[] stateBrock_1;
            [SerializeField] private EnemyBossStateType[] stateBrock_2;
            [SerializeField] private EnemyBossStateType[] stateBrock_3;

            private EnemyBossStateType[] stateFlow;
            private EnemyBossCore core;
            private int thisStateNum = 0;


            private EnemyBossStateType crrentEnemyBossState = EnemyBossStateType.START;
            private Dictionary<EnemyBossStateType, IEnemyBossState> enemyStateDic = new Dictionary<EnemyBossStateType, IEnemyBossState>((int)EnemyBossStateType.COUNT);


            void Start()
            {
                core = GetComponent<EnemyBossCore>();

                // 複数あるステートクラスを取得
                IEnemyBossState[] stateComponents = GetComponents<IEnemyBossState>();

                if (stateComponents.Length != (int)EnemyBossStateType.COUNT)
                {
                    Debug.LogError("Stateの数が違います");
                }

                // ここでDictionaryの中身を設定してる
                for (int i = 0; i < stateComponents.Length; i++)
                {
                    // ステートクラスを設定
                    IEnemyBossState state = stateComponents[i];

                    // eventを設定
                    state.ChangeStateEvent += ChangeState;

                    if (state.StateType == EnemyBossStateType.COUNT)
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

                    // playerStateDicってのが現在のPlayerのステートを保持してる
                    // Dictionaryの[Key]にvalueを代入している
                    // Key(名前みたいなもん？)をPlayerのステートEnumにしている
                    // Valueは現在のPlayerのステートを入れてる

                    // playerStateDic[添え字] == 現在のステート
                }
            }

            private void Update()
            {
                enemyStateDic[crrentEnemyBossState].OnUpdate(enemyBoss);
            }

            private void FixedUpdate()
            {
                enemyStateDic[crrentEnemyBossState].OnFixedUpdate(enemyBoss);
            }

            // ダメージ状態遷移メソッド
            private void DamageState()
            {
                enemyStateDic[crrentEnemyBossState].OnEnd(EnemyBossStateType.IDLE/*ダメージ*/, enemyBoss);
                //thisStateNum = 0; // ステートを一回リセットする場合
                crrentEnemyBossState = EnemyBossStateType.DAMAGE; // ダメージ
                enemyStateDic[crrentEnemyBossState].OnStart(EnemyBossStateType.IDLE, enemyBoss);
            }

            // ステート中断処理
            public void BreakState()
            {
                DamageState();
            }

            // ステート変更メソッド(event変数に代入するメソッド)
            public void ChangeState(EnemyBossStateType enemyState)
            {
                if (enemyState is EnemyBossStateType.COUNT)
                {
                    Debug.LogErrorFormat($"Count state is specified ,CurrentState{crrentEnemyBossState}");
                    return;
                }

                enemyStateDic[crrentEnemyBossState].OnEnd(enemyState, enemyBoss);


                // キャラクターのHPによって行うステートを変更する
                if (core.Hp < 30) stateFlow = stateBrock_3;
                else if (core.Hp < 50) stateFlow = stateBrock_2;
                else if (core.Hp <= 100) stateFlow = stateBrock_1;


                // 中身を変更
                crrentEnemyBossState = stateFlow[thisStateNum];

                // 要素数が配列の長さ未満の場合
                if (thisStateNum < stateFlow.Length - 1) thisStateNum++;
                else thisStateNum = 0;

                enemyStateDic[crrentEnemyBossState].OnStart(enemyState, enemyBoss);
            }

            // Updateインタフェース
            void IEnemyUpdateSendable.EnemyUpdate()
            {
            }
        }
    }
}
