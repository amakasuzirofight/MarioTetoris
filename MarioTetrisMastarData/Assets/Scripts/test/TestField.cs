using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestField : MonoBehaviour
    {
        public static float blockLange = 1;
        public float[,] testFieldArray = new float[8, 20] {
            {0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,0,0,0,0, 0,0,0,0,0,0,0,0,0,0},
        };

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
    }

}
