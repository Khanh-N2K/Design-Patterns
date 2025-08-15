using System;
using UnityEngine;

namespace N2K.StateMachine
{
    public abstract class StateBase : MonoBehaviour
    {
        public abstract void Initialize(object initializer = null);

        public abstract void EnterState(object data = null, Action callback = null);

        public abstract void ExitState();
    }
}