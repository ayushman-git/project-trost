using UnityEngine;
using System.Collections;

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
  public bool spray = true;

  public int maxAmmo = 5;
  private int currentAmmo;
  float reloadTime = 2f;
  private bool isReloading = false;
  public float fireRate = 0.1f;
  public float nextFire = 0f;
  public Animator reloadControl;

  void Start()
  {
    currentAmmo = maxAmmo;
    Type();
  }

  // Update is called once per frame
  void Update()
  {
    if (isReloading)
    {
      return; //To terminate execution if the gun is reloading
    }

    if (currentAmmo <= 0)
    {
      StartCoroutine(Reload()); //Syntax for calling the coroutine function(Reload)
      return;
    }

    if (Input.GetButton("Fire1") && spray && Time.time > nextFire)
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

  IEnumerator Reload()  //Used IEnumerator to mark this function as a coroutine(It allows pausing the function at any given time and resuming it after a certain condition is met.)
  {
    isReloading = true;

    Debug.Log("Reloading");

    reloadControl.SetBool("Reloading", true);

    yield return new WaitForSeconds(reloadTime - 2f);     //Adding reload time(This will pause the execution for "reloadTime" seconds and then resume it after the condition is met.)

    reloadControl.SetBool("Reloading", false);

    currentAmmo = maxAmmo;

    isReloading = false;
  }

  void Shoot()
  {
    nextFire = Time.time + fireRate;
    currentAmmo--;

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

      // GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
      // Destroy(impactObject, 2f);

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

  }

  void Type()
  {
    int i = 0;
    foreach (Transform weapon in impacts.transform)
    {
      Debug.Log(i);
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