﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Player player;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float focusSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {

            Vector3 offsetFinal = offset;
            offsetFinal.z += player.distanceFromCamera;

            transform.position = Vector3.Lerp(transform.position, transform.position = player.transform.position + offsetFinal, Time.deltaTime * focusSpeed);

            if (player.JustTeleported) {
                transform.position = player.transform.position + offsetFinal;
            }
        }
    }
}
