using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    private Dictionary<System.Type, IState> states = new Dictionary<System.Type, IState>();

    public void AddState(IState state)
    {
        states[state.GetType()] = state;
    }

    public void ChangeState<T>() where T : IState
    {
        if (states.TryGetValue(typeof(T), out IState newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        else
        {
            Debug.LogError($"State of type {typeof(T)} not found in state machine!");
        }
    }

    public void ChangeState(System.Type stateType)
    {
        if (states.TryGetValue(stateType, out IState newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        else
        {
            Debug.LogError($"State of type {stateType} not found in state machine!");
        }
    }

    public T GetState<T>() where T : IState
    {
        if (states.TryGetValue(typeof(T), out IState state))
        {
            return (T)state;
        }
        return default(T);
    }

    public bool IsCurrentState<T>() where T : IState
    {
        return currentState != null && currentState.GetType() == typeof(T);
    }

    private void Update()
    {
        currentState?.Update();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}