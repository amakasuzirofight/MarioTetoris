using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FieldType_Town : FieldBase
    {
        public string[] setCharacters_;
        [SerializeField] private GameObject tileMapObject;
        private GameObject tilemap;

        public override void OpenField()
        {
            activeChenger = default;
            chengers_objects = new GameObject[fieldChengers.Length];
            setCharacters = setCharacters_;
            tilemap = Instantiate(tileMapObject);

            CreateCharacters();
        }
        public override void FieldCheck()
        {
            ChengerCheck();
        }

        public override void CloseField()
        {
            Destroy(tilemap);

            DestroyObjects();
        }

        public override string[] setCharactersGeter => base.setCharactersGeter;
    }

}