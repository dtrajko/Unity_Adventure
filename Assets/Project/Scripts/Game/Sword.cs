using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Vector3 defaultAngle = new Vector3(100, 70, -20);
    public Vector3 attackAngle = new Vector3(180, 70, -20);
    public Vector3 runningAngle = new Vector3(100, 70, -20);

    public GameObject playerModel;

    public float swingingSpeed = 16f;
    public float cooldownSpeed = 8f;
    public float attackDuration = 0.4f;
    public float cooldownDuration = 0.4f;

    public Quaternion targetRotation;
    private float cooldownTimer;
    private bool isAttacking;
    private bool justAttacked;

    public bool IsAttacking {
        get {
            return isAttacking;
        }
    }

    public bool JustAttacked {
        get {
            return justAttacked;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = Quaternion.Euler(defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * (isAttacking ? swingingSpeed : cooldownSpeed));
        cooldownTimer -= Time.deltaTime;
    }
    public void Attack() {
        if (cooldownTimer > 0f) {
            return;
        }
        targetRotation = Quaternion.Euler(attackAngle);
        cooldownTimer = cooldownDuration;
        StartCoroutine(CooldownWait());
    }

    private IEnumerator CooldownWait()
    {
        isAttacking = true;
        justAttacked = true;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        justAttacked = false;

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;

        targetRotation = Quaternion.Euler(defaultAngle);
    }
}
