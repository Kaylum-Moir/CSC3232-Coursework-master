using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateID {
    Idle,
    Dead,
    Roaming,
    Chasing,
    Attack
}

public interface AIState
{
    AIStateID GetID();
    void Enter(AgentController agent);
    void Update(AgentController agent);
    void Exit(AgentController agent);
}
