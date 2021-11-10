using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;

    public bool isPlayer = false;

    private PlayerUIControl playerUIControl = null;
    void Start()
    {
        if (isPlayer)
        {
            playerUIControl = GetComponent<PlayerUIControl>();
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
        Destroy(gameObject);
    }
}
