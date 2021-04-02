using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHandler : MonoBehaviour
{
  public Rigidbody rb;
  public float power = 500.0f;
  public float rotationSpeed = 500f;
  private bool inAir = false;
  private bool isReturning = false;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.O)) ThrowAxe();
    if (Input.GetKeyDown(KeyCode.P)) ReturnAxe();
    if (inAir)
    {
      transform.localEulerAngles += (transform.forward) * rotationSpeed * Time.deltaTime;
    }
  }

  void ThrowAxe()
  {
    rb.isKinematic = false;
    isReturning = false;
    rb.transform.parent = null;
    rb.AddForce((transform.right * -1) * power * Time.deltaTime, ForceMode.Impulse);
    inAir = true;
  }

  void ReturnAxe()
  {
    isReturning = true;
    rb.isKinematic = false;
    rb.velocity = Vector3.zero;
  }

  private void OnCollisionEnter(Collision coll)
  {
    inAir = false;
    if (!isReturning)
    {
      rb.isKinematic = true;
    }
  }
}
