using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour {

    public float motorTorquePowerAI = 100f; // the car AI variant variable factor for speed.
    public float brakeTorquePowerAI = 50.0f; // how much brake power can it get
    public float steeringAngle;
    public float maxSteerAngle = 30;
    public float brakeAngle = 30f; // determines how cautious the car should be when braking
    public float minSpeed = 0.0f; // the minimum speed of the car
    public float maxSpeed = 100.0f; // the maximum speed of the car
    public float rotSpeed = 1.0f; // how much should it rotate

    public WheelCollider wheelFrontLeft, wheelFrontRight;
    public WheelCollider wheelRearLeft, wheelRearRight;

    public Transform transformWheelFrontLeft, transformWheelFrontRight;
    public Transform transformWheelRearLeft, transformWheelRearRight;

    Vector3 wheelPos;
    Quaternion wheelRot;


    public void MoveAICar()
    {
        wheelFrontLeft.motorTorque = motorTorquePowerAI;
        wheelFrontRight.motorTorque = motorTorquePowerAI;
        wheelRearLeft.motorTorque = motorTorquePowerAI;
        wheelRearRight.motorTorque = motorTorquePowerAI;
    }

    public void SteerAICar()
    {
        // set the steering angle to the maximum steer angle times by the horizontal movement variable
        // Assign them to the left and right wheels.
        steeringAngle = maxSteerAngle;
        wheelFrontLeft.steerAngle = steeringAngle;
        wheelFrontRight.steerAngle = steeringAngle;
    }

    public void UpdateWheelMotions()
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
