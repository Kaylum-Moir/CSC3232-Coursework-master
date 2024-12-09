using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField]
    public float
    maxRadius,
    maxAngle;
    //maxDistance;

    [SerializeField]
    private LayerMask playerMask;

    [HideInInspector]
    public Vector3 targetDirection;

    public bool playerVisible;


    void Start()
    {
        playerVisible = false;
    }

    private void FixedUpdate()
    {
        if (!playerVisible) // No need to use the sensor if the AI should already know the position of the player
        {
            CheckFOV();
        }
    }

    private void CheckFOV()
    {
        Collider[] rangeCast = Physics.OverlapSphere(transform.position, maxRadius, playerMask); // Casting sphere to check if player is in visible range

        if(rangeCast.Length > 0)
        {
            Transform target = rangeCast[0].transform;
            targetDirection = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, targetDirection) < maxAngle / 2) // Divided by 2 as calculation is from centre of transform
            {
                float playerDistance = Vector3.Distance(transform.position, target.position);
                //if (playerDistance <= maxDistance) // Ensure player is within view distance (Redundant)
                Physics.Raycast(transform.position , targetDirection, out RaycastHit hit, playerDistance);
                if (hit.collider.tag == "Player")
                {
                    playerVisible = true;
                }
                else
                {
                    playerVisible = false;
                }
            }
            else
            {
                playerVisible = false;
            }
        }
        else
        {
            playerVisible = false;
        }
    }
}
