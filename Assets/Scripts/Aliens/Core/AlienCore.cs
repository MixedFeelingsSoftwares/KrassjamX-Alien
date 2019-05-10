using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCore : MonoBehaviour
{
    public UnityEngine.UI.Image PB_Health;
    public float Health = 100;
    private float MaxHealth;
    public void Start()
    {
        MaxHealth = Health;
        foreach (var img in transform.GetComponentsInChildren<UnityEngine.UI.Image>())
        {
            if (img.tag == "PB_Done")
            {
                PB_Health = img;
            }
        }
    }

    public void Update()
    {
        UpdateHealth();
    }

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

    public void UpdateHealth()
    {
        if (PB_Health != null)
        {
            float percentage = ((MaxHealth - Health) / 100);
            PB_Health.fillAmount = Mathf.Lerp(PB_Health.fillAmount, percentage, 10*Time.deltaTime);
        }
    }
}