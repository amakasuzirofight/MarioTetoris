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
            Vector2 vec = new Vector2(instancePosition[i].x,instancePosition[i].y * - 1);
            Utility_.CsvWriter(FieldInfo.VecToFieldInfo(vec),brockNumber);
        }
    }

    public void NewLoadCreate()
    {

    }
}
