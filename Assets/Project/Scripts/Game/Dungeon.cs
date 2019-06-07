using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    private int enemyCount;
    private int currentEnemyCount;
    private Enemy[] enemies;

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

    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
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
        // Debug.Log(currentEnemyCount + "/" + enemyCount);
    }
}
