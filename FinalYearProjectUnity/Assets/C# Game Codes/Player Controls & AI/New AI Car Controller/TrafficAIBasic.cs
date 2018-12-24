﻿using System.Collections;
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
    private Vector3 wheelPos;
    private Quaternion wheelRot;

    public Rigidbody rb;
    public Transform transformWheelFrontLeft, transformWheelFrontRight;
    public Transform transformWheelRearLeft, transformWheelRearRight;
    public WheelCollider wheelFrontLeft, wheelFrontRight;
    public WheelCollider wheelRearLeft, wheelRearRight;

    public float acceleration = 5.0f; // how much torque power can it get
    public float deacceleration = 5.0f; // how much brake power can it get
    public float brakeAngle = 20f; // determines how cautious the car should be when braking
    public float minSpeed = 0.0f; // the minimum speed of the car
    public float maxSpeed = 100.0f; // the maximum speed of the car
    public float rotSpeed = 1.0f; // how much should it rotate
    private float speed = 0.0f;

    public float torquePower = 100f; // the car AI variant variable factor for speed.
    public float brakeTorquePower = 50.0f; // how much brake power can it get
    public float steeringAngle;
    public float maxSteerAngle = 30;
    private float steering;
    public float CurrentSteerAngle { get { return steeringAngle; } }
    private float m_OldRotation;
    [Range(0, 1)] [SerializeField] private float m_SteerHelper;
    [SerializeField] private float m_SteerSensitivity = 0.05f;
    public float CurrentSpeed { get { return rb.velocity.magnitude * 2.23693629f; } }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.9f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCar(speed);
        UpdateWheelMotions();
        GoToTarget();
        Debug.Log(CurrentSpeed);
        //BrakingOnTurns();
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
        //if (Vector3.Angle(target.forward, transform.forward) > brakeAngle && speed > 10)
        //{
        //    speed = Mathf.Clamp(speed - (deacceleration * Time.deltaTime), minSpeed, maxSpeed);
        //    Debug.Log("Braking");
        //}
        //else
        //{
        //    speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);
        //}

        if (Vector3.Angle(target.forward, transform.forward) > brakeAngle && CurrentSpeed > 25)
        {
            speed = Mathf.Clamp(speed - (brakeTorquePower * Time.deltaTime), minSpeed, maxSpeed);
            Debug.Log("Braking");
        }
        else
        {
            speed = Mathf.Clamp(speed + (torquePower * Time.deltaTime), minSpeed, maxSpeed);
        }

        //this.transform.Translate(0, 0, speed);
        MoveCar(speed);
    }

    void MoveCar(float speed)
    {

        wheelPos = transform.position;
        wheelRot = transform.rotation;

        wheelFrontLeft.GetWorldPose(out wheelPos, out wheelRot);
        wheelFrontRight.GetWorldPose(out wheelPos, out wheelRot);
        wheelRearLeft.GetWorldPose(out wheelPos, out wheelRot);
        wheelRearRight.GetWorldPose(out wheelPos, out wheelRot);

        wheelFrontLeft.motorTorque = torquePower;
        wheelFrontRight.motorTorque = torquePower;
        wheelRearLeft.motorTorque = torquePower;
        wheelRearRight.motorTorque = torquePower;

        //      Steering stuff       //

        steering = Mathf.Clamp(steering, -1, 1);
        steeringAngle = steering * maxSteerAngle;
        wheelFrontLeft.steerAngle = steeringAngle;
        wheelFrontRight.steerAngle = steeringAngle;

        //SteeringHelp();
        WheelMeshPosRot();
    }

    void WheelMeshPosRot()
    {
        transformWheelFrontLeft.transform.position = wheelPos;
        transformWheelFrontRight.transform.position = wheelPos;
        transformWheelRearLeft.transform.position = wheelPos;
        transformWheelRearRight.transform.position = wheelPos;

        transformWheelFrontLeft.transform.rotation = wheelRot;
        transformWheelFrontRight.transform.rotation = wheelRot;
        transformWheelRearLeft.transform.rotation = wheelRot;
        transformWheelRearRight.transform.rotation = wheelRot;
    }

    void SteeringHelp()
    {
        WheelHit wheelHit;

        wheelFrontLeft.GetGroundHit(out wheelHit);
        wheelFrontRight.GetGroundHit(out wheelHit);
        wheelRearLeft.GetGroundHit(out wheelHit);
        wheelRearRight.GetGroundHit(out wheelHit);

        if (wheelHit.normal == Vector3.zero)
        {
            return;
        }

        // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
        if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_SteerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
            rb.velocity = velRotation * rb.velocity;
        }
        m_OldRotation = transform.eulerAngles.y;
    }

    void UpdateWheelMotions()
    {
        // This method updates all the wheel colliders and the wheel mesh in real time as they ride together.
        UpdateWheelMotion(wheelFrontLeft, transformWheelFrontLeft);
        UpdateWheelMotion(wheelFrontRight, transformWheelFrontRight);
        UpdateWheelMotion(wheelRearLeft, transformWheelRearLeft);
        UpdateWheelMotion(wheelRearRight, transformWheelRearRight);
    }


    void UpdateWheelMotion(WheelCollider wheelC, Transform wheelT)
    {
        wheelPos = transform.position;
        wheelRot = transform.rotation;

        // This gets the information of the wheel collider and transform
        // Returns the infromation with GetWorldPos
        // To align the wheels correctly to spin the right way 
        wheelC.GetWorldPose(out wheelPos, out wheelRot);

        // Then we pass the information into our method parameters...
        // Both position and rotation.
        wheelT.transform.position = wheelPos;
        wheelT.transform.rotation = wheelRot;
    }
}

