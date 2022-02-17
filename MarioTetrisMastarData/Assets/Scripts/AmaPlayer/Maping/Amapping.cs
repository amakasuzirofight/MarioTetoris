using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Player;
public class Amapping 
{
    [RuntimeInitializeOnLoadMethod]//ç≈ë¨
    void MappingAma()
    {
        PlayerJump playerJump = new PlayerJump();
        Locator<IPlayerAction>.Bind(playerJump, 2);
        PlayerWalk playerMove = new PlayerWalk();
        Locator<IPlayerAction>.Bind(playerMove,1);
        PlayerStay stay = new PlayerStay();
        Locator<IPlayerAction>.Bind(stay,0);
    }
 
 
}
