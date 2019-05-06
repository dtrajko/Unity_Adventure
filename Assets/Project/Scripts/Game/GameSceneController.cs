using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [Header("Game")]
    public Player player;

    [Header("UI")]
    public GameObject[] hearts;
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
        if (player != null)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i * (player.healthInit / hearts.Length) < player.health);
            }
            healthText.text = "Health: " + player.health.ToString();
            bombsText.text = "Bombs: " + player.bombAmount.ToString();
            arrowsText.text = "Arrows: " + player.arrowAmount.ToString();
        } else {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(false);
            }
            healthText.text = "Health: 000";
            bombsText.text = "Bombs: 000";
            arrowsText.text = "Arrows: 000";
        }
    }
}
