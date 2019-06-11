using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemy : Enemy
{
    public NavMeshAgent agent;
    public Animator enemyAnimator;
    public float movementRadius = 15f;
    public float movementUpdateDuration = 6f;

    private Vector3 initialPosition;
    private float movementUpdateTimer;
    private Vector3 originalEnemyAnimatorPosition;
    private Vector3 previousPosition;

    void Awake() {
        previousPosition = transform.position;
        originalEnemyAnimatorPosition = enemyAnimator.transform.localPosition;
    }

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

        if (agent.velocity.magnitude > 0) {
            enemyAnimator.SetFloat("Forward", 0.6f);
        } else {
            enemyAnimator.SetFloat("Forward", 0.0f);
        }

        if (Vector3.Distance(transform.position, previousPosition) < 0.03f) {
            enemyAnimator.SetFloat("Forward", 0.0f);
        }

        previousPosition = transform.position;
    }

    void MoveAroundStart()
    {
        agent.SetDestination(initialPosition + new Vector3(
            Random.Range(-movementRadius, movementRadius),
            0,
            Random.Range(-movementRadius, movementRadius)
        ));
    }

    void LateUpdate() {
        enemyAnimator.transform.localPosition = originalEnemyAnimatorPosition;
    }
}
