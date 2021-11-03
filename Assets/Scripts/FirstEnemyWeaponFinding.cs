using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstEnemyWeaponFinding : MonoBehaviour
{
    //Transform that NPC has to follow
    public Transform transformToFollow;
    //NavMesh Agent variable
    NavMeshAgent agent;
    public GameObject enemyOne;
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
        if (Vector3.Distance(agent.destination, enemyOne.transform.position) < 0.1)
        {
            animator.SetBool("isMoving", false);
            enemyOne.GetComponent<FirstEnemyWeaponFinding>().enabled = false;
            enemyOne.GetComponent<FirstEnemyMotion>().enabled = true;
        }
    }
}
