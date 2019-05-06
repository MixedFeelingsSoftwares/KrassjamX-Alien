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
        float rN = Random.Range(1.0f, 10.0f);

        float yPos = Speed * rN * Time.deltaTime * -1;
        transform.Translate(new Vector3(0, yPos, 0));
        {
            UpdatePercentage();
        }
    }

    private void UpdatePercentage()
    {
        Vector3 sPos = Camera.main.WorldToScreenPoint(transform.position);
        int percentage = (int)((double)sPos.y / (double)Screen.height * (double)100);
        if (percentage >= 0)
        {
            Percentage = percentage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}