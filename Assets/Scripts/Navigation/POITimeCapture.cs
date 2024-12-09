using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POITimeCapture : MonoBehaviour
{
    public float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        timestamp = Time.time;
    }

    public void UpdateTimestamp()
    {
        timestamp = Time.time;
    }

    /*
        Timestamp stores the last time this POI was checked
        Agents will roam between POIs to look for the player
        Agents will prioritise older timestamps at closer distances

        Agents will 'score' nearby POIs in their roaming radius
        score = timestampBiasWeight * (current time - timestamp) - distanceWeight * poiDistance

        Best score gets visited

        (Logic for this will be in AiRoamingState.cs)
    */
}
