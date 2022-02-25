using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FieldType_Select : FieldBase
    {
        [SerializeField] private TextAsset field_csv;
        private List<GameObject> objects;

        public override void OpenField()
        {
            activeChenger = default;
            Utility_.CsvToIntList(field_csv);
            CreateCharacters();
            objects = CreateField(Utility_.FieldData);
        }

        public override void FieldCheck()
        {
            ChengerCheck();
        }

        public override void CloseField()
        {
            DestroyObjects(objects);
            objects = new List<GameObject>();
        }
    }
}