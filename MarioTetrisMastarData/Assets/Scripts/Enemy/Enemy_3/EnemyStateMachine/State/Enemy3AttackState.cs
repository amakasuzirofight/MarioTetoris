using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemGenerater;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3AttackState : EnemyBaseMove, IEnemy3State
        {
            //EnemyのDamage状態処理

            public Enemy3StateType StateType => Enemy3StateType.ATTACK;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private GameObject player;
            private Enemy3Core core;
            private IGenerator itemGenerater;

            private float time;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                player        ??= Utility_.playerObject;
                core          ??= GetComponent<Enemy3Core>();
                itemGenerater ??= Utility.Locator<IGenerator>.GetT();

                time = 0f;

                // 一度だけしか攻撃しない
                if (!core.AtkFlg) return;

                // おそらくここで子オブジェクトにしてAttack状態で親子関係を解除すればいける
                itemGenerater.GenerateItem(RandomItem(), transform.position);
                core.AtkFlg = false;
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                StateChangeManager();
            }

            void IEnemy3State.OnFixedUpdate(Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnEnd(Enemy3StateType nextState, Enemy3Core enemy)
            {
            }



            // ステート変更メソッド
            private void StateChangeManager()
            {
                if (!Detection(Distance(player, gameObject), core.AtkRange))
                {
                    ChangeStateEvent(Enemy3StateType.STAY);
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                var player = collision.GetComponent<TestMarioAttack>();

                // Playerの本体に当たったら
                if (player != null)
                {
                    ChangeStateEvent(Enemy3StateType.DAMAGED);
                    StateChangeManager();
                }
            }

            // 時間待機メソッド
            public bool WaitTime(float count)
            {
                time += Time.deltaTime;

                if (time >= count)
                {
                    return true;
                }
                return false;
            }


            // アイテムランダム生成メソッド
            private ItemName RandomItem() 
            {
                int posionNum = 1;
                int maxRange  = 5;
                int randNum   = UnityEngine.Random.Range(0, maxRange);
                int[] itemTable = { 0,0,0,0,1};

                Debug.Log(randNum + "番のアイテムを生成");

                if (itemTable[randNum] == posionNum) return ItemName.Portion;
                else return ItemName.Stone;
            }
        }
    }
}
