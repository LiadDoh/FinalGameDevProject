using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_NPCFollow : MonoBehaviour
{

    //public float walk_Speed = 0.5f;
    public float run_Speed = 4f;
    public NavMeshAgent navAgent;
    private Animator animator;
    private Transform target;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Follow the player
        navAgent.speed = run_Speed;
        navAgent.SetDestination(target.position);
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void stopAgent()
    {
        navAgent.isStopped = true;
    }
}
