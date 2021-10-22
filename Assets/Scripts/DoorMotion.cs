using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource doorOpening;
    private float time = 0;
    private bool doorIsOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        doorOpening = GetComponent<AudioSource>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (Time.time - time > 1 || time == 0)
        {
            setDoor(true);
        } else
        {
            yield return new WaitForSeconds(1);
            setDoor(true);
        }

    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        if (Time.time - time > 1 || time == 0)
        {
            setDoor(false);
        }
        else
        {
            yield return new WaitForSeconds(1);
            setDoor(false);
        }
    }



    private void setDoor(bool isOpen)
    {
        animator.SetBool("Open", isOpen);
        doorOpening.PlayDelayed(0.5f);
        time = Time.time;
        doorIsOpen = isOpen;
    }
}
