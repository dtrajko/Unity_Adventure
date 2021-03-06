﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameCamera gameCamera;

    [Header("UI")]
    public GameObject[] hearts;
    public Text healthText;
    public Text orbText;
    public Text bombsText;
    public Text arrowsText;
    public GameObject dungeonPanel;
    public Text dungeonInfoText;

    private float resetTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Check for player information
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i * (player.healthInit / hearts.Length) < player.health);
            }
            healthText.text = "Health: " + player.health.ToString();
            orbText.text = "Orbs: " + player.orbAmount.ToString();
            bombsText.text = "Bombs: " + player.bombAmount.ToString();
            arrowsText.text = "Arrows: " + player.arrowAmount.ToString();

            // Check for dungeon information
            Dungeon currentDungeon = player.CurrentDungeon;
            if (currentDungeon != null)
            {
                dungeonPanel.SetActive(true);
                float clearPercentage = (float) (currentDungeon.EnemyCount - currentDungeon.CurrentEnemyCount) / (float) currentDungeon.EnemyCount;
                clearPercentage = Mathf.Floor(clearPercentage * 100);
                dungeonInfoText.text  = "Dungeon: " + currentDungeon.name;
                dungeonInfoText.text += " Enemies: " + currentDungeon.CurrentEnemyCount + "/" + currentDungeon.EnemyCount;
                dungeonInfoText.text += " [ " + clearPercentage + "% ]";

                if (currentDungeon.JustCleared) {
                    gameCamera.FocusOn(currentDungeon.Treasure.gameObject);
                }
            }
            else {
                dungeonPanel.SetActive(false);
            }
            

        } else {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(false);
            }

            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0f) {
                SceneManager.LoadScene("Menu");
            }

            healthText.text = "Health: 000";
            bombsText.text = "Bombs: 000";
            arrowsText.text = "Arrows: 000";
        }
    }
}
