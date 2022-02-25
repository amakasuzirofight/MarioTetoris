using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


[Serializable]
public struct PlayerData
{
    public string name;
    public int hp;
    public int attack;
    public int defense;
}

[Serializable]
public struct Data
{
    public string csv_baseData;

    public Data(string create)
    {
        csv_baseData = create;
    }
}

public class komuTestSC_JSON : MonoBehaviour
{
    [SerializeField] private TextAsset csv;
    string datapath;
    List<int[]> fieldPath = new List<int[]>();
    private void Awake()
    {
        //���߂ɕۑ�����v�Z����@Application.dataPath�ō��J���Ă���Unity�v���W�F�N�g��Assets�t�H���_�������w�肵�āA���ɕۑ���������
        datapath = Application.dataPath + "/TestJson.json";

        for (int i = 0;i < 9;i++)
        {
            fieldPath.Add(new int[9]);
            for (int j = 0;j < 9;j++)
            {
                fieldPath[i][j] = i;
                Debug.Log(fieldPath[i][j]);
            }
        }
    }

    void Start()
    {
        //PlayerData player1 = new PlayerData();
        //player1.name = "�^�J�V";
        //player1.hp = 350;
        //player1.attack = 20;
        //player1.defense = 10;
        //SaveTest(player1);
        SaveTest();
    }

    //�Z�[�u�̃��\�b�h
    public void SaveTest(PlayerData player)
    {
        string jsonstr = JsonUtility.ToJson(player);//�󂯎����PlayerData��JSON�ɕϊ�
        StreamWriter writer = new StreamWriter(datapath, false);//���߂Ɏw�肵���f�[�^�̕ۑ�����J��
        writer.WriteLine(jsonstr);//JSON�f�[�^����������
        writer.Flush();//�o�b�t�@���N���A����
        writer.Close();//�t�@�C�����N���[�Y����
    }

    public void SaveTest()
    {
        StreamWriter writer = new StreamWriter(datapath, false);
        StringReader reader = new StringReader(csv.text);

        string data = JsonUtility.ToJson(new Data(csv.text));
        writer.WriteLine(data);

        writer.Flush();
        writer.Close();
    }
}
