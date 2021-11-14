using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorMotion : MonoBehaviour
{
    public GameObject door;
    private Animator animator;
    // private AudioSource doorOpening;
    private float time = 0;
    private bool doorIsOpen = true;
    NavMeshObstacle obstacle;

    AudioSource aSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        obstacle = GetComponent<NavMeshObstacle>();
        aSource = door.GetComponent<AudioSource>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (Time.time - time > 1 || time == 0)
        {
            setDoorMotion(true);
        }
        else
        {
            yield return new WaitForSeconds(1);
            setDoorMotion(true);
        }
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
        if (door.tag == "Door")
        {
            AudioSource.PlayClipAtPoint(aSource.clip, door.transform.position);
        }
        if (door.tag == "SlidingDoor")
        {
            if (!aSource.isPlaying)
            {
                PlayForTime(0.5f);
            }
        }
        time = Time.time;
        doorIsOpen = isOpen;
    }

    public void PlayForTime(float time)
    {
        AudioSource.PlayClipAtPoint(aSource.clip, door.transform.position);
        Invoke("StopAudio", time);
    }

    private void StopAudio()
    {
        aSource.Stop();
    }
}
