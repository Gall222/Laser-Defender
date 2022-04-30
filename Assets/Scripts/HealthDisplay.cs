using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI playerHealthText;
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        playerHealthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = player.GetPlayerHealth().ToString();
    }
}
