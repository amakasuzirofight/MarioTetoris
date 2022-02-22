using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace Enemy1State
    {
        public class Enemy1MoveState : EnemyBaseMove, IEnemy1State
        {
            // PlayerÇ™é¿ëïÇ∑ÇÈÇÃÅIÅH
            // PlayerÇÃStartèÛë‘èàóù

            [SerializeField] private GameObject player;
            [SerializeField] private bool flg;
            [SerializeField] private float range;

            public Enemy1StateType StateType => Enemy1StateType.MOVE;
            public event Action<Enemy1StateType> ChangeStateEvent;

            private Enemy1Core enemy1Core;
            private Rigidbody2D rb;

            void IEnemy1State.OnStart(Enemy1StateType beforeState, Enemy1Core enemy)
            {
                enemy1Core ??= GetComponent<Enemy1Core>();
                rb ??= GetComponent<Rigidbody2D>();
            }

            void IEnemy1State.OnUpdate(Enemy1Core enemy)
            {
                //Debug.Log(StateType);

                Move(rb, enemy1Core.Spd, Ditection(player, range));
            }

            void IEnemy1State.OnFixedUpdate(Enemy1Core enemy)
            {
            }

            void IEnemy1State.OnEnd(Enemy1StateType nextState, Enemy1Core enemy)
            {
            }

            private void StateChangeManager()
            {
                // PlayerÇ∆êGÇÍÇΩéû
                ChangeStateEvent(Enemy1StateType.DAMAGED);
            }
        }

    }


}
