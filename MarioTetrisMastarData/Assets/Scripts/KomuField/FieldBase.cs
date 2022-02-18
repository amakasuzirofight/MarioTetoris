using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FieldBase : MonoBehaviour
{
    protected string[] setCharacters;
    protected FieldBase[] nextField;
    public Action fieldcomplete;
    protected FieldChenger[] chengers;
    public FieldChenger activeChenger;

    virtual public void OpenField()
    {
        
    }

    virtual public void FieldCheck()
    {

    }

    virtual public void CloseField()
    {

    }

    virtual public void SceneChenge()
    {

    }

    virtual public string[] setCharactersGeter
    {
        get => setCharacters;
    }

    virtual public FieldBase[] nextFieldGeter
    {
        get => nextField;
    }
    virtual public void CreateBrock(List<FieldInfo> positions)
    {
        Debug.Log("out");
    }
}