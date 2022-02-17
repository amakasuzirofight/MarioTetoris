using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldType_Town : FieldBase
{
    public string[] setCharacters_;
    [SerializeField] private FieldChenger[] fieldChengers;
    [SerializeField] private GameObject tileMapObject;
    private GameObject tilemap;
    private GameObject[] chengers;

    public override void OpenField()
    {
        chengers = new GameObject[fieldChengers.Length];
        setCharacters = setCharacters_;
        tilemap = Instantiate(tileMapObject);
        for (int i = 0;i < fieldChengers.Length;i++)
        {
            chengers[i] = Instantiate(fieldChengers[i].gameObject);
        }
    }
    public override void FieldCheck()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            activeChenger = fieldChengers[0];
            fieldcomplete();
        }
    }

    public override void CloseField()
    {
        Destroy(tilemap);
        for (int i = 0;i < chengers.Length;i++)
        {
            Destroy(chengers[i]);
        }
    }

    public override string[] setCharactersGeter => base.setCharactersGeter;
}
