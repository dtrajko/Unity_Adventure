using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [Header("Game")]
    public Player player;

    [Header("UI")]
    public Text healthText;
    public Text bombsText;
    public Text arrowsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int playerHealth = player != null ? player.health : 0;
        healthText.text = "Health: " + playerHealth.ToString();

        int bombAmount = player != null ? player.bombAmount : 0;
        bombsText.text = "Bombs: " + bombAmount.ToString();

        int arrowAmount = player != null ? player.arrowAmount : 0;
        arrowsText.text = "Arrows: " + arrowAmount.ToString();
    }
}
