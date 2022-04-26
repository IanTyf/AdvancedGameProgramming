using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    [SerializeField]
    LocalReference reference;
    [SerializeField]
    string referenceName = "MainCamera";
    Transform targetToLook;
	// Use this for initialization
	void Start () {
        targetToLook = reference.GetReference<Transform>(referenceName);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(targetToLook);
    }
}
