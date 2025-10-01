using System;
using UnityEngine;

public class ScreenBase : PoolMember
{
    [field: Header("=== SCREEN BASE ===")]

    [Header("References")]
    [field: SerializeField]
    public ScreenType Type { get; private set; }

    [Header("Events")]
    protected Action _onShowed;
    protected Action _onHidden;

    public void SetCallbacks(Action onShowed, Action onHidden)
    {
        _onShowed = onShowed;
        _onHidden = onHidden;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        _onShowed?.Invoke();
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        _onHidden?.Invoke();
    }
}
