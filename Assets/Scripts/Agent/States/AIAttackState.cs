using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIAttackState : AIState
{ // The Agent can see the player and is ready to fire

    public AIStateID GetID()
        {
            return AIStateID.Attack;
        }

    public void Enter(AgentController agent)
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log("Player name is: "+player.name);
    }

    public void Exit(AgentController agent)
    {
        
    }

    public void Update(AgentController agent)
    {
        agent.navAgent.destination = agent.player.position;
        agent.transform.LookAt(new Vector3(agent.player.transform.position.x, agent.transform.position.y, agent.player.transform.position.z));
    }
}
