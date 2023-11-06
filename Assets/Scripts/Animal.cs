using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private Rigidbody animalRigidbody;
    public bool isOnGround = true;
    public float jumpForce;
    public float gravityModifier;
    public float speed;
    private float forwardInput;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        animalRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
    }

    public void Walk()
    {
        forwardInput = Input.GetAxis("Horizontal");
        transform.Translate(Time.deltaTime * speed * forwardInput * Vector3.forward);
        
        if (forwardInput != 0 && isOnGround) {
            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            animalRigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            anim.SetTrigger("jump");
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
