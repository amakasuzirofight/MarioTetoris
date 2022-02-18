using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private FieldBase baseScene;
    FieldBase nowField;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        nowField = baseScene;
        // obj = Instantiate(nowField.gameObject);
        nowField.fieldcomplete = FieldChenge;
        nowField.OpenField();
    }

    // Update is called once per frame
    void Update()
    {
        nowField.FieldCheck();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            fields.Add(new FieldInfo(0,0));
            fields.Add(new FieldInfo(0,1));
            fields.Add(new FieldInfo(0,2));
            fields.Add(new FieldInfo(0,3));

            nowField.CreateBrock(fields);
        }
    }

    public void FieldChenge()
    {
        nowField.CloseField();
        FieldBase next = nowField.activeChenger.nextField;
        nowField.activeChenger = default;
        nowField = next;
        nowField.fieldcomplete = FieldChenge;
        nowField.OpenField();
    }
}
