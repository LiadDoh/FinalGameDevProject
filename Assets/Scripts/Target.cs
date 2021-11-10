using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;

    public bool isPlayer = false;

    private PlayerUIControl playerUIControl = null;

    private EnemiesFollow enemiesFollow = null;
    void Start()
    {
        if (isPlayer)
        {
            playerUIControl = GetComponent<PlayerUIControl>();
        }
        else if (gameObject.tag.Equals("FirstEnemy"))
        {
            enemiesFollow = GameObject.FindGameObjectWithTag("SecondEnemy").GetComponent<EnemiesFollow>();
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
        if (isPlayer)
        {

        }
        else if (gameObject != null)
        {
            if (enemiesFollow != null && enemiesFollow.getState().Equals("PATROL") && enemiesFollow.isActiveAndEnabled)
            {
                enemiesFollow.transformToFollow = gameObject.GetComponent<EnemiesFollow>().GetTransformToFollow();
                enemiesFollow.setAgentStoppingDistance(0);
                Debug.Log("First enemy has died, Secnond enemy began heading to " + enemiesFollow.transformToFollow.name);
            }
            gameObject.SetActive(false);
        }
    }
}
