using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;

namespace Enemy
{
    namespace Enemy2State
    {
        public class Enemy2Attack : MonoBehaviour
        {
            // Enemy2�̍U������

            private IDamageRecevable damageRecevable;
            private Enemy2Core core;

            // Start is called before the first frame update
            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                core = GetComponent<Enemy2Core>();
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                // Player��������
                damageRecevable.DamageRecevable(core.AtkPow);
            }
        }
    }
}
