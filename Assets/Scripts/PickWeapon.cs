using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickWeapon : MonoBehaviour
{
    private GameObject playerObject;
   // private GameObject firstEnemyObject;
    //private GameObject SecondEnemyObject;
    private Transform cam;

    float distance;
    public float distanceAllowed = 2f;

    public GameObject rifle1;
    public GameObject rifle2;
    public GameObject rifle3;
    public GameObject rifle4;

    public PlayerUIControl playerUIControl;

    string temp;
    bool wasPlayerClose = false;
    private RaycastHit hit;
    //bool hasPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("remy");

        //firstEnemyObject = GameObject.Find("FirstEnemy");

        //SecondEnemyObject = GameObject.Find("SecondEnemy");

        cam = Camera.main.transform;

        playerUIControl = GameObject.FindObjectOfType<PlayerUIControl>();

        temp = playerUIControl.getObjectiveText();

        Debug.Log("getObjectiveText = " + temp);

    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out hit);

        //distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (hit.transform != null && hit.transform.gameObject != null && hit.distance < distanceAllowed 
            && !rifle1.activeInHierarchy && !rifle2.activeInHierarchy)
        {
            playerUIControl.setObjectiveText("Press left click while looking at the rifle to pick it up");
            wasPlayerClose = true;
        } else if (!rifle1.activeInHierarchy && !rifle2.activeInHierarchy && !playerUIControl.getObjectiveText().Equals(temp) 
            && wasPlayerClose)
        {
            playerUIControl.setObjectiveText(temp);
        }

      /*  if ((Vector3.Distance(transform.position, firstEnemyObject.transform.position) <= distanceAllowed
            || Vector3.Distance(transform.position, SecondEnemyObject.transform.position) <= distanceAllowed)
            && !rifle3.activeInHierarchy && !rifle4.activeInHierarchy)
        {
            Debug.Log(firstEnemyObject.tag);
            gameObject.SetActive(false);
            rifle3.SetActive(true);
            rifle4.SetActive(true);
            playerUIControl.setStateText("Careful now, the enemies got themselves some weapons!!");
            GetComponent<PickWeapon>().enabled = false;

            if (!rifle1.activeInHierarchy && !rifle2.activeInHierarchy && !playerUIControl.getObjectiveText().Equals(temp))
            {
                playerUIControl.setObjectiveText(temp);
            }

            Destroy(gameObject);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirstEnemy" || other.tag == "SecondEnemy" && !rifle3.activeInHierarchy && !rifle4.activeInHierarchy)
        {
            Debug.Log(other.tag);
            gameObject.SetActive(false);
            rifle3.SetActive(true);
            rifle4.SetActive(true);
            playerUIControl.setStateText("Careful now, the enemies got themselves some weapons!!");
            GetComponent<PickWeapon>().enabled = false;

            if (!rifle1.activeInHierarchy && !rifle2.activeInHierarchy && !playerUIControl.getObjectiveText().Equals(temp))
            {
                playerUIControl.setObjectiveText(temp);
            }

            Destroy(gameObject);
        }

    }

    private void OnMouseDown()
    {
        if (hit.transform != null && hit.transform.gameObject != null && hit.distance < distanceAllowed
            && !rifle1.activeInHierarchy && !rifle2.activeInHierarchy)
        {
            rifle1.SetActive(true);
            rifle2.SetActive(true);
            playerUIControl.setObjectiveText("You found a weapon!\nNow get to killing the enemy team!");
            GetComponent<PickWeapon>().enabled = false;
            Destroy(gameObject);
        }
    }

}
