using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

                Debug.Log(StateType);

                if (core.Hp <= 0)
                {
                    Dead();
                }
                else
                {
                    animator.SetTrigger("Damage");
                    ChangeStateManager();
                } 

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

                StartCoroutine(WaitTime());
            }

            public void SceneChange() 
            {
                
            }

            IEnumerator WaitTime() 
            {
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("TakaoScene2");
            }
        }
    }
}
