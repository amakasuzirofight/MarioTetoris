using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    namespace EnemyBossState
    {
        public class EnemyBossDamageState : EnemyBaseDead, IEnemyBossState
        {
            //Enemy‚ÌStartó‘Ôˆ—

            [SerializeField] private GameObject bossObj;
            [SerializeField] private GameObject animObj;

            public EnemyBossStateType StateType => EnemyBossStateType.DAMAGE;
            public event Action<EnemyBossStateType> ChangeStateEvent;

            private EnemyBossCore core;
            private Animator animator;
            private float transTimeCount = 3f;

            void IEnemyBossState.OnStart(EnemyBossStateType beforeState, EnemyBossCore enemy)
            {
                core ??= GetComponent<EnemyBossCore>();
                animator ??= animObj.GetComponent<Animator>();

                animator.SetTrigger("Damage");
                Debug.Log(StateType);

                if (core.Hp <= 0)
                {
                    Dead();
                }
                else ChangeStateManager();
            }

            void IEnemyBossState.OnUpdate(EnemyBossCore enemy)
            {
            }

            void IEnemyBossState.OnFixedUpdate(EnemyBossCore enemy)
            {
            }

            void IEnemyBossState.OnEnd(EnemyBossStateType nextState, EnemyBossCore enemy)
            {
            }

            private void ChangeStateManager()
            {
                ChangeStateEvent(EnemyBossStateType.IDLE);
            }

            protected override void Dead()
            {
                animator.SetTrigger("Down");
                Destroy(bossObj, 1.2f);
            }
        }
    }
}
