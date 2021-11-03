using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesFollow : MonoBehaviour
{
    //Transform that NPC has to follow
    public Transform transformToFollow;
    //NavMesh Agent variable
    NavMeshAgent agent;
    public GameObject thisEnemy;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Follow the game object
        agent.destination = transformToFollow.position;
        if (!agent.isStopped)
        {
            animator.SetBool("isMoving", true);
        }
        if (Vector3.Distance(agent.destination, thisEnemy.transform.position) < 1)
        {
            animator.SetBool("isMoving", false);
            if (thisEnemy.tag == "FirstEnemy")
            {
                thisEnemy.GetComponent<EnemiesFollow>().enabled = false;
                thisEnemy.GetComponent<FirstEnemyMotion>().enabled = true;
            }

        }
    }
}
