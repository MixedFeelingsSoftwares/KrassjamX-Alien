using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAI : MonoBehaviour
{
    [Range(0, 100)]
    public float Percentage = 0;

    [Range(1, 100)]
    public int Speed = 1;

    private void FixedUpdate()
    {
        float xPos = Speed * Time.deltaTime * -1;

        transform.Translate(new Vector3(xPos, 0, 0));
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}