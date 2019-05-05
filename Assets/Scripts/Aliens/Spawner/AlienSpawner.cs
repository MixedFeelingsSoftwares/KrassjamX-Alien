using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public bool Fullscreen = false;

    public int MaxAliens = 10;

    public float SpawnRate;

    private float cTime { get; set; }

    private Vector3 spawnArea { get;  set; }

    // Start is called before the first frame update
    private void Start()
    {
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

        Gizmos.DrawWireCube(pos, spawnArea);
    }

    public void SpawnAlien()
    {
        var prefab = Resources.Load("Prefabs/Alien") as GameObject;
        if (prefab)
        {
            var obj = Instantiate(prefab, transform.position, Quaternion.identity);
            obj.transform.position = new Vector3(0, (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) * 2).y, 0);
        }
    }
}