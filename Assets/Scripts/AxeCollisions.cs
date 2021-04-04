using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollisions : MonoBehaviour
{
  public Rigidbody rb;
  public Axe axe;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void AddConstraints()
  {
    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
  }

  public void RemoveConstraints()
  {
    rb.constraints = RigidbodyConstraints.None;
  }

  void OnCollisionEnter(Collision col)
  {
    axe.CollisionOccured();
    rb.useGravity = false;
    rb.isKinematic = true;
    AddConstraints();
  }


}
