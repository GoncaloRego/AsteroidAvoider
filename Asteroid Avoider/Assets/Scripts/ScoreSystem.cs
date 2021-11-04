using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    private float score;
    private bool scoreStopped = false;

    void Update()
    {
        if (scoreStopped == true)
        {
            return;
        }

        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int StopScore()
    {
        scoreStopped = true;

        scoreText.text = string.Empty;

        return Mathf.FloorToInt(score);
    }

    public void StartTimer()
    {
        scoreStopped = false;
    }
}
