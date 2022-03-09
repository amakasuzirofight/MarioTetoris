using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBossBombSpawn : MonoBehaviour
    {
        // 爆弾を生成する処理

        [SerializeField] private float[] posX;
        [SerializeField] private float posY;
        [SerializeField] private float spawnSpanTime;

        private string[] strTable = { "Item", "Item", "Item", "Item", "Block", "Item" };
        private float time = 0f;

        private int i = 0;

        // Enemy生成処理
        public void BombSpawn()
        {
            Vector2 spawnPos = new Vector2(RandomPos(), posY);

            if (i < strTable.Length)
            {
                if (!TimeCount()) return;
                if (strTable[i] == "Block") Debug.Log("ブロックを生成");
                else                        Debug.Log("アイテムを生成");
                i++;
            }
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
