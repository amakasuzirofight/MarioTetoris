using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyToNumber", order = 1)]
public class EnemyToNumber : ScriptableObject
{
    [Header("“G‚ÌPrefab‚ðŠi”[‚µ‚Ü‚· csv‚É‚Í‚±‚Ì’l‚É20‘«‚µ‚½’l‚ð“ü—Í‚µ‚Ä‚­‚¾‚³‚¢")]
    public GameObject[] enemyList;
}
