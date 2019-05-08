using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCore : MonoBehaviour
{
    public AllWeapons.WeaponData data;
    public GameObject Sender;
    public AlienSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = AlienSpawner.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (data == null) { return; }
        foreach (var alien in spawner.aliens)
        {
            if (alien != null)
            {
                var bound = alien.GetComponent<SpriteRenderer>();
                if (alien.position.x < transform.position.x && alien.position.x > transform.position.x + bound.size.x && alien.position.y < transform.position.y && alien.position.y < transform.position.y - bound.size.y)
                {
                    Debug.Log("Test");
                }
            }
        }
        var calc = Vector2.right * data.weapon.Speed * Time.deltaTime;
        transform.Translate(calc + (Vector2.down * Mathf.Sin(calc.magnitude)));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hi");
    }
}
