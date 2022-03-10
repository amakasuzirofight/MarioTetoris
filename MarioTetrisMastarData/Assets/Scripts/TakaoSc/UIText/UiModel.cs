using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class UiModel : MonoBehaviour
{
    public IntReactiveProperty selectSceneNum = new IntReactiveProperty(0);
    int selectTempNum = 0;


    enum SelectSceneEnum
    {
        scene_Beginning,
        //scene_Continuation,
        scene_Edit,
        scene_Setting
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Utility.Locator<UiModel>.Bind(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(selectSceneNum.Value < (int)SelectSceneEnum.scene_Edit)
            {
                selectSceneNum.Value++;
            }
            else
            {
                selectSceneNum.Value = (int)SelectSceneEnum.scene_Beginning;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectSceneNum.Value > (int)SelectSceneEnum.scene_Beginning)
            {
                selectSceneNum.Value--;
            }
            else
            {
                selectSceneNum.Value = (int)SelectSceneEnum.scene_Edit;
            }
        }

        //if(Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if (selectSceneNum.Value == (int)SelectSceneEnum.scene_Setting) return;
        //    selectTempNum = selectSceneNum.Value;
        //    selectSceneNum.Value = (int)SelectSceneEnum.scene_Setting;
        //}

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    selectSceneNum.Value = selectTempNum;
        //}

        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (selectSceneNum.Value)
            {
                case (int)SelectSceneEnum.scene_Beginning:
                    SceneManager.LoadScene("KomugiScene");
                    break;
                //case (int)SelectSceneEnum.scene_Continuation:
                //    Debug.Log(selectSceneNum.Value + " ‚Â‚Ã‚«‚©‚ç");
                //    break;
                case (int)SelectSceneEnum.scene_Edit:
                    SceneManager.LoadScene("EditStage");
                    break;
                default:
                    break;
            }
        }
    }
}
