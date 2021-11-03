using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstEnemyMotion : MonoBehaviour
{
    //Transform that NPC has to follow
    public Transform transformToFollow;
    //NavMesh Agent variable
    NavMeshAgent agent;
    Animator animator;
    public GameObject enemyOne;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Follow the player
        agent.destination = transformToFollow.position;
        if (!agent.isStopped)
        {
            animator.SetBool("isMoving", true);
        }
        if (Vector3.Distance(agent.destination, enemyOne.transform.position) < 0.1)
        {
            animator.SetBool("isMoving", false);
        }
    }
}
