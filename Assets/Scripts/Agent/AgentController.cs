using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateID initialState;
    public NavMeshAgent navAgent;
    public EnemyData config;

    public Transform player;

    Animator animator;

    private float pingTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasingState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.RegisterState(new AIRoamingState());
        stateMachine.ChangeState(AIStateID.Roaming);
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        if (stateMachine == null)
        {
            Debug.LogError("StateMachine is null in AgentController!");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        stateMachine.UpdateState();
        pingTimer -= Time.deltaTime;
        if (pingTimer < 0.0f)
        {
            animator.SetFloat("Speed", navAgent.velocity.magnitude);
            pingTimer = 1.0f;
        }
    }

    void OnAnimatorMove() 
    {
        transform.position = navAgent.nextPosition;
    }
}
