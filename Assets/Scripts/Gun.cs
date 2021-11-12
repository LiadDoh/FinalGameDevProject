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


    private float bulletForce = 5000f;

    public GameObject holder;


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
        FindObjectOfType<SoundController>().Play("GunShot");

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            spawnAndDestroyBullet(hit);
    }

    public void NPCShoot()
    {
        muzzleFlash.Play();
        FindObjectOfType<SoundController>().Play("GunShot");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, holder.transform.forward, out hit, range))
            spawnAndDestroyBullet(hit);

    }


    public void spawnAndDestroyBullet(RaycastHit hit)
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        // newBullet.transform.position = hit.point;
        Rigidbody tempRigidBodyBullet = newBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletForce);

        Debug.Log(hit.transform.name);


        Target target = hit.transform.GetComponent<Target>();
        if (target != null)
        {
            Debug.Log(target + " took damage");
            target.TakeDamage(damage);
        }

        if (newBullet != null)
        {
            Destroy(newBullet, 2);
        }
    }
}