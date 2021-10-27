using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public GameObject explosion;
    private GameObject tempGrenade;

    public float delay = 3;

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
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2);
        //tempGrenade.SetActive(false);
        explosion.transform.position = tempGrenade.transform.position;
        Destroy(tempGrenade);
        
        explosion.SetActive(true);
        yield return new WaitForSeconds(1);
        explosion.SetActive(false);
        canExpload = true;
    }
}
