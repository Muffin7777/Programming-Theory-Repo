using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Chicken : Animal
{
    private float normalSpeed;

    // POLYMORPHISM
    public override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.transform.position.y < 30) {
                animalRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                anim.SetTrigger("jump");
                isOnGround = false;
            }
        }
    }

    // POLYMORPHISM
    public override void Sprint()
    {
        if (!sprinting && Input.GetKeyDown(KeyCode.LeftShift) && isOnGround)
        {
            normalSpeed = speed;
            this.speed = speed  + (speed/2);
            sprinting = true;
            StartCoroutine(SprintCountDownRoutine());
        }
    }

    IEnumerator SprintCountDownRoutine()
    {
        yield return new WaitForSeconds(2);
        this.speed = normalSpeed;
        StartCoroutine(PausingSprintRoutine());
    }

    IEnumerator PausingSprintRoutine()
    {
        yield return new WaitForSeconds(2F);
        sprinting = false;
    }
}
