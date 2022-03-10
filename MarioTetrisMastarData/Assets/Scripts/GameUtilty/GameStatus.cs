using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    [SerializeField] private BrockToNumber activeBrockList;
    [SerializeField] private EnemyToNumber enemyList;
    [SerializeField] private MinoToNumber minoBrockList;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject robot;
    [SerializeField] private Text nameText;
    [SerializeField] private Text mainText;
    [SerializeField] private Image namePanel;
    [SerializeField] private Image mainPanel;

    [SerializeField] private bool DebugMode = false;
    [SerializeField] private Vector3 DebugPos = Vector3.zero;
    [SerializeField] private bool CreateFlg = false;

    void Awake()
    {
        if (nameText != null) Utility_.MessageSetting(mainText,nameText,mainPanel,namePanel);

        if (!CreateFlg)
        {
            Utility_.playerObject = Instantiate(player);
            if (DebugMode) Utility_.playerObject.transform.position = DebugPos;
            Utility_.robotObject = Instantiate(robot);
            Utility_.robotObject.transform.position = Vector3.zero;
        }
        else
        {
            Utility_.playerObject = player;
        }

        for (int i = 0;i < activeBrockList.objectsList.Length;i++)
        {
            if (activeBrockList.objectsList[i] != null)
            {
                Utility_.objectGeter[i] = activeBrockList.objectsList[i];
                Debug.Log(Utility_.objectGeter[i] = activeBrockList.objectsList[i]);
            }
        }

        for (int i = 0;i < enemyList.enemyList.Length;i++)
        {
            Utility_.enemyGeter[i] = enemyList.enemyList[i];
        }

        for (int i = 0;i < minoBrockList.minoBrockList.Length;i++)
        {
            Utility_.minoGeter[i] = minoBrockList.minoBrockList[i];
        }
    }

    void Updata()
    {

    }
}
