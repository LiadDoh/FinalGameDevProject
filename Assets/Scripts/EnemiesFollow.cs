using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesFollow : MonoBehaviour
{
    //Transform that NPC has to follow
    public Transform transformToFollow = null;
    //NavMesh Agent variable
    NavMeshAgent agent;
    public GameObject thisEnemy;
    Animator animator;

    private bool beganSearching = false;
    private bool doneSearching = false;
    List<GameObject> remainingActivePositions;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!doneSearching)
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
                    if (thisEnemy.tag == "FirstEnemy")
                    {
                        thisEnemy.GetComponent<EnemiesFollow>().enabled = false;
                        thisEnemy.GetComponent<FirstEnemyMotion>().enabled = true;
                    }

                }
            }
            else if (beganSearching)
            {
                Debug.Log("Player has taken the enemies selected positioned weapon, switching to alternate position...");
                beganSearching = false; // Prevents repeating
                getClosestRiflePosition(remainingActivePositions);
            }
        }

    }

    public void getClosestRiflePosition(List<GameObject> activePositions)
    {
        int index = 0;
        float closestDistance = Vector3.Distance(activePositions[index].transform.position, transform.position);

        for (int i = 1; i < activePositions.Count; i++)
        {
            float tempDistance = Vector3.Distance(activePositions[i].transform.position, transform.position);
            if (closestDistance > tempDistance)
            {
                closestDistance = tempDistance;
                index = i;
            }
        }

        transformToFollow = activePositions[index].transform;
        Debug.Log(gameObject.name + " Begun heading to " + transformToFollow.name);
        activePositions.RemoveAt(index);

        remainingActivePositions = activePositions;
    }

    public void setTransformToFollow(Transform newTransform)
    {
        transformToFollow = newTransform;
    }

    public void setDoneSearching(bool boolValue)
    {
        doneSearching = boolValue;
    }


}
