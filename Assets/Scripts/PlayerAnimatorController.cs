using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator animator;
    int iswalkinghash;
    int isrunninghash;
    int islefthash;
    int isrighthash;
    int isbackhash;
    int iscrouchidlehash;
    int iscrouchwalkinghash;
 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        iswalkinghash = Animator.StringToHash("walking");
        isrunninghash = Animator.StringToHash("running");
        islefthash = Animator.StringToHash("walkstrafeleft");
        isrighthash = Animator.StringToHash("walkstraferight");
        isbackhash = Animator.StringToHash("walkingbackwards");
        iscrouchidlehash = Animator.StringToHash("crouch idle");
        iscrouchwalkinghash = Animator.StringToHash("crouchwalking");
 
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardsPressed = Input.GetKey(KeyCode.W);
        bool runningPressed = Input.GetKey("left shift");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");
        bool crouchPressed = Input.GetKey(KeyCode.LeftControl);


        if (forwardsPressed)
        {
            animator.SetBool(iswalkinghash, true);
        }
        if (!forwardsPressed)
        {
            animator.SetBool(iswalkinghash, false);
            animator.SetBool(isrunninghash, false);
            animator.SetBool(iscrouchwalkinghash, false);
        }

        if (forwardsPressed && runningPressed)
        {
            animator.SetBool(iswalkinghash, false);
            animator.SetBool(isrunninghash, true);
        }
 

        if (leftPressed)
        {
            animator.SetBool(islefthash, true);
        }
        if (!leftPressed)
        {
            animator.SetBool(islefthash, false);
        }

        if (rightPressed)
        {
            animator.SetBool(isrighthash, true);
        }
        if (!rightPressed)
        {
            animator.SetBool(isrighthash, false);
        }

        if (backPressed)
        {
            animator.SetBool(isbackhash, true);
        }
        if (!backPressed)
        {
            animator.SetBool(isbackhash, false);
        }

        if (crouchPressed)
        {
            animator.SetBool(iscrouchidlehash, true);
            if (forwardsPressed)
            {
                animator.SetBool(iscrouchwalkinghash, true);
            }
        }
        if (!crouchPressed)
        {
            animator.SetBool(iscrouchidlehash, false);
        }

 
    }
}
