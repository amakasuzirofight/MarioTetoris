using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBossBombSpawn : MonoBehaviour
    {
        // ���e�𐶐����鏈��

        [SerializeField] private float[] posX;
        [SerializeField] private float posY;
        [SerializeField] private float spawnSpanTime;

        private string[] strTable = { "Item", "Item", "Item", "Item", "Block", "Item" };
        private float time = 0f;

        private int i = 0;

        // Enemy��������
        public void BombSpawn()
        {
            Vector2 spawnPos = new Vector2(RandomPos(), posY);

            if (i < strTable.Length)
            {
                if (!TimeCount()) return;
                if (strTable[i] == "Block") Debug.Log("�u���b�N�𐶐�");
                else                        Debug.Log("�A�C�e���𐶐�");
                i++;
            }
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
