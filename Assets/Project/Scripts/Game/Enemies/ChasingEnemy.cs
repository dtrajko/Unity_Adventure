using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator enemyAnimator;
    public float movementRadius = 15f;
    public float movementUpdateDuration = 3f;

    private Vector3 initialPosition;
    private float movementUpdateTimer;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        movementUpdateTimer = movementUpdateDuration;
        MoveAroundStart();
    }

    // Update is called once per frame
    void Update()
    {
        movementUpdateTimer -= Time.deltaTime;
        if (movementUpdateTimer <= 0f) {
            movementUpdateTimer = movementUpdateDuration;
            MoveAroundStart();
        }
    }

    void MoveAroundStart()
    {
        agent.SetDestination(initialPosition + new Vector3(
            UnityEngine.Random.Range(-movementRadius, movementRadius),
            0,
            UnityEngine.Random.Range(-movementRadius, movementRadius)
        ));
    }
}
