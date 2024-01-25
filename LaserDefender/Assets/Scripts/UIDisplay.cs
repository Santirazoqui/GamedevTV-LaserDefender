using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] HealthScript playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    int health;
    int score;

    void Awake()
    {
        this.scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    void Update()
    {
        UpdateHealth();
        UpdateScore();
    }

    void UpdateHealth()
    {
        healthSlider.value = playerHealth.GetHealth();
    }

    void UpdateScore()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
