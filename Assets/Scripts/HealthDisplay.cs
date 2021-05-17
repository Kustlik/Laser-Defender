using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    Player player;

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
