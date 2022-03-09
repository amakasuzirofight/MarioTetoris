using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawnManager : MonoBehaviour
    {
        // ���uEnemy��������

        [SerializeField] private GameObject[] enemyObj;
        [SerializeField] private float[] posX;
        [SerializeField] private float   posY;
        [SerializeField] private float   spawnSpanTime = 1f;

        private float time = 0f;


        // Enemy��������
        public void EnemySpawn()
        {
            Vector2 spawnPos = new Vector2(RandomPos(),posY);

            if (!TimeCount()) return;

            // Enemy�𐶐�
            Instantiate(enemyObj[RandomEnemy()]).transform.position = spawnPos;
        }

        // �����_��Enemy���\�b�h
        private int RandomEnemy()
        {
            int rand = Random.Range(0, enemyObj.Length);

            return rand;
        }

        // �����_���ʒu���\�b�h
        private float RandomPos() 
        {
            // Enemy����������ʒu(X��)��Ԃ�
            int rand = Random.Range(0, posX.Length);

            return posX[rand];
        }

        // �҂����ԃ��\�b�h
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
