using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotion : MonoBehaviour
{
    private Animator animator;
   // private AudioSource doorOpening;
    private float time = 0;
    private bool doorIsOpen = true;

    //private Collider[] colChildren;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //colChildren = gameObject.GetComponentsInChildren<Collider>();
        // doorOpening = GetComponent<AudioSource>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (Time.time - time > 1 || time == 0)
        {
            setDoorOpen(true);
        } else
        {
            yield return new WaitForSeconds(1);
            setDoorOpen(true);
        }
        /*if(other.tag.Equals("FirstEnemy") || other.tag.Equals("SecondEnemy"))
        {
            foreach (Collider collider in colChildren)
            {
                collider.enabled = false;
            }
            yield return new WaitForSeconds(1);
            foreach (Collider collider in colChildren)
            {
                collider.enabled = true;
            }
            setDoorOpen(false);
        }*/


    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        if (Time.time - time > 1 || time == 0)
        {
            setDoorOpen(false);
        }
        else
        {
            yield return new WaitForSeconds(1);
            setDoorOpen(false);
        }

    }



    private void setDoorOpen(bool isOpen)
    {
        animator.SetBool("Open", isOpen);
      //  doorOpening.PlayDelayed(0.5f);
        time = Time.time;
        doorIsOpen = isOpen;
    }
}
