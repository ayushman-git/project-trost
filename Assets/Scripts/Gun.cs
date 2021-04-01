using UnityEngine;

public class Gun : MonoBehaviour
{
  public float damage = 1f;
  //public float range = 200f;

  public Camera shootCam;
  public ParticleSystem muzzleFlash;
  public bool spray = true;


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
      Debug.Log("HIT");
      Enemy target = hit.transform.GetComponent<Enemy>();
      if (target != null)
      {
        target.TakeDamage(damage);
      }
    }
  }
}
