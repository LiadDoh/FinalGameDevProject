using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{

    public float damage = 10f;

    public void on(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        Target target = collision.transform.GetComponent<Target>();
        if (target != null)
        {
            Debug.Log(target + " took damage");
            target.TakeDamage(damage);
        }

        Destroy(gameObject, 2);
    }
}
