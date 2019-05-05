using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject model;
    public float timeToRotate = 1f;
    public float rotationSpeed = 6f;
    public GameObject bulletPrefab;
    public float timeToShoot = 0.2f;


    private int targetAngle;
    private float rotationTimer;
    private float shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        rotationTimer = timeToRotate;
        shootingTimer = timeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the enemy's angle
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0f) {
            rotationTimer = timeToRotate;

            targetAngle += 45;
        }

        // Perform the enemy rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, targetAngle, 0), rotationSpeed * Time.deltaTime);

        // Shoot bullets
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f)
        {
            shootingTimer = timeToShoot;

            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = transform.position + model.transform.forward * 4 + model.transform.up * 0.4f;
            bulletObject.transform.forward = model.transform.forward;  
        }
    }
}
