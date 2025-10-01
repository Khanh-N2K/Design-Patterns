using System;
using UnityEngine;

public class PopupBase : PoolMember
{
    [field: Header("=== POPUP BASE ===")]

    [Header("References")]
    [field: SerializeField]
    public PopupType Type { get; private set; }

    [Header("Events")]
    protected Action OnShowed;
    protected Action OnHidden;

    public void SetCallbacks(Action onShowed, Action onHidden)
    {
        OnShowed = onShowed;
        OnHidden = onHidden;
    }

    #region ================================ ACTUAL SHOW/ HIDE ===================================
    public virtual void Show()
    {
        gameObject.SetActive(true);
        OnShowed?.Invoke();
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        OnHidden?.Invoke();
    }
    #endregion ------------------------------------------------------------------------------------

    #region ======================== TEMPORARLY SHOW/ HIDE UNDER TOP POPUP ========================
    public virtual void TempShowUnderTopPopup()
    {
        gameObject.SetActive(true);
    }
    public virtual void TempHideUnderTopPopup()
    {
        gameObject.SetActive(false);
    }
    #endregion ----------------------------------------------------------------------------------------
}
