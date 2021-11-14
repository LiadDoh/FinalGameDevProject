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

    private ThrowGrenade throwGrenade;
    private EnemyState currentState;
    //Transform that NPC has to follow
    public Transform transformToFollow = null;
    NavMeshAgent agent;
    public GameObject thisEnemy;
    Animator animator;

    private bool beganSearching = false;
    List<GameObject> remainingActivePositions;

    public GameObject[] remainingActiveEnemies;
    public GameObject target;
    private int nextState = 0;


    private float cooldown = 1f;
    private float cooldownTimer = 1f;

    public float range = 200f;

    private int rifleToGrenadeRatio = 5;

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
        bool isNotCloseToEnemies = true;
        foreach (GameObject enemy in remainingActiveEnemies)
        {
            GameObject tempEnemy = enemy;
            if (tempEnemy.tag.Equals("Player"))
            {
                tempEnemy = enemy.transform.Find("remy").gameObject;
            }
            Target temp = enemy.GetComponent<Target>();
            bool isEnemyAlive = temp.isAlive();

            NavMeshHit hit;
            if (Vector3.Distance(tempEnemy.transform.position, transform.position) < 20f && isEnemyAlive
            && (!agent.Raycast(tempEnemy.transform.position, out hit) && Vector3.Distance(tempEnemy.transform.position, transform.position) < 10f))
            {
                transform.LookAt(tempEnemy.transform);
                agent.isStopped = true;
                animator.SetBool("isMoving", false);
                attack();
                isNotCloseToEnemies = false;
                break;
            }

        }
        if (isNotCloseToEnemies)
        {
            agent.isStopped = false;
            animator.SetBool("isMoving", true);
        }
    }

    private void attack()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0)
            return;

        cooldownTimer = cooldown;

        if (Random.Range(0, rifleToGrenadeRatio) == rifleToGrenadeRatio - 1 && throwGrenade != null && throwGrenade.getCanExpload())
        {
            throwGrenade.throwGrenade();
        }
        else
        {
            gun.NPCShoot();
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

    public void SetStateToChase()
    {
        Debug.Log("Enemy" + gameObject.name + " is now chasing the player");
        currentState = EnemyState.CHASE;
        gun = gameObject.GetComponentInChildren<Gun>();
        throwGrenade = gameObject.GetComponent<ThrowGrenade>();
        throwGrenade.setCanExpload(true);
    }

    public string getState()
    {
        Debug.Log(currentState);
        return currentState.ToString();
    }

    public void setAgentStoppingDistance(float d)
    {
        if (agent != null)
            agent.stoppingDistance = d;
    }

    public void stopAgent()
    {
        if (agent != null)
            agent.isStopped = true;
    }

}

