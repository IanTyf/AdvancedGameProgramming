using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void select()
    {
        GameObject grid = GameObject.Find("Grid");
        grid.GetComponent<InitializeGrid>().SelectSlot(this.gameObject);
        Debug.Log("selected " + this.gameObject.name);
    }
}
