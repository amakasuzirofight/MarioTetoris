using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyToNumber", order = 1)]
public class EnemyToNumber : ScriptableObject
{
    [Header("�G��Prefab���i�[���܂� csv�ɂ͂��̒l��20�������l����͂��Ă�������")]
    public GameObject[] enemyList;
}
