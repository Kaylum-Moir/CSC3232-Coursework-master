using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIIdleState : AIState
{

    public AIStateID GetID()
        {
            return AIStateID.Idle;
        }

    public void Enter(AgentController agent)
    {
    }

    public void Exit(AgentController agent)
    {
        
    }

    public void Update(AgentController agent)
    {

    }
}
