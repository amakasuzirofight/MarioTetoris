using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
public class ViewUI : MonoBehaviour
{
    [SerializeField] Button beginningButton;
    [SerializeField] Button continuationButton;
    [SerializeField] Button editButton;
    [SerializeField] Button settingButton;

    private void Awake()
    {
        Utility.Locator<ViewUI>.Bind(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UIChanger(int num)
    {
        switch (num)
        {
            case 0:
                beginningButton.transform.DOScale(new Vector2(1.5f, 1.5f), 0.2f);
                continuationButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                editButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                settingButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                break;
            case 1:
                beginningButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                continuationButton.transform.DOScale(new Vector2(1.5f, 1.5f), 0.2f);
                editButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                settingButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                break;
            case 2:
                editButton.transform.DOScale(new Vector2(1.5f, 1.5f), 0.2f);
                continuationButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                beginningButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                settingButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                break;
            case 3:
                settingButton.transform.DOScale(new Vector2(1.5f, 1.5f), 0.2f);
                editButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                continuationButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                beginningButton.transform.DOScale(new Vector2(1, 1), 0.1f);
                break;
            default:
                break;
        }
    }
}
