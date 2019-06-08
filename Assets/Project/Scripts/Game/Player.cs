using System;
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
    public float playerRotatingSpeed = 400f;

    [Header("Equipment")]
    public int healthInit = 20;
    public int health;
    public Sword sword;
    public Bow bow;
    public GameObject quiver;
    public GameObject bombPrefab;
    public int bombAmount = 100;
    public int arrowAmount = 100;
    public float throwingSpeed = 200;
    public int orbAmount = 0;

    private Rigidbody playerRigidbody;
    private bool canJump = true;
    private Quaternion targetModelRotation;
    private float knockbackTimer = 1f;
    private bool justTeleported;
    private Vector3 originalAnimatorPosition;
    private Dungeon currentDungeon;

    public bool JustTeleported {
        get {
            bool returnValue = justTeleported;
            justTeleported = false;
            return returnValue;
        }
    }

    public Dungeon CurrentDungeon {
        get {
            return currentDungeon;
        }
    }

    public Quaternion TargetModelRotation
    {
        get
        {
            return targetModelRotation;
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
        // quiver.gameObject.SetActive(false);
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

        // Debug.Log("currentDungeon: " + currentDungeon);
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
            targetModelRotation = Quaternion.Euler(
                0,
                model.transform.localEulerAngles.y + playerRotatingSpeed * Time.deltaTime,
                0
            );
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            targetModelRotation = Quaternion.Euler(
                0,
                model.transform.localEulerAngles.y - playerRotatingSpeed * Time.deltaTime,
                0
            );
        }
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            playerRigidbody.velocity = new Vector3(
                model.transform.forward.x * movingVelocity,
                playerRigidbody.velocity.y,
                model.transform.forward.z * movingVelocity
            );
            isPlayerMoving = true;
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            playerRigidbody.velocity = new Vector3(
                -model.transform.forward.x * movingVelocity,
                playerRigidbody.velocity.y,
                -model.transform.forward.z * movingVelocity
            );
            isPlayerMoving = true;
        }

        playerAnimator.SetFloat("Forward", isPlayerMoving ? 1.0f : 0.0f);

        if (isPlayerMoving) {
            sword.targetRotation = Quaternion.Euler(sword.runningAngle);
        }
        else {
            sword.targetRotation = Quaternion.Euler(sword.defaultAngle);
        }

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
            // quiver.gameObject.SetActive(false);
            sword.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (arrowAmount > 0) {
                sword.gameObject.SetActive(false);
                bow.gameObject.SetActive(true);
                quiver.gameObject.SetActive(true);
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
        } else if (otherCollider.GetComponent<Treasure>() != null) {
            orbAmount++;
            Destroy(otherCollider.gameObject);
        }
    }

    void OnTriggerStay(Collider otherCollider) {
        if (otherCollider.GetComponent<Dungeon>() != null) {
            currentDungeon = otherCollider.GetComponent<Dungeon>();
        }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Dungeon>() != null)
        {
            Dungeon exitDungeon = otherCollider.GetComponent<Dungeon>();
            if (exitDungeon == currentDungeon) {
                currentDungeon = null;
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Enemy>()) {
            Hit((transform.position - collision.transform.position).normalized);
        }
        if (collision.gameObject.GetComponent<EnemyBullet>() != null) {
            Hit(transform.position - collision.transform.position);
        }
    }

    private void Hit(Vector3 direction) {
        Vector3 knockbackDirection = (direction + Vector3.up).normalized;
        playerRigidbody.AddForce(knockbackDirection * knockbackForce);
        knockbackTimer = 1f;
        health--;
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
