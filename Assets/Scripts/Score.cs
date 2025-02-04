using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    TextMeshProUGUI scoreText;
    private void Awake()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        UpdateUi();
    }

    public void UpdateStatus(int s)
    {
        score += s;
        UpdateUi();
    }

    private void UpdateUi()
    {
        scoreText.text = score.ToString();
    }
}
