using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorMotion : MonoBehaviour
{
    private Animator animator;
   // private AudioSource doorOpening;
    private float time = 0;
    private bool doorIsOpen = true;
    NavMeshObstacle obstacle;

    //private Collider[] colChildren;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        obstacle = GetComponent<NavMeshObstacle>();
        //colChildren = gameObject.GetComponentsInChildren<Collider>();
        // doorOpening = GetComponent<AudioSource>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (Time.time - time > 1 || time == 0)
        {
            setDoorMotion(true);
        } else
        {
            yield return new WaitForSeconds(1);
            setDoorMotion(true);
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
            setDoorMotion(false);
        }
        else
        {
            yield return new WaitForSeconds(1);
            setDoorMotion(false);
        }

    }



    private void setDoorMotion(bool isOpen)
    {
        if (obstacle != null)
        {
            obstacle.enabled = !isOpen;
        }
        animator.SetBool("Open", isOpen);
      //  doorOpening.PlayDelayed(0.5f);
        time = Time.time;
        doorIsOpen = isOpen;
    }
}
