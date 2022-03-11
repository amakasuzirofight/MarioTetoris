using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    namespace Reallty
    {
        public class PlayerView : MonoBehaviour
        {
            [SerializeField] GameObject player;
            ICoreGetter coreGetter;
            
            // Start is called before the first frame update
            void Start()
            {
                coreGetter = player.GetComponent<ICoreGetter>();
            }

            private void LateUpdate()
            {
                transform.position = coreGetter.GetCore().playerPos;
            }
        }

    }
}
