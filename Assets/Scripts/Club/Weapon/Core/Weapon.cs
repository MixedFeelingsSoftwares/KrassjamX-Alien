using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Weapon"), System.Serializable]
public class Weapon : ScriptableObject
{
    public float bullet_Speed = 0.75f;

    public AudioClip enemyHitAudio;

    public GameObject EnemyHitParticle;

    public float FireRate = 1.0f;

    public string Name;

    public AudioClip ShootAudio;

    public float WeaponDamage;

    public Sprite WeaponSprite;
}