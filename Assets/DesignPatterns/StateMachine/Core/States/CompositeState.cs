using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composite state is the state that contains multiple sub states inside
/// </summary>
public abstract class CompositeState : StateBase
{
    [Header("=== COMPOSITE STATE ===")]

    [Header("References")]
    [SerializeField] private List<StateBase> _subStates;
    [SerializeField] private StateBase _defaultSubState;       // Sub state that auto enter as default

    [Header("Data")]
    private StateBase _currentSubState;

    #region STATE BASE ==================================================================== STATE BASE

    public override void Initialize(object initializer = null)
    {
        foreach (var subState in _subStates)
            subState.Initialize(this);
    }

    public override void EnterState(object data = null, Action callback = null)
    {
        if (_defaultSubState != null)
            ChangeSubState(_defaultSubState, data, callback);
    }

    public override void ExitState()
    {
        ExitCurrentSubState();
    }

    #endregion

    #region SUB STATE ================================================================= SUB STATE

    public void ChangeSubState<T>(object data = null, Action callback = null) where T : StateBase
    {
        StateBase state = _subStates.Find(s => s.GetType() == typeof(T));
        if (state != null)
            ChangeSubState(state, data, callback);
        else
            Debug.LogError($"Can't find state {typeof(T)}");
    }

    private void ChangeSubState(StateBase newState, object data = null, Action callback = null)
    {
        _currentSubState?.ExitState();
        _currentSubState = newState;
        _currentSubState.EnterState(data, callback);
    }

    public void ExitCurrentSubState()
    {
        _currentSubState?.ExitState();
        _currentSubState = null;
    }

    #endregion
}
