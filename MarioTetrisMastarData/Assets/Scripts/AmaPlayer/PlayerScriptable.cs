using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CommandData", menuName = "CreateCommandData")]
public class PlayerScriptable : ScriptableObject
{
    [Header("�ړ����x"), SerializeField] public float walkSpeed;
    [Header("�W�����v��"), SerializeField] public float jumpPower;
    [Header("�ő�W�����v����"), SerializeField] public float jumpTime;

}