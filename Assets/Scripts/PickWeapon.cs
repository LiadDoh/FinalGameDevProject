using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour
{
    private GameObject tempParent;
    private Transform cam;

    float distance;
    public float distanceAllowed = 2f;

    public GameObject rifle1;
    public GameObject rifle2;
    public GameObject rifle3;
    public GameObject rifle4;

    // Start is called before the first frame update
    void Start()
    {
        tempParent = GameObject.Find("remy");

        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hit);

        distance = Vector3.Distance(transform.position, tempParent.transform.position);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirstEnemy" || other.tag == "SecondEnemy")
        {
            Debug.Log(other.tag);
            gameObject.SetActive(false);
            rifle3.SetActive(true);
            rifle4.SetActive(true);
            GetComponent<PickWeapon>().enabled = false;
        }

    }

    private void OnMouseDown()
    {
        if (distance <= distanceAllowed)
        {
            gameObject.SetActive(false);
            rifle1.SetActive(true);
            rifle2.SetActive(true);
            GetComponent<PickWeapon>().enabled = false;
        }
    }
}
