using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject model;
    public float timeToRotate = 3f;
    public float rotationSpeed = 6f;
    
    public GameObject bulletPrefab;
    public float timeToShoot = 1f;
    

    private int targetAngle;
    private float rotationTimer;
    private float shootingTimer;

    void Start()
    {
        rotationTimer = timeToRotate;
    }

    void Update()
    {
        //Updqtes enemies angle
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <- 0f) {
            rotationTimer = timeToRotate;

            targetAngle += 90;
        }
        //Performs enemy rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, targetAngle, 0), Time.deltaTime*rotationSpeed);

        //Shoots bullets
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <- 0f) {
            shootingTimer = timeToShoot;

            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = transform.position + model.transform.forward;
            bulletObject.transform.forward = model.transform.forward;
        }
    }
}
