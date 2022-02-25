using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private int brockNumber;

    [SerializeField] private Vector2[] instancePosition;

    public void Create()
    {
        for (int i = 0;i < instancePosition.Length;i++)
        {
            Utility_.CsvWriter(FieldInfo.VecToFieldInfo(instancePosition[i]),brockNumber);
        }
    }

    public void NewLoadCreate()
    {

    }
}
