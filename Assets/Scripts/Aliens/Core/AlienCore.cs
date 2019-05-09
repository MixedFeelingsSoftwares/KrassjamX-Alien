using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCore : MonoBehaviour
{
    public float Health = 100;

    public void Hit(float damage, AudioClip hitAudio,Action<GameObject> onDestroy)
    {
        if (Health - damage <= 0)
        {
            onDestroy(gameObject);
        }
        else
        {
            if (ShootSystem.Instance != null)
            {
                ShootSystem.Instance.source.PlayOneShot(hitAudio);
            }
            Health -= damage;
        }
    }
}