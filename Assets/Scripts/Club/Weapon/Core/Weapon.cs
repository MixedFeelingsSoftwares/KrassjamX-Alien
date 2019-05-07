using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Weapon"), System.Serializable]
public class Weapon : ScriptableObject
{
    public string Name;

    public float Speed = 0.75f;

    public Sprite WeaponSprite;
}