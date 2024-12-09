using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIRoamingState : AIState
{

     /*
        (description copied from POITimeCapture.cs)
        Timestamp stores the last time this POI was checked
        Agents will roam between POIs to look for the player
        Agents will prioritise older timestamps at closer distances

        Agents will 'score' nearby POIs in their roaming radius
        score = timestampBiasWeight * (current time - timestamp) - distanceWeight * poiDistance

        Best score gets visited
    */

    Collider currentPOI;
    bool searchState;

    public AIStateID GetID()
        {
            return AIStateID.Roaming;
        }

    public void Enter(AgentController agent)
    {
        currentPOI = null;
    }

    public void Exit(AgentController agent)
    {
        
    }

    public void Update(AgentController agent)
    {
        if (searchState)
        {
            //Debug.Log("TEST");
            Vector3 direction = (UnityEngine.Random.insideUnitSphere * agent.config.roamingRange) + currentPOI.transform.position;
            if (NavMesh.SamplePosition(direction, out NavMeshHit hit, agent.config.roamingRange, NavMesh.AllAreas)) // Check if the random point is on the NavMesh
            {
                agent.navAgent.SetDestination(hit.position); // Set the new destination
            }
        }

        if (!currentPOI)
        { // If current POI is null (Get new one)
            Collider[] closePOIs = GetNearbyPOIs(agent);
            currentPOI = EvaluatePOIs(agent, closePOIs);
        }
        
        else if (Vector3.Distance(agent.transform.position, currentPOI.transform.position) <= 6f)
        { // If Agent has reached POI
            agent.StartCoroutine(OnPOIArrival());
        }
    }

    private IEnumerator OnPOIArrival() // Mini-state to search around when at a POI for a short time
    {
        searchState = true;
        //Debug.Log("TEST");

        yield return new WaitForSeconds(UnityEngine.Random.Range(10, 25));

        searchState = false;
        if (currentPOI)
        {
            currentPOI.GetComponent<POITimeCapture>().UpdateTimestamp(); // Causes errors but works
            currentPOI = null;
        }
        
    }

    private Collider[] GetNearbyPOIs(AgentController agent)
    {
        return Physics.OverlapSphere(agent.transform.position, agent.config.roamingRange, LayerMask.GetMask("POI"));
    } // get all items on POI layer

    private Collider EvaluatePOIs (AgentController agent, Collider[] colliders)
    {
        float closestDistance = float.MaxValue; // Holders to keep track of points
        Collider bestPOI = null;

        foreach (Collider collider in colliders)
        {
            Vector3 poiPos = collider.transform.position;
            float poiDistance = Vector3.Distance(agent.transform.position, poiPos);
            
            if (poiDistance < closestDistance && (Time.time - collider.GetComponent<POITimeCapture>().timestamp) > 15f)
            { // Go to closest POI if it hasn't been recently visited
                bestPOI = collider;
                closestDistance = poiDistance;
            }
        }

        if (bestPOI != null)
        {
            agent.navAgent.destination = bestPOI.transform.position; // Assign POI to navmesh agent
            return bestPOI;
        }
        else
        {
            return null;
        }
    }
}
