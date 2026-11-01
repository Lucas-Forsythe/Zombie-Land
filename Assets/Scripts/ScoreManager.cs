
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int Score { get; private set; }

    [Header("Score Settings")]
    public int pointsPerKill = 100;

    [Header("UI")]
    public TMP_Text scoreText;

    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        UpdateScoreText();
    }

    public void AddKillScore()
    {
        Score += pointsPerKill;
        UpdateScoreText();

        Debug.Log("Player Score: " + Score);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score;
        }
    }

    public void ResetScore()
    {
        Score = 0;
        UpdateScoreText();
    }
}
