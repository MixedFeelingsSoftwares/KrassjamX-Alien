using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorHelper
{
    /// <summary>
    /// if object contains Area
    /// </summary>
    /// <param name="alien">Transform</param>
    /// <param name="pos">Position</param>
    /// <param name="boundSize">Bounds of Area (full size)</param>
    /// <returns>Boolean</returns>
    public static WLGCollider Contains(this Transform alien, Vector3 pos, Vector3 boundSize)
    {
        return alien.position.x > pos.x - (boundSize.y / 2) && alien.position.x < pos.x + boundSize.x && alien.position.y < pos.y + (boundSize.y / 2) && alien.position.y > pos.y - boundSize.y ? new WLGCollider() { gameobject = alien.gameObject, Hit = pos, transform = alien.transform } : null;
    }

    public class WLGCollider
    {
        public GameObject gameobject { get; set; }

        public Vector3 Hit { get; set; }

        public Transform transform { get; set; }
    }
}

public class ProjectileCore : MonoBehaviour
{
    private bool onHit;

    public AllWeapons.WeaponData data;

    public GameObject Sender;

    public AlienSpawner spawner;

    // Start is called before the first frame update
    private void Start()
    {
        spawner = AlienSpawner.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        outsideScreenDestroy();
        foreach (var alien in spawner.aliens)
        {
            if (alien != null)
            {
                var bound = alien.GetComponent<SpriteRenderer>();
                var container = alien.Contains(transform.position, bound.size);
                if (container != null)
                {
                    if (!onHit)
                    {
                        onHit = true;
                        Destroy(gameObject);

                        var cCore = container.transform.GetComponent<AlienCore>();
                        if (cCore != null)
                        {
                            container.transform.GetComponent<Animator>().SetTrigger("Hit");
                            cCore.Hit(data.weapon.WeaponDamage, data.weapon.enemyHitAudio, new Action<GameObject>((x) =>
                            {
                                RunEnemyHitExplosion(x.transform);
                                Destroy(x);
                            }));
                        }
                    }
                }
            }
        }
        var calc = Vector2.right * data.weapon.bullet_Speed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + calc.x, transform.position.y + calc.y);

        transform.Rotate(Vector3.back, 1.0f * data.weapon.bullet_Speed);
    }

    public void outsideScreenDestroy()
    {
        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x)
        {
            Destroy(gameObject);
        }
    }

    public void RunEnemyHitExplosion(Transform ob)
    {
        GameObject exp = Instantiate(data.weapon.EnemyHitParticle, ob.position, Quaternion.identity);
        var dest = exp.AddComponent<ExplosionDestroyer>();
        dest.data = data;
    }
}