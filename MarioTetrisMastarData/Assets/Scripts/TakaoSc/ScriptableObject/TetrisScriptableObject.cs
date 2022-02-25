using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tetris;

namespace Tetris
{
    public class TetrisScriptableObject : ScriptableObject
    {
        [SerializeField] public int fallSpeed;
        [SerializeField] public string assetName;
        [SerializeField] public TetrisTypeEnum tetrisTypeEnum;
        [SerializeField] public TetrisAngle tetrisAngle;
        [HideInInspector][SerializeField] public bool[] tetriminoArray = new bool[16];

        public void ActivateScriptableObject(string name,int speed, TetrisTypeEnum tetrisType)
        {
            assetName = name;
            fallSpeed = speed;
            tetrisTypeEnum = tetrisType;
        }
    }

    
    public enum TetrisTypeEnum
    {
        Type_T,
        Type_I,
        Type_L,
        Type_J,
        Type_Z,
        Type_S,
        Type_O
    }

    public enum TetrisAngle
    {
        Angle_0,
        Angle_90,
        Angle_180,
        Angle_270,
        Count
    }
}

