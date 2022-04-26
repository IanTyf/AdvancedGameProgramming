using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelManagerExample : MonoBehaviour
{
    [SerializeField]
    ItemScriptableObjectExample itemScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        //itemExcelItem item = itemDatabase.GetExcelItemByID("W001");
        var itemInfo = itemScriptableObject.GetItem("W001");
        Debug.Log(itemInfo.excelData.name);
    }

}
