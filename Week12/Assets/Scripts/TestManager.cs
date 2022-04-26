using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField]
    SimpleScriptableObject simpleScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        simpleScriptableObject.timeAllowed = 998;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDestroy()
    {
        simpleScriptableObject.ResetCertainValue();
    }

}
