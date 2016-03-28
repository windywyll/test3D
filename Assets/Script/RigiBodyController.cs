using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class RigiBodyController : MonoBehaviour {

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float rotSpeed = 3;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public float heightReduc = 2.0f;
    public float speedReductor = 3.0f;
    private bool grounded = false;
    private bool isCrouched = false; 
    private CapsuleCollider mCollider;
    public Rigidbody rigidbody;
    private bool disabled;

    public void disableAllButMove()
    {
        disabled = true;
    }

    public void enableAll()
    {
        disabled = false;
    }

    public void setToCrouchSpeed()
    {
        speed /= speedReductor;
    }

    public void revertToInitSpeed()
    {
        speed *= speedReductor;
    }

    void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        mCollider = this.GetComponent<CapsuleCollider>();
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            //Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump") && !isCrouched && !disabled)
            {
                rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }

            if (Input.GetButtonDown("Crouch") && !disabled)
            {
                isCrouched = !isCrouched;

                if (isCrouched)
                {
                    speed /= speedReductor;
                    mCollider.height = mCollider.height / heightReduc;
                    mCollider.center = mCollider.center / heightReduc;
                    this.gameObject.transform.Translate(0, -(mCollider.height / heightReduc), 0);
                    this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y / heightReduc, this.transform.localScale.z);
                }
                else
                {
                    speed *= speedReductor;
                    mCollider.height = mCollider.height * heightReduc;
                    mCollider.center = mCollider.center * heightReduc;
                    this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * heightReduc, this.transform.localScale.z);
                }
            }
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        Vector3 rot = new Vector3(0, Input.GetAxis("Horizontal") * speed, 0);
        rot.Normalize();
        rot *= rotSpeed;
        this.gameObject.transform.Rotate(rot);

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
