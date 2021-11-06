using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text objective;
    public Text state;

    string temp;
    //bool hasPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        tempParent = GameObject.Find("remy");

        cam = Camera.main.transform;

        temp = objective.text;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hit);

        distance = Vector3.Distance(transform.position, tempParent.transform.position);
        if (distance <= distanceAllowed && !rifle1.activeInHierarchy && !rifle2.activeInHierarchy)
        {
            objective.text = "Press left click to pick up";
        } else if (!rifle1.activeInHierarchy && !rifle2.activeInHierarchy)
        {
            objective.text = temp;
        } 
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirstEnemy" || other.tag == "SecondEnemy" && !rifle3.activeInHierarchy && !rifle4.activeInHierarchy)
        {
            Debug.Log(other.tag);
            gameObject.SetActive(false);
            rifle3.SetActive(true);
            rifle4.SetActive(true);
            state.text = "Careful now, the enemies got themselves some weapons!!";
            yield return new WaitForSeconds(2);
            state.text = "";
            GetComponent<PickWeapon>().enabled = false;
        }

    }

    private void OnMouseDown()
    {
        if (distance <= distanceAllowed && !rifle1.activeInHierarchy && !rifle2.activeInHierarchy)
        {
            gameObject.SetActive(false);
            rifle1.SetActive(true);
            rifle2.SetActive(true);
            objective.text = "You found a weapon!\nNow get to killing the enemy team!";
            GetComponent<PickWeapon>().enabled = false;
        }
    }

}
