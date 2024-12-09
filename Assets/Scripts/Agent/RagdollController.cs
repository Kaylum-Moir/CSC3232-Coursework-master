using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        Disable();
    }

    public void Enable()
    {
        animator.enabled = false;
        foreach(var rigidbody in rigidBodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    public void Disable()
    {
        animator.enabled = true;
        foreach(var rigidbody in rigidBodies)
        {
            //Debug.Log(rigidbody.transform.name);
            rigidbody.isKinematic = true;
        }
    }
}
