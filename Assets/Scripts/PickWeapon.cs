using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickWeapon : MonoBehaviour
{
    private GameObject playerObject;
    public GameObject firstEnemyObject;
    public GameObject SecondEnemyObject;
    public GameObject friendlyNPCObject;
    private Transform cam;

    float distance;
    public float distanceAllowed = 2f;

    public GameObject rifle1;
    public GameObject rifle2;
    public GameObject rifle3;
    public GameObject rifle4;

    private PlayerUIControl playerUIControl;

    string temp;
    bool wasPlayerClose = false;
    private RaycastHit hit;
    //bool hasPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("remy");

        cam = Camera.main.transform;

        playerUIControl = GameObject.FindObjectOfType<PlayerUIControl>();

        temp = playerUIControl.getObjectiveText();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out hit);

        //distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (hit.transform != null && hit.transform.gameObject != null && hit.transform.gameObject == gameObject && hit.distance < distanceAllowed
            && !rifle1.activeInHierarchy && !rifle2.activeInHierarchy)
        {
            playerUIControl.setObjectiveText("Press left click while looking at the rifle to pick it up");
            wasPlayerClose = true;
        }
        else if (!rifle1.activeInHierarchy && !rifle2.activeInHierarchy && !playerUIControl.getObjectiveText().Equals(temp)
          && wasPlayerClose)
        {
            playerUIControl.setObjectiveText(temp);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider name : " + other.name);
        if (other.tag.Equals(firstEnemyObject.tag) || other.tag.Equals(SecondEnemyObject.tag) && !rifle3.activeInHierarchy && !rifle4.activeInHierarchy)
        {
            Debug.Log(other.tag);
            gameObject.SetActive(false);
            rifle3.SetActive(true);
            rifle4.SetActive(true);
            playerUIControl.setStateText("Careful now, the enemies got themselves some weapons!!");
            GetComponent<PickWeapon>().enabled = false;
            firstEnemyObject.GetComponent<EnemiesFollow>().SetStateToChase();
            SecondEnemyObject.GetComponent<EnemiesFollow>().SetStateToChase();
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
            cam.GetComponent<ThrowGrenade>().setCanExpload(true);
            cam.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 0.3f);
            playerUIControl.setObjectiveText("You found a weapon!\nNow get to killing the enemy team!");
            friendlyNPCObject.GetComponent<SC_NPCFollow>().SetStateToChase();
            // Debug.Log(firstEnemyObject.GetComponent<EnemiesFollow>().transformToFollow.name + "  VS  " + gameObject.transform.name);
            if (firstEnemyObject.GetComponent<EnemiesFollow>().transformToFollow == gameObject.transform)
            {
                Debug.Log("Yese");
                firstEnemyObject.GetComponent<EnemiesFollow>().setTransformToFollow(null);
            }
            GetComponent<PickWeapon>().enabled = false;
            Destroy(gameObject);
        }
    }

}
