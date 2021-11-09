using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE
}

public class EnemiesFollow : MonoBehaviour
{
    private EnemyState currentState;
    //Transform that NPC has to follow
    public Transform transformToFollow = null;
    //NavMesh Agent variable
    NavMeshAgent agent;
    public GameObject thisEnemy;
    Animator animator;

    private bool beganSearching = false;
    List<GameObject> remainingActivePositions;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.PATROL;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(currentState == EnemyState.PATROL)
        {
            Patrol();
        }
        if(currentState == EnemyState.CHASE)
        {
            Chase();
        }
    }

    
    public void getClosestRiflePosition(List<GameObject> positions)
    {
        GameObject closestPosition = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in positions)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPosition = go;
                distance = curDistance;
            }
        }
        transformToFollow = closestPosition.transform;
    }

    void Patrol()
    {
        if (transformToFollow != null)
        {
            beganSearching = true;
            //Follow the game object
            agent.destination = transformToFollow.position;
            if (!agent.isStopped)
            {
                animator.SetBool("isMoving", true);
            }
            if (Vector3.Distance(agent.destination, thisEnemy.transform.position) <= agent.stoppingDistance)
            {
                animator.SetBool("isMoving", false);
            }
        } else if (beganSearching)
        {
            Debug.Log("Player has taken the enemies selected positioned weapon, switching to alternate position...");
            beganSearching = false; // Prevents repeating
            getClosestRiflePosition(remainingActivePositions);
        }
    }

    void Chase()
    {

    }
}
