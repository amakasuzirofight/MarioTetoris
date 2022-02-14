using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestField : MonoBehaviour
    {
        public static float blockLange = 1;
        const int HIGHT = 8;
        const int WIDTH = 20;
        public float[,] testFieldArray = new float[HIGHT, WIDTH] {
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
        //Œ»İ‚ÌÀ•W‚ğ”z—ñ‚É•ÏŠ·
        public static Vector2 PosToArray(Vector3 pos)
        {
            return new Vector2((pos.x / 1)+HIGHT-1, WIDTH-(pos.y / 1));
        }

        //public static bool IsBetween(float value, float a, float b)
        //{
        //    if (a > b)
        //    {
        //        return value <= a && value >= b;
        //    }
        //    return value <= b && value >= a;
        //}

    }


}
