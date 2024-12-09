using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCombatController : MonoBehaviour
{
    private AISensor sensor;

    private AgentController agentController;
    
    [SerializeField]
    private GameObject weaponContainer;

    private AgentGunController gunController;
    // Start is called before the first frame update
    void Start()
    {
        sensor = GetComponent<AISensor>();
        gunController = weaponContainer.GetComponent<AgentGunController>();
        agentController = GetComponent<AgentController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sensor.playerVisible)
        {
            //Debug.Log("THE AI WANTS TO SHOOT");
            if (agentController.stateMachine.currentState != AIStateID.Dead)
            {
                gunController.Shoot();
            }
        }
    }
}
