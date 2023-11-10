using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Animal
{
    public float secondJumpForce;
    private bool secondJump = false;
    private bool released = true;

    public override void Walk()
    {
        bool walked = Input.GetKeyDown(KeyCode.RightArrow);
        if (!released)
        {
            released = Input.GetKeyUp(KeyCode.RightArrow);
        }
        if (walked && released) {

            animalRigidbody.AddForce(Vector3.forward * speed, ForceMode.Impulse);
            released = false;
        }
        if (walked && isOnGround)
        {
            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
        }
    }

    public override void Jump()
    {
        float forceForThisJump = jumpForce;
        if (Input.GetKeyDown(KeyCode.Space) && !secondJump)
        {
            if (!isOnGround)
            {
                secondJump = true;
                forceForThisJump = secondJumpForce;
            }
            animalRigidbody.AddForce(Vector3.up * forceForThisJump, ForceMode.Impulse);
            anim.SetTrigger("jump");
            if (!isOnGround)
            {
                secondJump = true;
            }
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
        secondJump = false;
    }
}

