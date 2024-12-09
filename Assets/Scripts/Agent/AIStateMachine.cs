using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    public AIState[] states;
    public AgentController agent;
    public AIStateID currentState;

    public AIStateMachine(AgentController agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AIStateID)).Length; // Get number of states for array length
        states = new AIState[numStates];
    }

    public void RegisterState(AIState state)
    {
        int i = (int)state.GetID();
        states[i] = state; // Register states for use in state machine
    }

    public AIState GetState(AIStateID stateID)
    {
        int i = (int)stateID;
        return states[i];
    }

    public void UpdateState()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(AIStateID newState)
    {
        //Debug.Log("Test");
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
