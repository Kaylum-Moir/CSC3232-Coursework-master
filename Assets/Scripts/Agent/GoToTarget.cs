using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTarget : MonoBehaviour
{
    [SerializeField]
    GameObject destination;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = destination.transform.position;
    }
}
