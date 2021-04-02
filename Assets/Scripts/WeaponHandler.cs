using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
  public int selectedWeapon = 0;

  void Start()
  {
    changeWeapon();
  }
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      selectedWeapon = 0;
      changeWeapon();
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      selectedWeapon = 1;
      changeWeapon();
    }
  }

  void changeWeapon()
  {
    int i = 0;
    foreach (Transform weapon in transform)
    {
      if (i == selectedWeapon)
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
