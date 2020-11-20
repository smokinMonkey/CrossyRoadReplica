using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // variables
    public PlayerController pc = null;
    private Animator animator = null;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.isDead)
        {
            animator.SetBool("isDead", true);
        }
        
        if (pc.jumpStart)
        {
            animator.SetBool("isJumpStart", true);
        }

        else if(pc.isJumping)
        {
            animator.SetBool("isJumping", true);
        } else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isJumpStart", false);
        }

        // check if player controller is not jumping
        if (!pc.isIdle) return;

        // check if there is a keydown up and w rotate accordingly
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            gameObject.transform.rotation = Quaternion.Euler(270, 0, 0);
        }

        // check if there is a keydown down and s rotate accordingly
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            gameObject.transform.rotation = Quaternion.Euler(270, 180, 0);
        }
        // check if there is a keydown up and w rotate accordingly
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.rotation = Quaternion.Euler(270, -90, 0);
        }
        // check if there is a keydown up and w rotate accordingly
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.rotation = Quaternion.Euler(270, 90, 0);
        }
    }
}
