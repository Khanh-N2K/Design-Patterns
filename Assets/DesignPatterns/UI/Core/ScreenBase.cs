using System;
using UnityEngine;

public class ScreenBase : PoolMember
{
    [field: Header("=== SCREEN BASE ===")]

    [field: SerializeField] public ScreenType Type { get; private set; }

    protected Action<object> OnActiveCallback;
    protected Action<object> OnInactiveCallback;

    public void SetCallbacks(Action<object> onActiveCallback, Action<object> onInactiveCallback)
    {
        OnActiveCallback = onActiveCallback;
        OnInactiveCallback = onInactiveCallback;
    }

    public virtual void OnActive() { }
    public virtual void OnInactive() { }
}
