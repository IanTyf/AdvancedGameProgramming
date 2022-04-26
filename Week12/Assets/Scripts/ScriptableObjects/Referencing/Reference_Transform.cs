using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference_Transform : Reference<Transform> {

	private Reference_Transform()
    {
        referenceName = "Transform Reference";
    }

    protected override void Awake()
    {
        reference = transform;
        base.Awake();
    }
}
