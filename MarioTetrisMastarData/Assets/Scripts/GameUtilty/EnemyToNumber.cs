using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyToNumber", order = 1)]
public class EnemyToNumber : ScriptableObject
{
    [Header("敵のPrefabを格納します csvにはこの値に20足した値を入力してください")]
    public GameObject[] enemyList;
}
