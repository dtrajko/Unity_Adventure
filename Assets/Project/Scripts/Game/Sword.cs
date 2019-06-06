using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Vector3 defaultAngle;
    public Vector3 attackAngle;
    public Vector3 runningAngle;

    public GameObject playerModel;

    public float swingingSpeed = 16f;
    public float cooldownSpeed = 8f;
    public float attackDuration = 0.4f;
    public float cooldownDuration = 0.4f;

    public Quaternion targetRotation;
    private float cooldownTimer;
    private bool isAttacking;
    private bool isEngaged;

    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }
    }

    public bool IsEngaged
    {
        get
        {
            return isEngaged;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = Quaternion.Euler(defaultAngle);
        GetComponent<BoxCollider>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * (isAttacking ? swingingSpeed : cooldownSpeed));
        cooldownTimer -= Time.deltaTime;
    }
    public void Attack() {
        isEngaged = true;
        if (cooldownTimer > 0f) {
            return;
        }
        targetRotation = Quaternion.Euler(attackAngle);
        cooldownTimer = cooldownDuration;
        StartCoroutine(CooldownWait());
        isEngaged = false;
    }

    private IEnumerator CooldownWait()
    {
        isAttacking = true;
        GetComponent<BoxCollider>().isTrigger = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        GetComponent<BoxCollider>().isTrigger = false;
        targetRotation = Quaternion.Euler(defaultAngle);
    }
}
