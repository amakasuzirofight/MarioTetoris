using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
    public class RobotMove : MonoBehaviour
    {
        Rigidbody2D rigidbody2D;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ExecutionRoboWalk(float speed)
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            transform.position = new Vector3
                (Mathf.Clamp(transform.position.x, 0, Utility_.FieldData[0].Length), 
                 transform.position.y);
        }
    }

}
