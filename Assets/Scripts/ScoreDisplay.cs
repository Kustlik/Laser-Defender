using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameSession gameSession;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
