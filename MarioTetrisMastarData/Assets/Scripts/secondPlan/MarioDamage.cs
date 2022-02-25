using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mario
{
    public class MarioDamage :MarioCore,Connector.IDamageRecevable
    {
        public void DamageRecevable(int damage)
        {
            Hp -= damage;
        }
    }

}
