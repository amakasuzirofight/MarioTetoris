using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] private BrockToNumber activeBrockList;
    [SerializeField] private MinoToNumber minoBrockList;

    void Awake()
    {
        for (int i = 0;i < activeBrockList.objectsList.Length;i++)
        {
            if (activeBrockList.objectsList[i] != null) Utility_.objectGeter[i] = activeBrockList.objectsList[i]; 
        }

        for (int i = 0;i < minoBrockList.minoBrockList.Length;i++)
        {
            Utility_.minoGeter[i] = minoBrockList.minoBrockList[i];
        }
    }
}
