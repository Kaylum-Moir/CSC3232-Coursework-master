using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeaponHold : MonoBehaviour
{
    [SerializeField]
    private Transform 
    agentHand, // Reference to the agent's hand
    weaponPivot; // Reference to the weapon container

    [SerializeField]
    private Vector3 rotationOffset; // Offset handled in editor for better visdualisation

    void Update()
    {
        // Update the weapon's position and rotation to match the agent's hand
        weaponPivot.position = agentHand.position;
        weaponPivot.rotation = agentHand.rotation * Quaternion.Euler(rotationOffset);
    }

}
