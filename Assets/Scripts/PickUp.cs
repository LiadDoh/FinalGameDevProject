using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public AudioSource itemPicked;

    float throwForce = 600f;
    private Vector3 objectPos;
    float distance;
    public float distanceAllowed = 2f;

    public bool canHold = true;
    public GameObject item;
    private GameObject tempParent;
    private Text text;
    public bool isHolding = false;
    private float timeAfterRelease = 0;
    private bool distanceFlag = false;
    private Transform cam;


    private void Start()
    {
        tempParent = GameObject.Find("Dest");

        text = GameObject.Find("Interactions").GetComponent<Text>();

       cam = Camera.main.transform;
    }

    private void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hit);

        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if (distance > distanceAllowed)
            isHolding = false;
        
        if (isHolding)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);
            text.text = "Release left mouse button to drop item \nPress right mouse button to throw item";

            if (Input.GetMouseButtonDown(1))
            {
                item.GetComponent<Rigidbody>().AddForce(cam.forward * throwForce);
                setDrop();
            }
        }
        else
        {
            if (distanceFlag && (distance > distanceAllowed || hit.collider == null || !hit.collider.CompareTag("Carriable")))
            {
                text.text = "";
                distanceFlag = false;
            }
            else if ((timeAfterRelease == 0 || Time.time - timeAfterRelease > 0.5) && distance <= distanceAllowed
               && hit.collider != null && hit.collider.CompareTag("Carriable") &&
               !text.text.Equals("Release left mouse button to drop item \nPress right mouse button to throw item"))
            {
                text.text = "Press left mouse button to interact";
                distanceFlag = true;
            }
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    private void OnMouseDown()
    {
        if (distance <= distanceAllowed)
        {
            if (!isHolding && itemPicked != null)
                itemPicked.Play();
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
            item.transform.SetPositionAndRotation(tempParent.transform.position, tempParent.transform.rotation);
        }
    }
    private void OnMouseUp()
    {
        setDrop();
    }

    private void setDrop()
    {
        isHolding = false;
        text.text = "";
        timeAfterRelease = Time.time;
    }
}
