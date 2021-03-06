﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingLogic : MonoBehaviour
{

    public Vector3[] directions;
    public float timeToChange = 1f;
    public float movementSpeed = 10f;
    private int directionPointer;
    private float directionTimer;

    void Start()
    {
        directionPointer = 0;
        directionTimer = timeToChange;
    }

    void Update()
    {
        //Changing the direction.
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0f) {
            directionTimer = timeToChange;
            directionPointer++;
            if (directionPointer >= directions.Length) {
                directionPointer = 0;
            }
        }
        //Make the object move
        GetComponent<Rigidbody>().velocity = new Vector3 (
            directions[directionPointer].x * movementSpeed,
            GetComponent<Rigidbody>().velocity.y,
            directions[directionPointer].z * movementSpeed
        );
    }
}
