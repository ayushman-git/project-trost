using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
  public float mouseSenstivity = 200f;
  public Transform playerBody;
  private float xRotation = 0f;
  // Start is called before the first frame update
  void Start()
  {
    //for hiding and lock the Cursor
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {
    float mouseX = Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime;

    playerBody.Rotate(Vector3.up * mouseX);

    xRotation -= mouseY;

    xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //clamp for stoping camera to over rotate

    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
  }
}
