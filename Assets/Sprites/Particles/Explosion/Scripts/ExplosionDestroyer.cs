using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    public AllWeapons.WeaponData data;

    public AudioSource source { get; set; }

    public void Audio()
    {
        if (data != null && data.weapon.enemyHitAudio != null && source != null && gameObject != null)
        {
            source.PlayOneShot(data.weapon.enemyHitAudio);
        }
    }

    public void Awake()
    {
        source = ShootSystem.Instance.source;
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }
}