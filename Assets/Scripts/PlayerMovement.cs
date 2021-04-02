using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  const float defualtSpeed = 8f;
  public CharacterController controller;

  public float speed = 8f;
  public float gravity = -9.81f;
  public float groundDistance = 0.4f;
  public float jumpHeight = 3f;
  public Transform groundCheck;
  public LayerMask groundMask;
  Vector3 velocity;
  bool isGrounded;




  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    //condition for player is on ground or not
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocity.y < 0)
    {
      velocity.y = -2f;
    }


    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");

    //transform player so that player facing the right way and move at the same time
    Vector3 move = transform.right * x + transform.forward * y;
    controller.Move(move * speed * Time.deltaTime);

    //for jump
    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
      speed = 4f;

    }
    else if (isGrounded)
    {
      speed = defualtSpeed;

    }

    //run
    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
    {
      speed = 30f;
    }
    else
    {
      speed = defualtSpeed;
    }

    //dash
    //if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
    {
      // transform.position = new Vector3(transform.position.x + 100f, transform.position.y, transform.position.z);
      
     // Vector3 newPosition = new Vector3(10f, transform.position.y, transform.position.z);
     // controller.Move(newPosition * 200f * Time.deltaTime);
    }

    //adding gravity to fall  
    velocity.y += gravity * Time.deltaTime;

    controller.Move(velocity * Time.deltaTime);

  }
}
