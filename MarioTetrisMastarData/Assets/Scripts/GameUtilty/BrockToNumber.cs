using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BrockData", menuName = "BrockToNumber", order = 1)]
public class BrockToNumber:ScriptableObject
{
    public GameObject[] objectsList;

    public GameObject ObjectGeter(int number)
    {
        return objectsList[number];
    }
}
