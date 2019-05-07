using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllWeapons : MonoBehaviour
{
    public List<WeaponData> Weapons = new List<WeaponData>();

    public static AllWeapons Instance { get; set; }

    public void Awake()
    {
        Instance = this;
    }

    [System.Serializable]
    public class WeaponData
    {
        public string DataName;

        public Weapon weapon;
    }
}