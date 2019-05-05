using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject model;
    public float rotatingSpeed = 5f;

    [Header("Movement")]
    public float movingVelocity = 10f;
    public float jumpingVelocity = 8f;
    private Rigidbody playerRigidbody;
    private bool canJump = true;
    private Quaternion targetModelRotation;

    [Header("Equipment")]
    public Sword sword;
    public Bow bow;
    public GameObject bombPrefab;
    public int bombAmount = 100;
    public int arrowAmount = 100;
    public float throwingSpeed = 200;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        targetModelRotation = Quaternion.Euler(0, 180, 0);
        sword.gameObject.SetActive(false);
        bow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast to identify if the player can jump.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)) {
            canJump = true;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);
        ProcessInput();
    }

    void ProcessInput() {
        // Move in the XZ plane
        playerRigidbody.velocity = new Vector3(
            0,
            playerRigidbody.velocity.y,
            0
        );
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(
                movingVelocity,
                playerRigidbody.velocity.y,
                playerRigidbody.velocity.z
            );
            targetModelRotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            playerRigidbody.velocity = new Vector3(
                -movingVelocity,
                playerRigidbody.velocity.y,
                playerRigidbody.velocity.z
            );
            targetModelRotation = Quaternion.Euler(0, 270, 0);
        }
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                playerRigidbody.velocity.y,
                movingVelocity
            );
            targetModelRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                playerRigidbody.velocity.y,
                -movingVelocity
            );
            targetModelRotation = Quaternion.Euler(0, 180, 0);
        }
        // Check for jumps
        if (canJump && Input.GetKeyDown("space"))
        {
            // transform.position += Vector3.up * 100f * Time.deltaTime;
            canJump = false;
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
}
