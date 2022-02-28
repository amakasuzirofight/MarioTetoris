using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tetris;

namespace Tetris
{
    public class TetrisScriptableObject : ScriptableObject
    {
        [SerializeField] public float fallSpeed;
        [SerializeField] public string assetName;
        [SerializeField] public TetrisTypeEnum tetrisTypeEnum;
        [SerializeField] public TetrisAngle tetrisAngle;
        [HideInInspector][SerializeField] public bool[] tetriminoArray = new bool[16];
        [HideInInspector] [SerializeField] public bool[,] tetriminoArrays = new bool[4, 4];

        public void ActivateScriptableObject(string name,float speed, TetrisTypeEnum tetrisType)
        {
            assetName = name;
            fallSpeed = speed;
            tetrisTypeEnum = tetrisType;
        }
    }

    
    public enum TetrisTypeEnum
    {
        Type_I, 
        Type_J,
        Type_L,
        Type_O,
        Type_S,
        Type_T,
        Type_Z
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

