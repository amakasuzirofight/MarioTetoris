using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Inputer;
namespace Mario
{
    public class MarioCore : MonoBehaviour, Connector.IDamageRecevable, IPlayerUpdate
    {
        [SerializeField] GameObject TestAttackCol;

        [SerializeField] bool IsDebugMode;
        [SerializeField] float speed;
        [SerializeField] float jumpPower;

        [SerializeField] MarioJump marioJump;
        [SerializeField] MarioWalk marioWalk;
        [SerializeField] LayerMask layerMask;
        [SerializeField] protected int attackPower;
        [SerializeField] protected int Hp;

        Rigidbody2D rigidbody2D;
        BoxCollider2D boxCollider2D;
        CapsuleCollider2D capsuleCollider2D;

        ICharInputter inputer;
        //IGroundCheck groundCheck;

        Vector3 oldPos;
        MarioState marioState;
        bool isGround;
        bool canJump;
        private void Awake()
        {
            Utility.Locator<IPlayerUpdate>.Bind(this);
        }
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            inputer = Utility.Locator<CharInput>.GetT();
            inputer.JumpEvent += JumpInputCheck;
            //groundCheck = groundCheckObj.GetComponent<IGroundCheck>();
            //groundCheck.ExitGround += ReceiveIsGround;
            //groundCheck.OnGround += ReceiveIsGround;

            canJump = true;
            marioState = MarioState.Stay;
            _localScale = transform.localScale;
        }
        public void MarioUpdate()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                TestAttackCol.SetActive(true);
            }
            else
            {
                TestAttackCol.SetActive(false);
            }
        }
        Vector3 _localScale;
        public void MarioFixedUpdate()
        {
            if (inputer.MoveInput() != 0)
            {
                //�e�X�g�p���]

                transform.localScale = new Vector3(inputer.MoveInput() == 1 ? _localScale.x : -_localScale.x, transform.localScale.y, transform.localScale.z);
            }

            isGround = CheckIsGround(capsuleCollider2D, 3, 0.005f);
            JumpJudge();
            MoveJudge();
            AirStateJudge();
            oldPos = transform.position;
        }
        private void Update()
        {
            if (IsDebugMode == false) return;
            MarioUpdate();

           
        }
        private void FixedUpdate()
        {
            if (IsDebugMode == false) return;
            MarioFixedUpdate();
          
        }
        void JumpInputCheck()
        {
            //�n�ʂɐڒn���Ă���Ƃ��W�����v����
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
            //�󒆂ɂ���Ƃ�
            if (isGround) return;
            //�O�̈ʒu��藎���Ă���ꍇ����
            if (oldPos.y > transform.position.y)
            {
                marioState = MarioState.JumpDown;
            }
            //�㏸���Ă�ꍇ
            if (oldPos.y < transform.position.y)
            {
                marioState = MarioState.JumpUp;
            }

        }
        public bool CheckIsGround(CapsuleCollider2D col, int maxLoop, float rayLange)
        {
            const float JUMPUP_CHECK_SPEED = 1f;
            bool hit;                   // �����������̔���ϐ�
            Vector3 checkPos = new Vector3(transform.position.x + (col.offset.x * transform.localScale.x), transform.position.y + (col.offset.y * transform.localScale.y), transform.position.z);          // �v���C���[�̍��W
            float colHalfWidth = ((col.size.x) * transform.localScale.x) / 2;         // �v���C���[�̔����̃R���C�_�[�̑傫��(X)
            float colHalfHigh = ((col.size.y) * transform.localScale.y) / 2;         // �v���C���[�̔����̃R���C�_�[�̑傫��(Y)
            Vector3 lineLength = transform.up * rayLange;  // ���C�̒���(�v����)

            // �㏸���͉������Ȃ�
            if (rigidbody2D.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }
            // checkPos�̈ʒu�����[�Ɉړ�
            checkPos.x -= colHalfWidth;
            checkPos.y -= colHalfHigh;
            // ���C������(5�{)
            for (int loop = 0; loop < maxLoop; ++loop)
            {
                Debug.DrawLine(checkPos + Vector3.down * 0.1f, checkPos - lineLength, Color.red);// �f�o�b�O�Ń��C��\��
                hit = Physics2D.Linecast(checkPos + Vector3.down * 0.1f, checkPos - lineLength, layerMask);
                if (hit) return true;
                checkPos.x += colHalfWidth;// ���W���{�P���Ă���
            }
            return false;
        }
        Vector2 dir = new Vector2(5f, 1f);

        public void DamageRecevable(int damage)
        {
            Hp -= damage;
        }

    }
    public enum MarioState
    {
        Walk, JumpUp, JumpDown, Stay, Count
    }

}
