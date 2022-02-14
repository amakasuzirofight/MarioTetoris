using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CommandData", menuName = "CreateCommandData")]
public class PlayerScriptable : ScriptableObject
{
    [Header("移動速度"), SerializeField] public float walkSpeed;
    [Header("ジャンプ力"), SerializeField] public float jumpPower;
    [Header("最大ジャンプ時間"), SerializeField] public float jumpTime;

}