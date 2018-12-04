using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficAIBasic : MonoBehaviour
{

    // TESTING VARIABLES TO GET ACCURACY OF THE AI CAR 
    // TO DO LATER..
    // ADD THE WHEELCOLLIDERS TO AI CARS
    // SECONDLY GET IT TO PATHFIND AFTER TESTING THIS CODE THROUGHLY
    //.........
    public Transform target; // the target the AI will follow
    private Vector3 lookAtTarget; // holds the info to look at the target we should follow...
    private Vector3 distance; // the distance between the player and target it has to follow..

    public float acceleration = 5.0f; // how much torque power can it get
    public float deacceleration = 5.0f; // how much brake power can it get
    public float brakeAngle = 20f; // determines how cautious the car should be when braking
    public float minSpeed = 0.0f; // the minimum speed of the car
    public float maxSpeed = 100.0f; // the maximum speed of the car
    public float rotSpeed = 1.0f; // how much should it rotate
    public float speed = 0.0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GoToTarget();
        BrakingOnTurns();
    }

    void GoToTarget()
    {
        lookAtTarget = new Vector3(target.position.x, transform.position.y, target.transform.position.z);
        distance = lookAtTarget - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distance, transform.up), Time.deltaTime * rotSpeed);
    }

    void BrakingOnTurns()
    {
        // if the angle between the 2 forward vectors the goal and the car's forward vector is greater than 20
        // Then reduce the speed.
        // else continue at the same speed.
        if (Vector3.Angle(target.forward, transform.forward) > brakeAngle && speed > 10)
        {
            speed = Mathf.Clamp(speed - (deacceleration * Time.deltaTime), minSpeed, maxSpeed);
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);
        }

        this.transform.Translate(0, 0, speed);
    }
}

