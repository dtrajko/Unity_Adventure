using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemy : Enemy
{
    public NavMeshAgent agent;
    public Animator enemyAnimator;
    public float attackRadius = 6f;
    public float attackUpdateDuration = 1f;
    public float movementRadius = 15f;
    public float movementUpdateDuration = 6f;

    private Vector3 initialPosition;
    private float attackUpdateTimer;
    private float movementUpdateTimer;
    private Vector3 originalEnemyAnimatorPosition;
    private Vector3 previousPosition;

    private bool isAttacking = false;

    public bool IsAttacking {
        get {
            return isAttacking;
        }
    }

    void Awake() {
        previousPosition = transform.position;
        originalEnemyAnimatorPosition = enemyAnimator.transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        movementUpdateTimer = movementUpdateDuration;
        attackUpdateTimer = attackUpdateDuration;

        MoveAroundStart();
    }

    // Update is called once per frame
    void Update()
    {
        // Walk around if didn't see the player
        if (isAttacking == false) {
            movementUpdateTimer -= Time.deltaTime;
            if (movementUpdateTimer <= 0f)
            {
                movementUpdateTimer = movementUpdateDuration;
                MoveAroundStart();
            }
        }

        //Search for the player
        attackUpdateTimer -= Time.deltaTime;
        if (attackUpdateTimer <= 0f) {
            attackUpdateTimer = attackUpdateDuration;
            SearchPlayer();
        }

        // Animate the chasing enemy
        if (agent.velocity.magnitude > 0) {
            enemyAnimator.SetFloat("Forward", 0.6f);
        }

        if (Vector3.Distance(transform.position, previousPosition) < 0.03f) {
            enemyAnimator.SetFloat("Forward", 0.0f);
        }

        previousPosition = transform.position;
    }

    void SearchPlayer()
    {
        isAttacking = false;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRadius, transform.forward);
        foreach (RaycastHit hit in hits) {
            if (hit.transform.GetComponent<Player>() != null) {
                agent.SetDestination(hit.transform.position);
                isAttacking = true;
                break;
            }
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

    void LateUpdate() {
        enemyAnimator.transform.localPosition = originalEnemyAnimatorPosition;
    }
}
