using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Inputer;
namespace Mario
{
    public class MarioCore : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float jumpPower;
        [SerializeField] GameObject inputObj;
        //[SerializeField] GameObject groundCheckObj;
        [SerializeField] MarioJump marioJump;
        [SerializeField] MarioWalk marioWalk;
        [SerializeField] LayerMask layerMask;

        Rigidbody2D rigidbody2D;
        BoxCollider2D boxCollider2D;
        CapsuleCollider2D capsuleCollider2D;

        ICharInputter inputer;
        //IGroundCheck groundCheck;

        Vector3 oldPos;
        MarioState marioState;
        bool isGround;
        bool canJump;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            inputer = inputObj.GetComponent<ICharInputter>();
            inputer.JumpEvent += JumpInputCheck;
            //groundCheck = groundCheckObj.GetComponent<IGroundCheck>();
            //groundCheck.ExitGround += ReceiveIsGround;
            //groundCheck.OnGround += ReceiveIsGround;

            canJump = true;
            marioState = MarioState.Stay;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            isGround = CheckIsGround(capsuleCollider2D, 3, 0.005f);
            JumpJudge();
            MoveJudge();
            AirStateJudge();
            oldPos = transform.position;
            Debug.Log(isGround);

        }
        void JumpInputCheck()
        {
            //地面に接地しているときジャンプする
            if (isGround)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
        }
        void JumpJudge()
        {
            if (canJump == false) return;
            canJump = false;
            marioJump.ExecutionJump(jumpPower);
        }
        void MoveJudge()
        {
            marioWalk.ExecutionWalk(speed * inputer.MoveInput());
            if (!isGround) return;
            if (inputer.MoveInput() != 0)
            {
                marioState = MarioState.Walk;
            }
            else
            {
                marioState = MarioState.Stay;
            }
        }
        void ReceiveIsGround(bool Ground)
        {
            isGround = Ground;
        }
        void AirStateJudge()
        {
            //空中にいるとき
            if (isGround) return;
            //前の位置より落ちている場合落下
            if (oldPos.y > transform.position.y)
            {
                marioState = MarioState.JumpDown;
            }
            //上昇してる場合
            if (oldPos.y < transform.position.y)
            {
                marioState = MarioState.JumpUp;
            }

        }
        public bool CheckIsGround(CapsuleCollider2D col, int maxLoop, float rayLange)
        {
            const float JUMPUP_CHECK_SPEED = 1f;
            bool hit;                   // 当たった時の判定変数
            Vector3 checkPos = new Vector3(transform.position.x + (col.offset.x * transform.localScale.x), transform.position.y + (col.offset.y * transform.localScale.y), transform.position.z);          // プレイヤーの座標
            float colHalfWidth = ((col.size.x) * transform.localScale.x) / 2;         // プレイヤーの半分のコライダーの大きさ(X)
            float colHalfHigh = ((col.size.y) * transform.localScale.y) / 2;         // プレイヤーの半分のコライダーの大きさ(Y)
            Vector3 lineLength = transform.up * rayLange;  // レイの長さ(要調節)

            // 上昇中は何もしない
            if (rigidbody2D.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }
            // checkPosの位置を左端に移動
            checkPos.x -= colHalfWidth;
            checkPos.y -= colHalfHigh;
            // レイを引く(5本)
            for (int loop = 0; loop < maxLoop; ++loop)
            {
                Debug.DrawLine(checkPos + Vector3.down * 0.1f, checkPos - lineLength, Color.red);// デバッグでレイを表示
                hit = Physics2D.Linecast(checkPos + Vector3.down * 0.1f, checkPos - lineLength, layerMask);
                if (hit) return true;
                checkPos.x += colHalfWidth;// 座標を＋１していく
            }
            return false;
        }
    }
    public enum MarioState
    {
        Walk, JumpUp, JumpDown, Stay, Count
    }

}
