using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    private float normalSpeed;


    public override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animalRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("jump");
            isOnGround = false;
        }
    }

    public override void Sprint()
    {
        if (!sprinting && Input.GetKeyDown(KeyCode.LeftShift) && isOnGround)
        {
            normalSpeed = speed;
            this.speed = speed * 2;
            sprinting = true;
            StartCoroutine(SprintCountDownRoutine());
        }
    }

    IEnumerator SprintCountDownRoutine()
    {
        yield return new WaitForSeconds(1);
        this.speed = normalSpeed;
        sprinting = false;
    }
}
