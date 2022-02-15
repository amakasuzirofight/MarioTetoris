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
