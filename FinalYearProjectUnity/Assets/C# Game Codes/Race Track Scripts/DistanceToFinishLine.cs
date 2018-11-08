using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToFinishLine : MonoBehaviour
{

    public GameObject playerCar;
    public GameObject[] waypoints;
    public GameObject currentWayPoint;
    public float distanceToWaypoint;
    float lengthOfTrack;
    float completed; // how much have you completed of the track



    // Use this for initialization
    void Start()
    {

        //previous checkpoint set to null
        // length of track set to 0 for check testing.
        GameObject previousWaypoint = null;
        lengthOfTrack = 0;

        // loop through the waypoints in the track and get the distance of the whole track
        foreach (GameObject WP in waypoints)
        {
            Debug.Log(WP);
            if (previousWaypoint != null)
            {
                //lengthOfTrack += Vector3.Distance(WP.transform.position, previousWaypoint.transform.position);
                lengthOfTrack += Vector3.Distance(previousWaypoint.transform.position, WP.transform.position);
            }

            previousWaypoint = WP;
        }

        //initialize the currentwaypoint to the array index of the first item in the array
        // currentWayPoint = waypoints[0];
        currentWayPoint = waypoints[3];


        //Debug.Log(lengthOfTrack);
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToWaypointNodes();
    }

    void DistanceToWaypointNodes()
    {
        //gets the distance between the first waypoint and the player
        distanceToWaypoint = Vector3.Distance(playerCar.transform.position, currentWayPoint.transform.position);
        completed = distanceToWaypoint / lengthOfTrack;
        Debug.Log(100 * completed);
    }


    void OnTriggerEnter(Collider other)
    {
        TriggerWaypoints(other);
    }

    void TriggerWaypoints(Collider other)
    {
        if ((other.gameObject.name == "Waypoint1") && (completed <= 30))
        {
            Debug.Log("Triggered waypoint one");
            other.gameObject.GetComponent<Collider>().enabled = false;
            //currentWayPoint = waypoints[1];
        }


        if ((other.gameObject.name == "Waypoint2") && (completed <= 30))
        {
            Debug.Log("Triggered waypoint two");
            other.gameObject.GetComponent<Collider>().enabled = false;
            //currentWayPoint = waypoints[2];
        }


        if ((other.gameObject.name == "Waypoint3") && (completed <= 30))
        {
            Debug.Log("Triggered waypoint three");
            other.gameObject.GetComponent<Collider>().enabled = false;
            //currentWayPoint = waypoints[3];
        }

        if ((other.gameObject.name == "Waypoint4") && (completed <= 30))
        {
            Debug.Log("Triggered waypoint four");
            other.gameObject.GetComponent<Collider>().enabled = false;
            
            //if (waypoints.Length >= 4)
            //{
            //    Debug.Log("Finished");
            //    return;
            //}
        }

    }





    //if(other.bounds.Contains(playerCar.transform.position))
    //{
    //    currentWayPoint = waypoints[1];
    //}
}