using System;
using UnityEngine;

public class PopupBase : PoolMember
{
    [field: Header("=== POPUP BASE ===")]

    [field: SerializeField] public PopupType Type { get; private set; }

    protected Action<object> OnActiveCallback;
    protected Action<object> OnInactiveCallback;

    public void SetCallbacks(Action<object> onActiveCallback, Action<object> onInactiveCallback) 
    {
        OnActiveCallback = onActiveCallback;
        OnInactiveCallback = onInactiveCallback;
    }

    // Actually hidden/ active, which may return/ get popup from pool
    public virtual void OnActive() { }
    public virtual void OnInactive() { }

    // Temporary hidden/ active when another popup is called on top
    public virtual void OnTempActive() 
    { 
        gameObject.SetActive(true);
    }
    public virtual void OnTempInactive() 
    {
        gameObject.SetActive(false);
    }
}
