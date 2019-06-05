﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject model;
    public Animator playerAnimator;
    public float rotatingSpeed = 5f;

    [Header("Movement")]
    public float movingVelocity = 10f;
    public float jumpingVelocity = 6f;
    public float knockbackForce = 100f;

    [Header("Equipment")]
    public int healthInit = 20;
    public int health;
    public Sword sword;
    public Bow bow;
    public GameObject bombPrefab;
    public int bombAmount = 100;
    public int arrowAmount = 100;
    public float throwingSpeed = 200;

    private Rigidbody playerRigidbody;
    private bool canJump = true;
    private Quaternion targetModelRotation;
    private float knockbackTimer = 1f;
    private bool justTeleported;
    private Vector3 originalAnimatorPosition;

    public bool JustTeleported {
        get {
            bool returnValue = justTeleported;
            justTeleported = false;
            return returnValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = healthInit;
        playerRigidbody = GetComponent<Rigidbody>();
        targetModelRotation = Quaternion.Euler(0, 0, 0);
        sword.gameObject.SetActive(false);
        bow.gameObject.SetActive(false);
        originalAnimatorPosition = playerAnimator.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast to identify if the player can jump.
        RaycastHit hit;
        bool raycastResult = Physics.Raycast(transform.position, Vector3.down, out hit, 1.41f);
        // Debug.Log("Player raycastResult: " + raycastResult);
        if (raycastResult) {
            canJump = true;
        }

        playerAnimator.SetBool("OnGround", raycastResult);
        // Debug.Log("playerAnimator raycastResult: " + raycastResult  + " canJump: " + canJump);

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);

        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
        }
        // else ProcessInput();
        ProcessInput();
    }

    void LateUpdate() {
        // Keep the character animator's game object in place
        playerAnimator.transform.localPosition = originalAnimatorPosition;
    }

    void ProcessInput() {
        // Move in the XZ plane
        playerRigidbody.velocity = new Vector3(
            0,
            playerRigidbody.velocity.y,
            0
        );

        bool isPlayerMoving = false;

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            playerRigidbody.velocity = new Vector3(
                movingVelocity,
                playerRigidbody.velocity.y,
                playerRigidbody.velocity.z
            );
            targetModelRotation = Quaternion.Euler(0, 90, 0);
            isPlayerMoving = true;
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            playerRigidbody.velocity = new Vector3(
                -movingVelocity,
                playerRigidbody.velocity.y,
                playerRigidbody.velocity.z
            );
            targetModelRotation = Quaternion.Euler(0, 270, 0);
            isPlayerMoving = true;
        }
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                playerRigidbody.velocity.y,
                movingVelocity
            );
            targetModelRotation = Quaternion.Euler(0, 0, 0);
            isPlayerMoving = true;
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                playerRigidbody.velocity.y,
                -movingVelocity
            );
            targetModelRotation = Quaternion.Euler(0, 180, 0);
            isPlayerMoving = true;
        }

        playerAnimator.SetFloat("Forward", isPlayerMoving ? 1.0f : 0.0f);

        // Check for jumps
        if (canJump && Input.GetKeyDown("space"))
        {
            // transform.position += Vector3.up * 100f * Time.deltaTime;
            canJump = true;
            // playerRigidbody.AddForce(0, jumpingVelocity, 0);
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                jumpingVelocity,
                playerRigidbody.velocity.z
            );
        }

        // Check equipment interaction
        if (Input.GetMouseButtonDown(0)) {
            sword.gameObject.SetActive(true);
            bow.gameObject.SetActive(false);
            sword.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (arrowAmount > 0) {
                sword.gameObject.SetActive(false);
                bow.gameObject.SetActive(true);
                bow.Attack();
                arrowAmount--;
            }
        }

        // Check equipment interaction
        if (Input.GetMouseButtonDown(2))
        {
            ThrowBomb();
        }
    }

    private void ThrowBomb() {
        if (bombAmount <= 0) {
            Debug.Log("No more bombs left!");
            return;
        }
        GameObject bombObject = Instantiate(bombPrefab);
        bombObject.transform.position = transform.position + model.transform.forward;
        Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;
        bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);
        bombAmount--;
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.GetComponent<EnemyBullet>() != null) {
            Hit((transform.position - otherCollider.transform.position).normalized);
            Destroy(otherCollider.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Enemy>() != null) {
            Hit((transform.position - collision.transform.position));
        }
        if (collision.gameObject.GetComponent<EnemyBullet>() != null) {
            Hit(transform.position - collision.transform.position);
        }
    }

    private void Hit(Vector3 direction) {
        Vector3 knockbackDirection = (direction + Vector3.up).normalized;
        playerRigidbody.AddForce(knockbackDirection * knockbackForce);
        knockbackTimer = 1f;
        if (!sword.IsEngaged) {
            health--;
        }
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void Teleport(Vector3 target)
    {
        transform.position = target;
        justTeleported = true;
    }

}
