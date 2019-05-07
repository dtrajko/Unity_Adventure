using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerrain : MonoBehaviour
{
    [Header("Movement")]
    public float movingVelocity = 50f;

    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        // Move in the XZ plane
        playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            playerRigidbody.velocity = new Vector3(
                movingVelocity, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            playerRigidbody.velocity = new Vector3(
                -movingVelocity, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
        }
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x, playerRigidbody.velocity.y, movingVelocity);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x, playerRigidbody.velocity.y, -movingVelocity);
        }
        if (Input.GetKey("space"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                movingVelocity,
                playerRigidbody.velocity.z
            );
        }
        if (Input.GetKey("left shift"))
        {
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                -movingVelocity,
                playerRigidbody.velocity.z
            );
        }
    }
}
