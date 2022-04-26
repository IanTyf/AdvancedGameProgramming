using System.Collections.Generic;
using UnityEngine;

public class LocalReference : ScriptableObject
{
    private Dictionary<string, object> refs;

    public void Register(string name, object obj)
    {
        CheckStatus();

        if (refs.ContainsKey(name))
        {
            refs[name] = obj;
        }
        else
        {
            refs.Add(name, obj);
        }
    }

    public void Unregister(string name)
    {
        CheckStatus();

        if (refs.ContainsKey(name))
        {
            refs.Remove(name);
        }
    }

    public T GetReference<T>(string name)
    {
        CheckStatus();

        object obj = null;

        if (refs.TryGetValue(name, out obj))
        {
            return (T)obj;
        }

        return default(T);
    }

    private void CheckStatus()
    {
        if (refs == null)
        {
            refs = new Dictionary<string, object>();
        }
    }
}
