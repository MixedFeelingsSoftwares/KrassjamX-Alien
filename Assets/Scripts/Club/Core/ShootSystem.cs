using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public Transform ProjectieParent;
    public int currentWeaponIndex;

    public Aim currentAim = new Aim();

    public int currentMaxAliens;

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

    public void Shoot()
    {
        if (ProjectieParent == null) { ProjectieParent = new GameObject("Projectiles").transform; }

        var cWeapon = equippedWeapon;
        GameObject obj = new GameObject("Projectile");
        obj.transform.SetParent(ProjectieParent, true);

        obj.transform.position = transform.position;

        var sp = obj.AddComponent<SpriteRenderer>();
        sp.sprite = equippedWeapon.weapon.WeaponSprite;

        var ProjCore = obj.AddComponent<ProjectileCore>();

        var collider = obj.AddComponent<BoxCollider2D>();

        collider.size = sp.bounds.size;

        ProjCore.data = cWeapon;
        ProjCore.Sender = gameObject;
        
    }


    public ShootSystem Instance { get; set; }
    private void UpdateClubPosition()
    {
        if (AlienSpawner.Instance == null) { return; }

        int TotalAliens = AlienSpawner.Instance.MaxAliens;
        currentMaxAliens = TotalAliens;
        Vector3 bounds = GetComponent<SpriteRenderer>().bounds.size;
        Vector3 CameraPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)) + (bounds / 2);

        float tW = CameraPos.y / TotalAliens;

        currentAim.X = CameraPos.x;
        transform.position = new Vector3(CameraPos.x, (tW * currentAim.Y), transform.position.z);
    }

    private void ShootSystem_WeaponChanged(object sender, WeaponEventArgs e)
    {
        UpdateClubSprite();
        Debug.Log($"Weapon Changed!\n{e.WeaponData.DataName}");
    }

    // Start is called before the first frame update
    private void Start()
    {
        WeaponChanged += ShootSystem_WeaponChanged;
        if (AllWeapons.Instance.Weapons.Count > 0)
        {
            currentWeaponIndex = 0;
            equippedWeapon = AllWeapons.Instance.Weapons[currentWeaponIndex];
        }
    }

    // Update is called once per frame
    private void Update()
    {
        checkIfWeaponChanged();
        checkMoveClubPosition();
        checkIfShoot();
    }

    private void FixedUpdate()
    {
        UpdateClubPosition();
    }

    public void checkIfShoot()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void checkMoveClubPosition()
    {
        if (Input.GetKeyUp(KeyCode.W)) // Up
        {
            if (currentAim.Y < (currentMaxAliens - 1))
            {
                currentAim.Y++;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S)) // Down
        {
            if (currentAim.Y > (-currentMaxAliens + 1))
            {
                currentAim.Y--;
            }
        }
    }

    public void checkIfWeaponChanged()
    {
        if (AllWeapons.Instance != null)
        {
            var aWep = AllWeapons.Instance;
            if (Input.GetKeyUp(KeyCode.Q)) // forward
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
            else if (Input.GetKeyUp(KeyCode.E)) // backwards
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
}

[System.Serializable]
public class Aim
{
    public float X { get; set; }

    public float Y { get; set; }
}

public class WeaponEventArgs : EventArgs
{
    public AllWeapons.WeaponData WeaponData { get; set; }
}