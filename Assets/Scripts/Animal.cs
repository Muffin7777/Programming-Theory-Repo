using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Animal : MonoBehaviour
{
    protected Rigidbody animalRigidbody;
    public bool isOnGround = true;
    public float jumpForce;
    public float speed;
    protected Animator anim;
    private float forwardInput;
    protected bool sprinting = false;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        animalRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
        Sprint();
    }

    public virtual void Walk()
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

    public abstract void Jump();

    public virtual void Sprint()
    {
        if (sprinting)
        {
            anim.SetInteger("Walk", 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
