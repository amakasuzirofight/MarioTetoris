using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace Field
{
    public class FieldType_Stage : FieldBase
    {
        int frameCount = 0;
        int brockNumber = 10;
        StreamWriter writer;
        public string[] setCharacters_;
        List<Brock> activeBrock = new List<Brock>();
        private List<GameObject> groundObjects;
        [SerializeField] private int checkFrame;
        [SerializeField] private TextAsset stage_csv;
        [SerializeField] private TextAsset brock_csv;

        public override void OpenField()
        {
            Utility.Locator<FieldBase>.Bind(this);

            activeChenger = default;
            frameCount = 0;
            brockNumber = 10;
            setCharacters = setCharacters_;
            groundObjects = new List<GameObject>();

            #region フィールド展開

            Utility_.CsvToIntList(stage_csv);

            groundObjects = CreateField(Utility_.FieldData);

            #endregion

            #region ブロック展開
            StringReader brockReader = new StringReader(brock_csv.text);
            List<string[]> brockBase = new List<string[]>();

            while (brockReader.Peek() != -1)
            {
                string line = brockReader.ReadLine();
                brockBase.Add(line.Split(','));
            }

            for (int i = 0; i < brockBase.Count; i++)
            {
                activeBrock.Add(new Brock(Convert.ToInt32(brockBase[i][0])));
                brockNumber = Convert.ToInt32(brockBase[i][0]);
                for (int j = 1; j < brockBase[i].Length - 1; j += 2)
                {
                    activeBrock[i].csv_pos.Add(new FieldInfo(Convert.ToInt32(brockBase[i][j]), Convert.ToInt32(brockBase[i][j + 1])));
                    GameObject obj = Instantiate(Utility_.minoGeter[0]);
                    obj.transform.position = new Vector3(activeBrock[i].csv_pos[activeBrock[i].csv_pos.Count - 1].width, activeBrock[i].csv_pos[activeBrock[i].csv_pos.Count - 1].height * -1);
                    activeBrock[activeBrock.Count - 1].minos.Add(obj);
                }
            }

            #endregion

            CreateCharacters();
        }

        public override void FieldCheck()
        {
            frameCount++;

            if (frameCount % checkFrame == 0)
            {
                FieldUpdate();
                frameCount = 0;
            }

            ChengerCheck();
        }

        public override void CloseField()
        {

            DestroyObjects(groundObjects);

            groundObjects = new List<GameObject>();
        }

        public override void CreateBrock(List<FieldInfo> positions)
        {
            brockNumber++;

            activeBrock.Add(new Brock(brockNumber));

            for (int i = 0; i < positions.Count; i++)
            {
                Utility_.FieldData[positions[i].height][positions[i].width] = brockNumber;
                activeBrock[activeBrock.Count - 1].csv_pos.Add(positions[i]);
                activeBrock[activeBrock.Count - 1].minos.Add(Instantiate(Utility_.minoGeter[0]));
            }

        }

        private void FieldUpdate()
        {
            for (int i = 0; i < activeBrock.Count; i++)
            {
                // if (activeBrock[i].stateCheck())
                {
                    Debug.Log($"number{activeBrock[i].brockNumGet()} is Fall");
                    for (int j = 0; j < activeBrock[i].csv_pos.Count; j++)
                    {
                        if (activeBrock[i].csv_pos[j].height >= Utility_.FieldData.Count - 1) // 最下層であるなら落下防止
                        {
                            Debug.Log("先がないため移動を中止します");
                            activeBrock[i].stateChenge(false);
                            break;
                        }
                        else if (activeBrock[i].brockNumGet() != Utility_.FieldData[activeBrock[i].csv_pos[j].height + 1][activeBrock[i].csv_pos[j].width]
                                 && Utility_.FieldData[activeBrock[i].csv_pos[j].height + 1][activeBrock[i].csv_pos[j].width] != (int)FieldNumber.NONE) // 移動先が違うブロックであるなら落下防止
                        {
                            activeBrock[i].stateChenge(false);
                            Debug.Log("先にブロックを確認したため移動を中止します");
                            Debug.Log(activeBrock[i].stateCheck());
                            break;
                        }
                    }

                    if (activeBrock[i].stateCheck()) fallBrocks(i);
                }

            }
            #region writer
            //using (writer = new StreamWriter($"Assets/FieldData/{stage_csv.name}.csv", append: false))
            //{
            //    for (int i = 0; i < activeBrock.Count; i++)
            //    {
            //        // if (activeBrock[i].stateCheck())
            //        {
            //            Debug.Log($"number{activeBrock[i].brockNumGet()} is Fall");
            //            for (int j = 0; j < activeBrock[i].csv_pos.Count; j++)
            //            {
            //                if (activeBrock[i].csv_pos[j].height >= FieldData.Count - 1) // 最下層であるなら落下防止
            //                {
            //                    Debug.Log("先がないため移動を中止します");
            //                    activeBrock[i].stateChenge(false);
            //                    break;
            //                }
            //                else if (activeBrock[i].brockNumGet() != FieldData[activeBrock[i].csv_pos[j].height + 1][activeBrock[i].csv_pos[j].width]
            //                         && FieldData[activeBrock[i].csv_pos[j].height + 1][activeBrock[i].csv_pos[j].width] != (int)FieldNumber.NONE) // 移動先が違うブロックであるなら落下防止
            //                {
            //                    activeBrock[i].stateChenge(false);
            //                    Debug.Log("先にブロックを確認したため移動を中止します");
            //                    Debug.Log(activeBrock[i].stateCheck());
            //                    break;
            //                }
            //            }

            //            if (activeBrock[i].stateCheck()) fallBrocks(i);
            //        }

            //    }

            //    // 記述
            //    for (int i = 0; i < FieldData.Count; i++)
            //    {
            //        for (int j = 0; j < FieldData[i].Length; j++)
            //        {
            //            writer.Write(FieldData[i][j].ToString());
            //            if (j < FieldData[i].Length - 1) writer.Write(",");
            //        }

            //        writer.WriteLine();
            //    }
            //}
            #endregion
        }

        private void fallBrocks(int i)
        {
            for (int j = 0; j < activeBrock[i].csv_pos.Count; j++)
            {
                Utility_.FieldData[activeBrock[i].csv_pos[j].height][activeBrock[i].csv_pos[j].width] = 0;
                activeBrock[i].csv_pos[j] += new FieldInfo(1, 0);
                activeBrock[i].minos[j].transform.position = new Vector3(activeBrock[i].csv_pos[j].width, activeBrock[i].csv_pos[j].height * -1, 0);
                Utility_.FieldData[activeBrock[i].csv_pos[j].height][activeBrock[i].csv_pos[j].width] = activeBrock[i].brockNumGet();
            }
        }

        public FieldNumber FieldNumberGeter(Vector3 pos, Difference difference = Difference.STAY)
        {
            FieldNumber fieldnum = FieldNumber.NULL;
            int number = -1;
            switch (difference)
            {
                case Difference.UP:
                    number = Utility_.FieldData[((int)pos.y + 1) * -1][(int)pos.x];
                    break;
                case Difference.DOWN:
                    number = Utility_.FieldData[((int)pos.y - 1) * -1][(int)pos.x];
                    break;
                case Difference.LEFT:
                    number = Utility_.FieldData[(int)pos.y * -1][(int)pos.x - 1];
                    break;
                case Difference.RIGHT:
                    number = Utility_.FieldData[(int)pos.y * -1][(int)pos.x + 1];
                    break;
                case Difference.STAY:
                    number = Utility_.FieldData[(int)pos.y * -1][(int)pos.x];
                    break;

            }

            if (number >= 0)
            {
                number = number >= 10 ? 10 : number;
                return fieldnum = (FieldNumber)number;
            }
            else
            {
                Debug.LogError("確認できない値を検知しました");
                return fieldnum = FieldNumber.NULL;
            }
        }
    }
}