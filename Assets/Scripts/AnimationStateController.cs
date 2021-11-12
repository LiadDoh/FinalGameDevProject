using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public GameObject player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject child1 = player.transform.GetChild(0).transform.GetChild(0).gameObject;
        if (child1.activeSelf)
        {
            animator.SetBool("gunExist", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("isFiring", true);
        }
        else
            animator.SetBool("isFiring", false);
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            animator.SetBool("isWalking", true);
            if (Input.GetKey("left shift"))
            {
                animator.SetBool("isRunning", true);
            }
            else
                animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    public void dead()
    {
        animator.SetBool("isDead", true);
    }
}
