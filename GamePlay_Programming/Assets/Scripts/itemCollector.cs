using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    int score = 0;

    [SerializeField] Text scoreText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("score"))
        {
            Destroy(other.gameObject);
            score++;
            scoreText.text = "score: " + score;
        }
    }
}
