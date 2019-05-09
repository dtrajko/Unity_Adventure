using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject model;
    public float timeToRotate = 1.5f;
    public float timeToShoot = 0.5f;
    public float rotationSpeed = 6f;
    public bool rotateClockwise = true;
    public int startingAngle = 0;
    public GameObject bulletPrefab;

    private int targetAngle;
    private float rotationTimer;
    private float shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        base.health = 5;
        rotationTimer = timeToRotate;
        shootingTimer = timeToShoot;

        targetAngle = startingAngle;
        transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the enemy's angle
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0f) {
            rotationTimer = timeToRotate;

            targetAngle += rotateClockwise ? 90 : -90;
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
