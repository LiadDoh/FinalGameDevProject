using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE
}

public class EnemiesFollow : MonoBehaviour
{
    private Gun gun;
    private EnemyState currentState;
    //Transform that NPC has to follow
    public Transform transformToFollow = null;
    // public float impactForce = 0.1f;
    // public float fireRate = 15f;
    // private float range = 10f;
    // private float damage = 10f;
    // private float nextTimeToFire = 0f;
    //NavMesh Agent variable
    NavMeshAgent agent;
    public GameObject thisEnemy;
    Animator animator;

    private bool beganSearching = false;
    List<GameObject> remainingActivePositions;

    public GameObject[] remainingActiveEnemies;
    public GameObject target;
    private int nextState = 0;

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
        if (currentState == EnemyState.PATROL)
        {
            patrol();
        }
        if (currentState == EnemyState.CHASE)
        {
            chase();
        }
    }

    void patrol()
    {
        if (transformToFollow != null)
        {
            beganSearching = true;
            agent.destination = transformToFollow.position;
            //Follow the game object

            if (Vector3.Distance(agent.destination, thisEnemy.transform.position) > agent.stoppingDistance)
                agent.isStopped = false;

            else
                agent.isStopped = true;


            if (!agent.isStopped)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);
        }
        else if (beganSearching)
        {
            Debug.Log("Player has taken the enemies selected positioned weapon, switching to alternate position...");
            beganSearching = false; // Prevents repeating
            getClosestRiflePosition(remainingActivePositions);
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

    void chase()
    {
        target = remainingActiveEnemies[nextState];
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(target.transform.position, transform.position) < 20f)
        {
            if (transform.tag.Equals("SecondEnemy"))
                Debug.Log("Second Enemy has reached the player");
            agent.isStopped = true;
            animator.SetBool("isMoving", false);
            transform.LookAt(target.transform);
            gun.NPCShoot();
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("isMoving", true);
        }

    }


    public void setTransformToFollow(Transform newTransform)
    {
        transformToFollow = newTransform;
    }

    public Transform GetTransformToFollow()
    {
        return transformToFollow;
    }

    public void SetStateToChase(bool boolValue)
    {
        Debug.Log("Enemy" + gameObject.name + " is now chasing the player");
        currentState = EnemyState.CHASE;
        gun = gameObject.GetComponentInChildren<Gun>();
    }

    public string getState()
    {
        Debug.Log(currentState);
        return currentState.ToString();
    }

    public void setAgentStoppingDistance(float d)
    {
        agent.stoppingDistance = d;
    }

    public void stopAgent()
    {
        agent.isStopped = true;
    }

}

