using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenClose : MonoBehaviour
{
    private Animator animator;
    private int isDoorOpen;
    public GameObject crossHair;
    public GameObject crossHairTouch;
    private Transform cam;
    private Text text;
    public float distance = 3;
    private bool canChangeCrossHair = true;
    public string variable;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        isDoorOpen = 0;
        cam = Camera.main.transform;

        text = GameObject.Find("Interactions").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hit);
        if (hit.transform != null && hit.transform.gameObject != null && hit.distance < distance)
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                idleCrossHair(false);
                canChangeCrossHair = true;
                text.text = "Press left mouse button to interact";
                if (Input.GetMouseButton(0) && (Time.time - time > 0.3 || time == 0))
                {
                    if (isDoorOpen == 0)
                    {
                        animator.SetBool(variable, true);
                        isDoorOpen = 1;                 
                    }
                    else
                    {
                        animator.SetBool(variable, false);
                        isDoorOpen = 0;
                    }
                    time = Time.time;
                }
            } else
            {
                idleCrossHair(true);
            }
        }
        else
        {
            idleCrossHair(true);
            if (text.text.Equals("Press left mouse button to interact"))
                text.text = "";
        }
    }

    private void idleCrossHair(bool b)
    {
        if (!b || canChangeCrossHair)
        {
            crossHair.SetActive(b);
            crossHairTouch.SetActive(!b);
        }
        canChangeCrossHair = false;
    }
}
