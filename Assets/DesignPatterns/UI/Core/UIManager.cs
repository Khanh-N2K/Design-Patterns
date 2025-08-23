using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ScreenType
{
    Screen1 = 0,
    Screen2 = 1,
}

[Serializable]
public enum PopupType
{
    Popup1 = 0,
    Popup2 = 1,
}

public class UIManager : Singleton<UIManager>
{
    [Header("=== UI MANAGER ===")]

    [Header("Holders")]
    [SerializeField] private Transform _screenHolder;
    [SerializeField] private Transform _popupHolder;

    [Header("Prefabs")]
    [SerializeField] private List<ScreenBase> _screenPrefabs;
    [SerializeField] private List<PopupBase> _popupPrefabs;

    // Mapping type to prefab
    private Dictionary<ScreenType, ScreenBase> _screenDict = new();
    private Dictionary<PopupType, PopupBase> _popupDict = new();

    private ScreenBase _currentScreen;
    private Stack<PopupBase> _popupStack = new();

    public override void Initialize()
    {
        base.Initialize();

        foreach (ScreenBase screen in _screenPrefabs)
            _screenDict.Add(screen.Type, screen);
        foreach (PopupBase popup in _popupPrefabs)
            _popupDict.Add(popup.Type, popup);
    }

    #region ================================== SCREEN ====================================
    public void ShowScreen(ScreenType type, Action<object> onActiveCallback = null, Action<object> onInactiveCallback = null)
    {
        HideCurrentScreen();

        ScreenBase newScreen = ObjectPoolAtlas.Instance.Get(_screenDict[type].gameObject, _screenHolder)
            .GetComponent<ScreenBase>();
        newScreen.SetCallbacks(onActiveCallback, onInactiveCallback);
        newScreen.OnActive();
        _currentScreen = newScreen;
    }

    public void HideCurrentScreen()
    {
        if (_currentScreen != null)
        {
            _currentScreen?.OnInactive();
            _currentScreen.ReleaseToPool();
            _currentScreen = null;
        }
    }
    #endregion ------------------------------------------------------------------------------

    #region ======================================= POPUP ===================================
    public void ShowPopup(PopupType type, Action<object> onActiveCallback = null, Action<object> onInactiveCallback = null)
    {
        if (_popupStack.Count > 0)
            _popupStack.Peek().OnTempInactive();

        PopupBase newPopup = ObjectPoolAtlas.Instance.Get(_popupDict[type].gameObject, _popupHolder)
            .GetComponent<PopupBase>();
        newPopup.SetCallbacks(onActiveCallback, onInactiveCallback);
        newPopup.OnActive();

        _popupStack.Push(newPopup);
    }

    public void HideTopPopup()
    {
        if (_popupStack.Count > 0)
        {
            PopupBase topPopup = _popupStack.Pop();
            topPopup?.OnInactive();
            topPopup.ReleaseToPool();

            if (_popupStack.Count > 0)
                _popupStack.Peek().OnTempActive();
        }
    }

    public void HidePopup(PopupBase targetPopup)
    {
        if (targetPopup == null || !_popupStack.Contains(targetPopup))
            return;

        Stack<PopupBase> buffer = new Stack<PopupBase>();

        while (_popupStack.Count > 0)
        {
            PopupBase top = _popupStack.Pop();
            if (top == targetPopup)
            {
                top.OnInactive();
                top.ReleaseToPool();
                break;
            }
            else
            {
                buffer.Push(top);
            }
        }

        while (buffer.Count > 0)
            _popupStack.Push(buffer.Pop());

        if (_popupStack.Count > 0)
            _popupStack.Peek().OnTempActive();
    }

    public void HideAllPopups()
    {
        while (_popupStack.Count > 0)
        {
            PopupBase popup = _popupStack.Pop();
            popup?.OnInactive();
            popup.ReleaseToPool();
        }
    }
    #endregion -------------------------------------------------------------------------------
}
