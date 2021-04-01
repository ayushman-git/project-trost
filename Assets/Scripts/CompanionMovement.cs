using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
  public GameObject Player;
  public float TargetDistance;
  public float AllowedDistance = 5;
  public float UpwardThrust = 60.0f;
  public float FollowSpeed;
  public RaycastHit Ray;
  Rigidbody rb;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    rb.AddForce(transform.up * UpwardThrust * Time.deltaTime);
    // Keeping companion's front towards player
    transform.LookAt(Player.transform);
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Ray))
    {
      TargetDistance = Ray.distance;
      if (TargetDistance >= AllowedDistance)
      {
        FollowSpeed = 0.02f;
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, FollowSpeed);
      }
      else if (TargetDistance < 2)
      {
        rb.AddForce((transform.forward * -1) * 5.0f * Time.deltaTime, ForceMode.Impulse);
      }
      else
      {
        FollowSpeed = 0;
      }
    }
  }
}
