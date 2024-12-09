using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIDeathState : AIState
{

    public AIStateID GetID()
        {
            return AIStateID.Dead;
        }

    public void Enter(AgentController agent)
    {
        agent.navAgent.enabled = false;
    }

    public void Exit(AgentController agent)
    {

    }

    public void Update(AgentController agent)
    {
        
    }
}
