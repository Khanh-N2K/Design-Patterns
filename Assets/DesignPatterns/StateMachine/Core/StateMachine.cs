using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [Header("=== STATE MACHINE ===")]

    [Header("References")]
    [SerializeField] private List<StateBase> _states;

    [Header("Data")]
    private StateBase _currentState;

    public virtual void Initialize()
    {
        foreach (var state in _states)
            state.Initialize(this);
    }

    public void ChangeState<T>(object data = null, Action callback = null) where T : StateBase
    {
        StateBase state = _states.Find(s => s.GetType() == typeof(T));
        if (state != null)
        {
            _currentState?.ExitState();
            _currentState = state;
            _currentState?.EnterState(data, callback);
        }
        else
        {
            Debug.LogError($"Can't find state {nameof(T)}");
        }
    }

    public void ExitCurrentState()
    {
        _currentState?.ExitState();
        _currentState = null;
    }
}