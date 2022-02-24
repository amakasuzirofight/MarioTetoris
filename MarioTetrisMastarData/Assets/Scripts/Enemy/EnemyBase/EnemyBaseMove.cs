using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBaseMove : MonoBehaviour
    {
        // Enemy�̊��ړ��֌W�N���X
        protected int dir = -1;

        // �ړ����\�b�h
        protected virtual void Move(Rigidbody2D rb ,float spd) 
        {
            Vector3 scale = transform.localScale;

            rb.velocity = new Vector3(dir * spd, rb.velocity.y, 0f);
            transform.localScale = new Vector3(-dir , scale.y);
        }


        // ������Ԃ����\�b�h
        protected virtual float Distance(GameObject obj)
        {
            float distance = obj.transform.position.x - transform.position.x;

            return distance;
        }


        // �I�u�W�F�N�g���o���\�b�h
        protected virtual bool Detection(float dis, float diteRange) 
        {
            if (Mathf.Abs(dis) < diteRange) return true;
            return false;
        }


        // ���o�����I�u�W�F�N�g�Ǐ]���\�b�h
        protected virtual void Follow(Rigidbody2D rb, float spd, float dis, bool flg)
        {
            Vector3 scale = transform.localScale;

            if (!flg) return;

            if (dis < 0) dir = -1;
            else         dir =  1;

            rb.velocity = new Vector3(dir * spd, rb.velocity.y, 0f);
            transform.localScale = new Vector3(-dir , scale.y);
        }
    }
}
