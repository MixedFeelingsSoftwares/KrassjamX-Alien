using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject AlienPrefab;

    public List<Transform> aliens = new List<Transform>();

    public AudioClip enemyHitSound;

    public bool Fullscreen = false;

    public int MaxAliens = 10;

    public float SpawnRate;

    private float cTime { get; set; }

    private Vector3 spawnArea { get; set; }

    public static AlienSpawner Instance { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        cTime = Time.time;
        UpdateFullscreen();
    }

    private void Update()
    {
        if (Time.time >= cTime)
        {
            cTime = Time.time + SpawnRate;
            SpawnAlien();
        }
    }

    private void UpdateFullscreen()
    {
        spawnArea = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) * 2;
    }

    public void OnDrawGizmos()
    {
        if (spawnArea == Vector3.zero) { return; }

        if (aliens != null && aliens.Count > 0)
        {
            for (int i = 0; i < aliens.Count; i++)
            {
                var alien = aliens[i];
                if (alien != null)
                {
                    var CameraPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

                    var pos1 = alien.position;
                    var pos2 = new Vector3(CameraPos.x, alien.position.y, alien.transform.position.z); // TODO

                    Gizmos.DrawLine(pos1, pos2);
                }
                else
                {
                    aliens.RemoveAt(i);
                }
            }
        }
    }

    public void SpawnAlien()
    {
        if (!AlienPrefab) { return; }
        if (aliens.Count < MaxAliens)
        {
            int max = MaxAliens;

            float pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y / max;

            float randomYpos = pos * Random.Range(0, max);

            float randomPos = Random.Range(-1.0f, 1.0f);

            Vector3 startPos = new Vector3((Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x, randomYpos * randomPos, 0)
                + (AlienPrefab.GetComponent<SpriteRenderer>().bounds.size / 2);

            GameObject obj = Instantiate(AlienPrefab, startPos, Quaternion.identity, transform) as GameObject;
            obj.AddComponent<AlienCore>();
            aliens.Add(obj.transform);
        }
    }
} 