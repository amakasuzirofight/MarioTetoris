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
        [SerializeField] public TetrisTypeEnum tetrisTeypEnum;
        [SerializeField] public TetrisAngle tetrisAngle;
        [HideInInspector][SerializeField] public bool[] tetriminoArray = new bool[16];

        public void ActivateScriptableObject(string name,int speed, TetrisTypeEnum tetrisType)
        {
            assetName = name;
            fallSpeed = speed;
            tetrisTeypEnum = tetrisType;
        }
    }

    
    public enum TetrisTypeEnum
    {
        Teyp_T,
        Teyp_I,
        Teyp_L,
        Teyp_J,
        Teyp_Z,
        Teyp_S,
        Teyp_O
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

