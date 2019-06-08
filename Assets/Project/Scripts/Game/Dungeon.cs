using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    private int enemyCount;
    private int currentEnemyCount;
    private Enemy[] enemies;
    private Treasure treasure;
    private bool isClear;
    private bool justCleared;

    public int EnemyCount {
        get {
            return enemyCount;
        }
    }

    public int CurrentEnemyCount {
        get {
            return currentEnemyCount;
        }
    }

    public bool JustCleared {
        get {
            bool returnValue = justCleared;
            justCleared = false;
            return returnValue;
        }
    }

    public Treasure Treasure {
        get {
            return treasure;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
        treasure = GetComponentInChildren<Treasure>();
        isClear = false;
        // treasure.gameObject.SetActive(false);
        // Debug.Log(enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemyCount = 0;
        foreach (Enemy enemy in enemies) {
            if (enemy != null) {
                currentEnemyCount++;
            }
        }

        if (isClear == false) {
            if (currentEnemyCount == 0) {
                isClear = true;
                justCleared = true;
            }
        }

        if (treasure != null) {
            treasure.gameObject.SetActive(isClear);
        }

        // Debug.Log(currentEnemyCount + "/" + enemyCount);
    }
}
