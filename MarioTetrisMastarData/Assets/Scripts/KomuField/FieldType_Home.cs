using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldType_Home : FieldBase
{
    public string[] setCharacters_;
    FieldChenger[] fieldChengers;
    [SerializeField] private GameObject tileMapObject;

    // Start is called before the first frame update
    void Start()
    {
        setCharacters = setCharacters_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OpenField()
    {
        base.OpenField();
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
