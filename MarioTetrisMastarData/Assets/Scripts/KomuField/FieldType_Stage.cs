using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class FieldType_Stage : FieldBase
{
    int frameCount = 0;
    int brockNumber = 10;
    StreamWriter writer;
    public string[] setCharacters_;
    FieldChenger[] fieldChengers;
    List<int[]> FieldData = new List<int[]>();
    List<Brock> activeBrock = new List<Brock>();
    [SerializeField] private int checkFrame;
    [SerializeField] private TextAsset stage_csv;
    [SerializeField] private GameObject groundObject;
    [SerializeField] private GameObject minoObject;

    public override void OpenField()
    {
        Debug.Log("clear");
        frameCount = 0;
        brockNumber = 10;
        setCharacters = setCharacters_;
        StringReader reader = new StringReader(stage_csv.text);
        List<string[]> baseData = new List<string[]>();

        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            baseData.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
        }

        for (int i = 0; i < baseData.Count; i++)
        {
            FieldData.Add(new int[baseData[i].Length]);
            for (int j = 0; j < baseData[i].Length; j++)
            {
                FieldData[i][j] = Convert.ToInt32(baseData[i][j]);
            }
        }

        for (int i = 0;i < FieldData.Count;i++)
        {
            for (int j = 0;j < FieldData[i].Length;j++)
            {
                Debug.Log($"FieldData[{i}][{j}] = {FieldData[i][j]}");
                if (FieldData[i][j] == (int)FieldNumber.GROUND)
                {
                    GameObject obj = Instantiate(groundObject);
                    obj.transform.position = new Vector3(j, i * -1, 0);
                }
            }
        }
    }

    public override void FieldCheck()
    {
        if (frameCount >= 1500)
        {
            Debug.Log("�������I��");
            return;
        }

        frameCount++;

        if (frameCount % checkFrame == 0)
        {
            FieldUpdate();
            frameCount = 0;
        }
    }

    public override void CloseField()
    {
        base.CloseField();
    }

    public override string[] setCharactersGeter => base.setCharactersGeter;

    public override void CreateBrock(List<FieldInfo> positions)
    {
        activeBrock.Add(new Brock(brockNumber));

        for (int i = 0;i < positions.Count ;i++)
        {
            FieldData[positions[i].height][positions[i].width] = brockNumber;
            activeBrock[activeBrock.Count - 1].csv_pos.Add(positions[i]);
            activeBrock[activeBrock.Count - 1].minos.Add(Instantiate(minoObject));
        }

        brockNumber++;
    }

    private void FieldUpdate()
    {
        using (writer = new StreamWriter($"Assets/FieldData/{stage_csv.name}.csv", append: false))
        {
            for (int i = 0; i < activeBrock.Count; i++)
            {
                if (activeBrock[i].stateCheck())
                {
                    Debug.Log($"number{activeBrock[i].brockNumGet()} is Fall");
                    for (int j = 0; j < activeBrock[i].csv_pos.Count; j++)
                    {
                        if (activeBrock[i].csv_pos[j].height >= FieldData.Count - 1) // �ŉ��w�ł���Ȃ痎���h�~
                        {
                            Debug.Log("�悪�Ȃ����߈ړ��𒆎~���܂�");
                            activeBrock[i].stateChenge(false);
                            break;
                        }
                        else if (activeBrock[i].brockNumGet() != FieldData[activeBrock[i].csv_pos[j].height + 1][activeBrock[i].csv_pos[j].width]
                                 && FieldData[activeBrock[i].csv_pos[j].height + 1][activeBrock[i].csv_pos[j].width] != (int)FieldNumber.NONE) // �ړ��悪�Ⴄ�u���b�N�ł���Ȃ痎���h�~
                        {
                            activeBrock[i].stateChenge(false);
                            Debug.Log("��Ƀu���b�N���m�F�������߈ړ��𒆎~���܂�");
                            Debug.Log(activeBrock[i].stateCheck());
                            break;
                        }
                    }

                    if (activeBrock[i].stateCheck()) fallBrocks(i);
                }

            }

            // �L�q
            for (int i = 0; i < FieldData.Count; i++)
            {
                for (int j = 0; j < FieldData[i].Length; j++)
                {
                    writer.Write(FieldData[i][j].ToString());
                    if (j < FieldData[i].Length - 1) writer.Write(",");
                }

                writer.WriteLine();
            }
        }
    }

    private void fallBrocks(int i)
    {
        for (int j = 0; j < activeBrock[i].csv_pos.Count; j++)
        {
            FieldData[activeBrock[i].csv_pos[j].height][activeBrock[i].csv_pos[j].width] = 0;
            activeBrock[i].csv_pos[j] += new FieldInfo(1, 0);
            activeBrock[i].minos[j].transform.position = new Vector3(activeBrock[i].csv_pos[j].width, activeBrock[i].csv_pos[j].height * -1, 0);
            FieldData[activeBrock[i].csv_pos[j].height][activeBrock[i].csv_pos[j].width] = activeBrock[i].brockNumGet();
        }
    }
}
