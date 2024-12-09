using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    public AgentHealthController healthController;

    public void OnRaycastHit(float damage, Vector3 rayDirection)
    {
        healthController.ApplyDamage(damage);
    }
}
