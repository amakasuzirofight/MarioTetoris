using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;
using Mario;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2Attack : MonoBehaviour
        {
            // Enemy2�̍U������

            private IDamageRecevable damageRecevable;
            private Enemy2Core core;


            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                core = GetComponent<Enemy2Core>();
            }


            // Player�ɐG�ꂽ���Ƀ_���[�W��^����(�Ñ��҂�)
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;
                    Debug.Log("�E���I�I�I");
                    damage.DamageRecevable(core.AtkPow);
                }
            }
        }
    }
}
