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
            // Enemy2の攻撃処理

            private IDamageRecevable damageRecevable;
            private Enemy2Core core;

            // Start is called before the first frame update
            void Start()
            {
                damageRecevable = GetComponent<IDamageRecevable>();
                core = GetComponent<Enemy2Core>();
            }

            // Playerに触れた時にダメージを与える(甘糟待ち)
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.TryGetComponent(out MarioCore at))
                {
                    IDamageRecevable damage = at;
                    Debug.Log("殺す！！！");
                    damage.RecoveryRecevable(core.AtkPow);
                }
            }
        }
    }
}
