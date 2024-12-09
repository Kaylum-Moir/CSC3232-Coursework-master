using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIChasingState : AIState
{

    public AIStateID GetID()
        {
            return AIStateID.Chasing;
        }

    public void Enter(AgentController agent)
    {
        //Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log("Player name is: "+player.name);

         Collider[] nearbyAgents = Physics.OverlapSphere(agent.transform.position, agent.config.broadcastRange, LayerMask.GetMask("Enemy"));
         foreach (Collider near in nearbyAgents)
         {
            near.GetComponent<AgentController>().stateMachine.ChangeState(AIStateID.Chasing); // Turns nearby enemies onto the player
         }
    }

    public void Exit(AgentController agent)
    {
        
    }

    public void Update(AgentController agent)
    {
        agent.navAgent.destination = agent.player.position;
    }
}
