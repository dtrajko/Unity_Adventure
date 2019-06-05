﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingLogic : MonoBehaviour
{
    public GameObject model;
    public Vector3[] directions;
    public float timeToChange = 1f;
    public float movementSpeed;

    private int directionPointer;
    private float directionTimer;

    /*  0      1     2     3
     * Up, right, down, left
     */

    // Start is called before the first frame update
    void Start()
    {
        directionPointer = 0;
        directionTimer = timeToChange;
    }

    // Update is called once per frame
    void Update()
    {
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0) {
            directionTimer = timeToChange;
            directionPointer++;
            if (directionPointer >= directions.Length) {
                directionPointer = 0;
            }
        }

        model.transform.forward = directions[directionPointer];

        // Make the object move
        GetComponent<Rigidbody>().velocity = new Vector3(
            directions[directionPointer].x * movementSpeed,
            GetComponent<Rigidbody>().velocity.y,
            directions[directionPointer].z * movementSpeed
        );
    }
}
