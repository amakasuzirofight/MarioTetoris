using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector;

public class TestUpdateManager : MonoBehaviour
{
    IEnemyUpdateSendable update;

    // Start is called before the first frame update
    void Start()
    {
        update = GetComponent<IEnemyUpdateSendable>();
    }

    // Update is called once per frame
    void Update()
    {
        update.EnemyUpdate();
    }
}
