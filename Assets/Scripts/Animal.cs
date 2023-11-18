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
    public bool gameRunning;
    public bool finishGame;

    // Start is called before the first frame update
    void Start()
    {
        finishGame = false;
        gameRunning = false;
        anim = GetComponent<Animator>();
        animalRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning) {
            CorrectPosition();
            Walk();
            Jump();
            Sprint();
        }
    }

    // ABSTRACTION
    private void CorrectPosition()
    {
        if (gameObject.transform.position.y < -0.5F)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.09F, gameObject.transform.position.z);
        }
    }

    // ABSTRACTION
    public virtual void Walk()
    {
        forwardInput = Input.GetAxis("Horizontal");
        float forwardVel = Time.deltaTime * speed * forwardInput;
        animalRigidbody.velocity = new Vector3(animalRigidbody.velocity.x, animalRigidbody.velocity.y, forwardVel);
        if (forwardInput != 0 && isOnGround) {
            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
        }
    }

    // ABSTRACTION
    public abstract void Jump();

    // ABSTRACTION
    public virtual void Sprint()
    {
        if (sprinting)
        {
            anim.SetInteger("Walk", 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null  || collision.collider == null || !collision.collider.CompareTag("obstacle")) {
            isOnGround = true;
        }
    }
        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goalWall"))
        {
            anim.SetInteger("Walk", 0);
            finishGame = true;
        }
    }
}