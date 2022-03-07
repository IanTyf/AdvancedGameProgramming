using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NectarUI : MonoBehaviour
{
    public HummingbirdAgent birdAgent;
    public TMP_Text nectarText;

    // Start is called before the first frame update
    void Start()
    {
        birdAgent = transform.parent.GetComponent<HummingbirdAgent>();
        nectarText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        nectarText.text = ""+birdAgent.NectarObtained.ToString("0.00");
    }
}
