using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum RemoveRefType { OnDisable, OnDestroy, OnDisableAndDestroy }

public abstract class Reference<T> : MonoBehaviour
{
    [SerializeField]
    protected string referenceName;
    [SerializeField]
    protected RemoveRefType removeRefType;
    [SerializeField]
    protected LocalReference referenceBank;

    protected T reference;

    protected virtual void Awake()
    {
        Assert.IsNotNull(referenceBank, string.Format("{0}'s Reference script is missing a LocalReference object!", name));
        Assert.IsTrue(!string.IsNullOrEmpty(referenceName));

        if (reference != null)
        {
            referenceBank.Register(referenceName, reference);
        }
    }

    protected virtual void OnDisable()
    {
        switch (removeRefType)
        {
            case RemoveRefType.OnDisable:
            case RemoveRefType.OnDisableAndDestroy:
                referenceBank.Unregister(referenceName);
                break;

            default:
                break;
        }
    }

    protected virtual void OnDestroy()
    {
        switch (removeRefType)
        {
            case RemoveRefType.OnDestroy:
            case RemoveRefType.OnDisableAndDestroy:
                referenceBank.Unregister(referenceName);
                break;

            default:
                break;
        }
    }
}
