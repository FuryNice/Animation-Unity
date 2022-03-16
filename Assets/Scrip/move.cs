using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public CharacterController characterController;
    public float jumpSpeed;
    private float ySpeed;
    public float rotationSpeed;
    public Animator animator;
    public GameObject Player;
    public float speed = 7f;
    private float count = 0f;
     void Start()
    {
        characterController = GetComponent<CharacterController>();

    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
    float horizontalInput = Input.GetAxis("Horizontal");
    float VerticalInput = Input.GetAxis("Vertical");

    Vector3 movementDirection = new Vector3(horizontalInput, 0, VerticalInput);
    float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
    movementDirection.Normalize();

    ySpeed += Physics.gravity.y * Time.deltaTime;
    if(Input.GetButtonDown("Jump") && characterController.isGrounded)
    {
        ySpeed = jumpSpeed;
    }
    Vector3 velocity = movementDirection * magnitude;
    velocity.y = ySpeed;

    characterController.Move(velocity * Time.deltaTime);
    if (movementDirection != Vector3.zero)
    {
        Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation , rotationSpeed * Time.deltaTime);
    }
        if (movementDirection.magnitude >= 0.01f)
        {
            animator.SetBool("moving", true);
              if (count >= 10f)
            {
                animator.SetBool("runing", true);
            }
                count += 0.01f;
            float angle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            Player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            characterController.Move(movementDirection * (speed+count) * Time.deltaTime);
        }
        else
        {
            count = 0f;
            animator.SetBool("moving", false);
            animator.SetBool("runing", false);
        }
    }
}