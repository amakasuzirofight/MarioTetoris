using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldType_Town : FieldBase
{
    public string[] setCharacters_;
    FieldChenger[] fieldChengers;
    [SerializeField] private GameObject tileMapObject;

    public override void OpenField()
    {
        setCharacters = setCharacters_;
    }
    public override void FieldCheck()
    {
        base.FieldCheck();
    }

    public override void CloseField()
    {
        base.CloseField();
    }

    public override string[] setCharactersGeter => base.setCharactersGeter;
}
