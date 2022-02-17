using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilty
{
}

public enum Difference
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    STAY,
    COUNT
}

public enum FieldNumber
{
    NULL = -1,
    NONE = 0,
    GROUND = 1,
    MINO = 10
}

public struct FieldInfo
{
    public int height;
    public int width;

    public FieldInfo(int newHeight, int newWidth)
    {
        height = newHeight;
        width = newWidth;
    }

    public static FieldInfo operator +(FieldInfo base_,FieldInfo add)
    {
        return new FieldInfo(base_.height + add.height,base_.width + add.width);
    }

    public static FieldInfo operator -(FieldInfo base_, FieldInfo add)
    {
        return new FieldInfo(base_.height - add.height, base_.width - add.width);
    }
}

public class Brock
{
    public List<FieldInfo> csv_pos;
    public bool fallFlg;
    public List<GameObject> minos;
    private int brockNumber;

    public Brock(int newNum)
    {
        csv_pos = new List<FieldInfo>();
        minos = new List<GameObject>();
        fallFlg = true;
        brockNumber = newNum;
        Debug.Log($"CreateBrock number:{brockNumber}");
    }

    public void brockNumSet(int num)
    {
        brockNumber = num;
    }

    public int brockNumGet()
    {
        return brockNumber;
    }

    public void stateChenge(bool fallState)
    {
        fallFlg = fallState;
        Debug.Log($"chenge => {fallFlg}");
    }

    public bool stateCheck()
    {
        return fallFlg;
    }
}