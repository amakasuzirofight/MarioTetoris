using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStay :IPlayerAction

{
        public event ChangeStater changeStateEvent;
        public event CoreUpdate coreUpdateEvent;

        public void StateUpdate()
        {
            throw new System.NotImplementedException();
        }

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

