using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public int currentWeaponIndex;

    public AllWeapons.WeaponData Weapon;

    //#1
    public event System.EventHandler<WeaponEventArgs> WeaponChanged;

    public AllWeapons.WeaponData equippedWeapon
    {
        get
        {
            return Weapon;
        }

        set
        {
            if (Weapon != value)
            {
                Weapon = value;
                OnWeaponChanged(value);
            }
        }
    }

    public ShootSystem Instance { get; set; }

    private void ShootSystem_WeaponChanged(object sender, WeaponEventArgs e)
    {
        UpdateClubSprite();
        Debug.Log($"Weapon Changed!\n{e.WeaponData.DataName}");
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (AllWeapons.Instance.Weapons.Count > 0)
        {
            currentWeaponIndex = 0;
            equippedWeapon = AllWeapons.Instance.Weapons[currentWeaponIndex];
        }

        WeaponChanged += ShootSystem_WeaponChanged;
    }

    // Update is called once per frame
    private void Update()
    {
        if (AllWeapons.Instance != null)
        {
            var aWep = AllWeapons.Instance;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                // Positive
                if (aWep != null && aWep.Weapons.Count > (currentWeaponIndex + 1))
                {
                    currentWeaponIndex++;
                    equippedWeapon = aWep.Weapons[currentWeaponIndex];
                }
                else if (aWep.Weapons.Count > 0)
                {
                    currentWeaponIndex = 0;
                    equippedWeapon = aWep.Weapons[currentWeaponIndex];
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                // Negative
                if (aWep.Weapons.Count > 0 && currentWeaponIndex - 1 >= 0)
                {
                    currentWeaponIndex--;
                    equippedWeapon = aWep.Weapons[currentWeaponIndex];
                }
                else if (aWep.Weapons.Count > 0)
                {
                    currentWeaponIndex = aWep.Weapons.Count - 1;

                    equippedWeapon = aWep.Weapons[currentWeaponIndex];
                }
            }
        }
    }

    //#2
    protected virtual void OnWeaponChanged(AllWeapons.WeaponData data)
    {
        if (WeaponChanged != null) WeaponChanged(this, new WeaponEventArgs() { WeaponData = data });
    }

    public void UpdateClubSprite()
    {
        SpriteRenderer renderer = null;

        if (transform.GetComponent<SpriteRenderer>() != null)
        {
            renderer = transform.GetComponent<SpriteRenderer>();
        }
        else
        {
            renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if (renderer != null)
        {
            renderer.sprite = equippedWeapon.weapon.WeaponSprite;
        }
    }

    public class Aim
    {
        public float X { get; set; }

        public float Y { get; set; }
    }
}

public class WeaponEventArgs : EventArgs
{
    public AllWeapons.WeaponData WeaponData { get; set; }
}