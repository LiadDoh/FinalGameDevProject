using System.Collections;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public GameObject explosion;

    public float delay = 3f;
    public float radius = 5;
    public float damage = 40f;

    private bool canExpload = false;


    public void throwGrenade()
    {
        if (canExpload)
        {
            canExpload = false;
            GameObject tempGrenade = GameObject.Instantiate(grenade);
            float x, y, z;
            x = transform.forward.x * 10;
            z = transform.forward.z * 10;
            y = 6;
            tempGrenade.transform.position = this.transform.position;
            Rigidbody rb = tempGrenade.GetComponent<Rigidbody>();
            rb.AddForce(x, y, z, ForceMode.Impulse);
            rb.useGravity = true;
            FindObjectOfType<SoundController>().Play("Throw");
            StartCoroutine(Explode(tempGrenade));
        }
    }

    IEnumerator Explode(GameObject tempGrenade)
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<SoundController>().Play("Grenade");
        GameObject tempExplosion = GameObject.Instantiate(explosion);
        tempExplosion.transform.position = new Vector3(tempGrenade.transform.position.x, tempGrenade.transform.position.y + 4,
            tempGrenade.transform.position.z);
        Destroy(tempGrenade);

        Collider[] colliders = Physics.OverlapSphere(tempGrenade.transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Debug.Log(nearbyObject.name);
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Target target = nearbyObject.transform.GetComponent<Target>();
                if (target != null)
                {
                    Debug.Log("Object : " + nearbyObject.name + " takes " + damage + " damage!");
                    target.TakeDamage(damage);
                }
            }

        }

        tempExplosion.SetActive(true);
        yield return new WaitForSeconds(1);
        tempExplosion.SetActive(false);
        canExpload = true;
    }

    public bool getCanExpload()
    {
        return canExpload;
    }

    public void setCanExpload(bool canExpload)
    {
        this.canExpload = canExpload;
    }

}
