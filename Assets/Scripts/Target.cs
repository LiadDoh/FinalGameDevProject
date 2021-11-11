using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;

    public bool isPlayer = false;

    private PlayerUIControl playerUIControl = null;

    private EnemiesFollow selfEnemiesFollow = null;
    private EnemiesFollow enemiesFollow = null;

    private SC_NPCFollow selfNPCFollow = null;

    //private AnimationStateController animationStateController = null;
    void Start()
    {
        if (isPlayer)
        {
            playerUIControl = GetComponent<PlayerUIControl>();
        }
        else if (gameObject.tag.Equals("FirstEnemy") || gameObject.tag.Equals("SecondEnemy"))
        {
            if (gameObject.tag.Equals("FirstEnemy"))
                enemiesFollow = GameObject.FindGameObjectWithTag("SecondEnemy").GetComponent<EnemiesFollow>();
            selfEnemiesFollow = GetComponent<EnemiesFollow>();
        }
        else if (gameObject.tag.Equals("NPC"))
        {
            selfNPCFollow = GetComponent<SC_NPCFollow>();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (playerUIControl != null)
        {
            playerUIControl.UpdateHealth(health);
        }
        Debug.Log("Health of " + gameObject.name + " is " + health);
        if (health <= 0f)
            Die();
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died");
        if (isPlayer)
        {
            //animationStateController = GameObject.FindGameObjectWithTag("remy").GetComponent<AnimationStateController>();
            //animationStateController.dead();
        }
        else if (gameObject != null)
        {
            if (enemiesFollow != null && enemiesFollow.getState().Equals("PATROL") && enemiesFollow.enabled)
            {
                enemiesFollow.transformToFollow = gameObject.GetComponent<EnemiesFollow>().GetTransformToFollow();
                enemiesFollow.setAgentStoppingDistance(0);
                Debug.Log("First enemy has died, Secnond enemy began heading to " + enemiesFollow.transformToFollow.name);
            }
            if (selfEnemiesFollow != null)
            {
                selfEnemiesFollow.stopAgent();
                selfEnemiesFollow.enabled = false;
                Debug.Log("Enemy ded");
            }
            else if (selfNPCFollow != null)
            {
                selfNPCFollow.stopAgent();
                selfNPCFollow.enabled = false;
                Debug.Log("NPC ded");
            }

            gameObject.GetComponent<Animator>().SetBool("isDead", true);

        }
    }
}
