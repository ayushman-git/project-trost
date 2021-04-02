using UnityEngine;

public class Gun : MonoBehaviour
{
  public float damage = 10f;
  //public float range = 200f;

  public Camera shootCam;
  public ParticleSystem muzzleFlash;
  // public GameObject impactEffect1;
  // public GameObject impactEffect2;
  // public GameObject impactEffect3;
  public int impactType = 0;
  public GameObject impacts;
  public float impactForce = 30f;
  public bool spray = true;

  void Start()
  {
    Type();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButton("Fire1") && spray)
    {
      Shoot();
    }
    if (Input.GetButtonDown("Fire1") && !spray)
    {
      Shoot();
    }
    if (Input.GetKeyDown(KeyCode.M))
    {
      spray = !spray;
    }
  }

  void Shoot()
  {
    muzzleFlash.Play();

    RaycastHit hit;
    if (Physics.Raycast(shootCam.transform.position, shootCam.transform.forward, out hit))
    {
      Debug.Log(hit.transform.name);
      Enemy target = hit.transform.GetComponent<Enemy>();
      if (target != null)
      {

        target.TakeDamage(damage);
      }

      //GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
      //Destroy(impactObject, 2f);

      if (hit.transform.name == "EnemyCapsule")
      {
        impactType = 2;
        Type();
        // GameObject impactObject = Instantiate(impactEffect1, hit.point, Quaternion.LookRotation(hit.normal));
        // Destroy(impactObject, 2f);
      }

      else if (hit.transform.name == "Platform")
      {
        impactType = 0;
        Type();
        // GameObject impactObject = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
        // Destroy(impactObject, 2f);
      }
    }
    if (hit.rigidbody != null)
    {
      hit.rigidbody.AddForce(-hit.normal * impactForce);
    }

  }
  void Type()
  {
    int i = 0;
    foreach (Transform weapon in impacts.transform)
    {
      if (i == impactType)
      {
        weapon.gameObject.SetActive(true);
      }
      else
      {
        weapon.gameObject.SetActive(false);
      }
      i++;
    }
  }
}
