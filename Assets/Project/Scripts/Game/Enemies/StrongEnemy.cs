using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemy
{
    public Animator enemyAnimator;
    private Vector3 originalAnimatorPosition;

    public override void Hit()
    {
        base.Hit();
    }

    // Start is called before the first frame update
    void Awake()
    {
        base.health = 3;

        enemyAnimator.SetFloat("Forward", 0.3f);
        originalAnimatorPosition = enemyAnimator.transform.localPosition;
    }

    void LateUpdate() {
        // Keep the character animator's game object in place
        enemyAnimator.transform.localPosition = originalAnimatorPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
