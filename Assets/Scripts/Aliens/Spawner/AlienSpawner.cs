using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public static AlienSpawner Instance { get; private set; }

    public List<Transform> aliens = new List<Transform>();

    public GameObject AlienPrefab;

    public bool Fullscreen = false;

    public int MaxAliens = 10;

    public float SpawnRate;

    private float cTime { get; set; }

    private Vector3 spawnArea { get;  set; }

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

        Vector3 pos = Camera.main.transform.position;

        float vPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        Vector3 zPos = new Vector3(vPos, (Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0))).y, 0);

        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireCube(transform.position, zPos);

        Gizmos.color = new Color(255, 255, 255);
        Gizmos.DrawWireCube(pos, spawnArea);

        if (aliens != null && aliens.Count > 0)
        {
            for (int i = 0; i < aliens.Count; i++)
            {
                var alien = aliens[i];
                if (alien != null)
                {
                    var CameraPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

                    var pos1 = alien.position;
                    var pos2 = new Vector3(alien.position.x, CameraPos.y, 0); // TODO

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
        int max = MaxAliens;

        float pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y / max;

        float randomYpos = pos * Random.Range(0, max);

        float randomPos = Random.Range(-1.0f, 1.0f);

        Vector3 startPos = new Vector3((Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x, randomYpos*randomPos, 0)
            + (AlienPrefab.GetComponent<SpriteRenderer>().bounds.size / 2);

        GameObject obj = Instantiate(AlienPrefab, startPos, Quaternion.identity, transform) as GameObject;

        aliens.Add(obj.transform);
    }
} 