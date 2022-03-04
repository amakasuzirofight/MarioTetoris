using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class UIPresentter : MonoBehaviour
{
    [SerializeField] UiModel uiModel;
    [SerializeField] ViewUI viewUI;
    // Start is called before the first frame update
    void Start()
    {
        uiModel.selectSceneNum.Subscribe(x =>
        {
            viewUI.UIChanger(x);
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
