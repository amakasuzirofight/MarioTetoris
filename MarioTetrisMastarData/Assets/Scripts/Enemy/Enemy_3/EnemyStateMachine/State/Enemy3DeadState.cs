using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy3State
    {
        public class Enemy3DeadState : EnemyBaseDead, IEnemy3State
        {
            //EnemyのDead状態処理

            public Enemy3StateType StateType => Enemy3StateType.DEAD;
            public event Action<Enemy3StateType> ChangeStateEvent;

            private Enemy3Core core;

            void IEnemy3State.OnStart(Enemy3StateType beforeState, Enemy3Core enemy)
            {
                core ??= GetComponent<Enemy3Core>();

                ItemSpawn();

                // 死亡処理
                Dead();
            }

            void IEnemy3State.OnUpdate(Enemy3Core enemy)
            {
                
            }

            void IEnemy3State.OnFixedUpdate(Enemy3Core enemy)
            {
            }

            void IEnemy3State.OnEnd(Enemy3StateType nextState, Enemy3Core enemy)
            {
            }


            // ここで子オブジェクトを解除する
            private void ItemSpawn() 
            {
                if (core.AtkFlg) 
                {

                    Debug.Log("落ちたアイテム:");
                } 
            }
        }
    }
}
