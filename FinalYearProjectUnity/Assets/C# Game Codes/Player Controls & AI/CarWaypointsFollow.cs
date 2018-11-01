using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWaypointsFollow : MonoBehaviour {

    //public GameObject carAI;
    //public Transform target;

    public AICarController aiCarControl;
    public GameObject[] waypoints;
    public float speed;
    public float accuracy;
    public float rotSpeed;
    int currentWP = 0; // what is the current waypoint

    void Start()
    {
        aiCarControl = GetComponent<AICarController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoints();
    }

    void MoveToWaypoints()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        // Get the waypoints transform position within x and z
        // Remember to keep the transform.position.y as stated to avoid the car having wierd behaviour... Must be in line with looking at the waypoint node..
        Vector3 lookAtWaypoint = new Vector3(waypoints[currentWP].transform.position.x, transform.position.y, waypoints[currentWP].transform.position.z);


        Vector3 direction = lookAtWaypoint - transform.position; // we want to get the direction between the waypoint and the car... 

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);


        // turn/rotate the car towards the waypoints
        //aiCarControl.SteerAICar();

        // call our functions in the ai controller base class
        aiCarControl.MoveAICar();
        aiCarControl.UpdateWheelMotions();
        //aiCarControl.SteerAICar();


        //if the direction to the waypoint is smaller than 1
        // or we are close enough to the waypoint..
        // move to the next waypoint
        if (direction.magnitude < accuracy)
        {
            currentWP++;
        }

        // if the current waypoint number is equal to the waypoints maximum length 
        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }



    }
}
