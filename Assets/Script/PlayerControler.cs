using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float heightReduc = 2.0f;
    public int angleSlide = 55;
    private bool isCrouched = false;
    private bool sliding = false;
    private bool canMove = true;
    private bool canDoAction = true;
    private Vector3 moveDirection = Vector3.zero;
    private CapsuleCollider mCollider;
    private CharacterController controller;
    
    void OnTriggerEnter(Collider col)
    {
        /*float angle = Vector3.Angle(hit.normal, Vector3.up);
        if (angle > angleSlide)
        {
            Vector3 slideDir = hit.normal;
            Vector3 slideDirTest = hit.normal;
            slideDirTest.y *= -1;
            slideDir.y = this.transform.up.y * -1.0f;
            slideDir = slideDir.normalized;      
            controller.Move(slideDir * Time.deltaTime);
            sliding = true;
        }
        else
        {
            sliding = false;
        }*/
        Debug.Log(col.transform.tag);
    }
    

    public void cantMove()
    {
        canMove = false;
    }

    public void cantDoAnAction()
    {
        canDoAction = false;
    }

    public void stopMovement()
    {
        moveDirection = new Vector3(0, 0, 0);
    }

    public void freeMove()
    {
        canMove = true;
    }

    public void freeAction()
    {
        canDoAction = true;
    }

    void Start()
    {
        mCollider = this.gameObject.GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove)
        {
            if (canDoAction)
            {
                // is the controller on the ground?
                if (controller.isGrounded)
                {
                    //Feed moveDirection with input.
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDirection = transform.TransformDirection(moveDirection);

                    //Multiply it by speed.
                    moveDirection *= speed;

                    //Jumping
                    if (Input.GetButton("Jump") && !sliding)
                    {
                        moveDirection.y = jumpSpeed;
                    }

                    if (Input.GetButtonDown("Crouch"))
                    {
                        isCrouched = !isCrouched;

                        if (isCrouched)
                        {
                            speed /= 3;
                            mCollider.height = mCollider.height / heightReduc;
                            mCollider.center = mCollider.center / heightReduc;
                            this.gameObject.transform.Translate(0, -(mCollider.height / heightReduc), 0);
                            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y / heightReduc, this.transform.localScale.z);
                        }
                        else
                        {
                            speed *= 3;
                            mCollider.height = mCollider.height * heightReduc;
                            mCollider.center = mCollider.center * heightReduc;
                            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * heightReduc, this.transform.localScale.z);
                        }
                    }

                }
            }

            //Applying gravity to the controller
            moveDirection.y -= gravity * Time.deltaTime;

            //Making the character move
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
