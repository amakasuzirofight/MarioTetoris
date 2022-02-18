using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreatePlayerData")]
public class PlayerScriptable : ScriptableObject
{
    [Header("�ړ����x"), SerializeField] public float walkSpeed;
    [Header("�W�����v��"), SerializeField] public float jumpPower;
    [Header("�ő�W�����v����"), SerializeField] public float jumpTime;
    [Header("�W�����v�̍��x�J��"), SerializeField] public AnimationCurve jumpCurve;
    [Header("�v���C���[�̗������x"), SerializeField] public float fallSpeed;
    [Header("�L�����̔w�̍���"), SerializeField] public float playerHigh;
    [Header("�L�����̉���"), SerializeField] public float playerWidth;
}