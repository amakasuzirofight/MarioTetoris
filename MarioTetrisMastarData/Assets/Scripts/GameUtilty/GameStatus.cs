using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] private BrockToNumber activeBrockList;
    [SerializeField] private EnemyToNumber enemyList;
    [SerializeField] private MinoToNumber minoBrockList;
    [SerializeField] private GameObject player;

    void Awake()
    {
        Utility_.playerObject = Instantiate(player);

        for (int i = 0;i < activeBrockList.objectsList.Length;i++)
        {
            if (activeBrockList.objectsList[i] != null)
            {
                Utility_.objectGeter[i] = activeBrockList.objectsList[i];
                Debug.Log(Utility_.objectGeter[i] = activeBrockList.objectsList[i]);
            }
        }

        for (int i = 0;i < enemyList.enemyList.Length;i++)
        {
            Utility_.enemyGeter[i] = enemyList.enemyList[i];
        }

        for (int i = 0;i < minoBrockList.minoBrockList.Length;i++)
        {
            Utility_.minoGeter[i] = minoBrockList.minoBrockList[i];
        }
    }

    void Updata()
    {

    }
}
