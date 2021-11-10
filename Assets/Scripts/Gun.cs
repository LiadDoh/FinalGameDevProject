using System;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

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
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);
            bullet.transform.position = hit.point;
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

    private IEnumerator spawnBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

        yield return new WaitForSeconds(2);

        Destroy(newBullet);
    }
}
