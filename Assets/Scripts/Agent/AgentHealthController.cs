using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealthController : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    private RagdollController ragdollController;

    [SerializeField]
    private float currentHealth;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyData.maxHealth;
        ragdollController = GetComponent<RagdollController>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.healthController = this;
        }
    }

    public void ApplyDamage(float damagePoints)
    {
        GetComponent<AgentController>().stateMachine.ChangeState(AIStateID.Chasing); // Alerted to the player
        currentHealth -= damagePoints;
        // Do something to visualise taking hit
        Debug.Log("Agent took "+damagePoints+"hp");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<AgentController>().stateMachine.ChangeState(AIStateID.Dead);
        ragdollController.Enable();
        foreach (var rigidBody in GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.transform.gameObject.layer = 7;
        }
    }
}
