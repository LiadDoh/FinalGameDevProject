using System;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    public bool isPlayer = false;

    public GameObject bullet;
    public float impactForce = 0.1f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            PlayerShoot();
        }
        if (!isPlayer)
        {
            NPCShoot();
        }
    }

    private void PlayerShoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {

                Debug.Log(hit.transform.name);
                spawnAndDestroyBullet(hit);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
    }

    private void NPCShoot()
    {

    }

    private void spawnAndDestroyBullet(RaycastHit hit)
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.transform.position = hit.point;

        if (newBullet != null)
        {
            Destroy(newBullet, 2);
        }
    }
}