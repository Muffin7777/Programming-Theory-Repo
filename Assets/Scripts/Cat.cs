using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Cat : Animal
{

    // POLYMORPHISM
    public override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            animalRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("jump");
            isOnGround = false;
        }
    }

    // POLYMORPHISM
    public override void Sprint()
    {
        if (!sprinting && Input.GetKeyDown(KeyCode.LeftShift) && isOnGround)
        {
            this.speed = speed * 2;
            sprinting = true;
            StartCoroutine(SprintCountDownRoutine());
        }
    }

    IEnumerator SprintCountDownRoutine()
    {
        yield return new WaitForSeconds(2);
        this.speed = normalSpeed + (normalSpeed/2);
        StartCoroutine(SprintSlowDownRoutine());
    }

    IEnumerator SprintSlowDownRoutine()
    {
        yield return new WaitForSeconds(1F);
        this.speed = normalSpeed;
        StartCoroutine(PausingSprintRoutine());
    }

    IEnumerator PausingSprintRoutine()
    {
        yield return new WaitForSeconds(2F);
        sprinting = false;
    }

}