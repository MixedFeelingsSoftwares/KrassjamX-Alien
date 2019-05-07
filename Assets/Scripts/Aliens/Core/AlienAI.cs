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
        {
            UpdatePercentage();
        }
    }

    private void UpdatePercentage()
    {
        Vector3 sPos = Camera.main.WorldToScreenPoint(transform.position);

        int percentage = (int)((double)sPos.x / (double)Screen.width * (double)100);
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