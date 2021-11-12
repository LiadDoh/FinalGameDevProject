using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public GameObject explosion;
    private GameObject tempGrenade;

    public float delay = 3f;
    public float radius = 200f;
    public float explosionForce = 500000f;
    public float damage = 100f;

    Boolean canExpload;

    // Start is called before the first frame update
    void Start()
    {
        canExpload = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && canExpload)
        {
            canExpload = false;
            tempGrenade = GameObject.Instantiate(grenade);
            //grenade.SetActive(true);
            float x, y, z;
            x = transform.forward.x*10;
            z = transform.forward.z*10;
            y = 6;
            tempGrenade.transform.position = this.transform.position;
            Rigidbody rb = tempGrenade.GetComponent<Rigidbody>();
            rb.AddForce(x,y,z, ForceMode.Impulse);
            rb.useGravity = true;
            FindObjectOfType<SoundController>().Play("Throw");
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<SoundController>().Play("Grenade");
        explosion.transform.position = new Vector3(tempGrenade.transform.position.x, tempGrenade.transform.position.y + 4,
            tempGrenade.transform.position.z);
        Destroy(tempGrenade);

        Collider[] colliders = Physics.OverlapSphere(tempGrenade.transform.position, radius);

        foreach (Collider nearbyObject in colliders){
            Debug.Log(nearbyObject.name);
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, tempGrenade.transform.position, radius);
                Target target = nearbyObject.transform.GetComponent<Target>();
                if (target != null)
                {
                    Debug.Log("Object : " + nearbyObject.name + " takes " + damage + " damage!");
                    target.TakeDamage(damage);
                }
            }

        }

        explosion.SetActive(true);
        yield return new WaitForSeconds(1);
        explosion.SetActive(false);
        canExpload = true;
    }
}
