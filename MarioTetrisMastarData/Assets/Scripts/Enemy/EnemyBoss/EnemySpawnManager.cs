using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawnManager : MonoBehaviour
    {
        // モブEnemy生成処理

        [SerializeField] private GameObject[] enemyObj;
        [SerializeField] private float[] posX;
        [SerializeField] private float   posY;
        [SerializeField] private float   spawnSpanTime = 1f;

        private float time = 0f;


        // Enemy生成処理
        public void EnemySpawn()
        {
            Vector2 spawnPos = new Vector2(RandomPos(),posY);

            if (!TimeCount()) return;

            // Enemyを生成
            Instantiate(enemyObj[RandomEnemy()]).transform.position = spawnPos;
        }

        // ランダムEnemyメソッド
        private int RandomEnemy()
        {
            int rand = Random.Range(0, enemyObj.Length);

            return rand;
        }

        // ランダム位置メソッド
        private float RandomPos() 
        {
            // Enemyが生成する位置(X軸)を返す
            int rand = Random.Range(0, posX.Length);

            return posX[rand];
        }

        // 待ち時間メソッド
        private bool TimeCount() 
        {
            time += Time.deltaTime;

            if (time > spawnSpanTime) 
            {
                time = 0f;
                return true;
            }

            return false;
        }
    }
}
