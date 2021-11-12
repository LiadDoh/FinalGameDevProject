using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_NPCFollow : MonoBehaviour
{


    public enum FriendlyNPCState
    {
        PATROL,
        CHASE
    }
    //public float walk_Speed = 0.5f;

    private Gun gun;

    private ThrowGrenade throwGrenade;
    public NavMeshAgent navAgent;
    private Animator animator;
    private Transform target;

    private FriendlyNPCState currentState;

    public GameObject[] enemies;

    private float cooldown = 1f;
    private float cooldownTimer = 1f;

    private int rifleToGrenadeRatio = 5;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = FriendlyNPCState.PATROL;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Target>().isAlive())
        {
            if (currentState == FriendlyNPCState.PATROL)
            {
                patrol();
            }
            if (currentState == FriendlyNPCState.CHASE)
            {
                chase();
            }
        }
    }

    void patrol()
    {
        //Follow the player
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

    void chase()
    {
        navAgent.SetDestination(target.transform.position);
        bool isNotCloseToEnemies = true;
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < 20f && enemy.GetComponent<Target>().isAlive())
            {
                navAgent.isStopped = true;
                animator.SetBool("isMoving", false);
                transform.LookAt(enemy.transform);
                attack();
                isNotCloseToEnemies = false;
            }
        }
        if (isNotCloseToEnemies)
        {
            navAgent.isStopped = false;
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

    public void stopAgent()
    {
        navAgent.isStopped = true;
    }

    public void SetStateToChase()
    {
        Debug.Log("Enemy" + gameObject.name + " is now chasing the player");
        currentState = FriendlyNPCState.CHASE;
        gun = gameObject.GetComponentInChildren<Gun>();
        throwGrenade = gameObject.GetComponent<ThrowGrenade>();
        throwGrenade.setCanExpload(true);
    }
}
