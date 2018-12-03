using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToFinishLine : MonoBehaviour
{

    public GameObject playerCarBumper;
    public GameObject[] waypoints;
    public GameObject currentWayPoint;
    public float distanceToWaypoint;
    float lengthOfTrack;
    float completed = 0; // how much have you completed of the track
    bool completedFinish = false;
    int currentWaypointIndex = 0;
    float raycastLength = 20f;

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
        currentWayPoint = waypoints[waypoints.Length-1];


        //Debug.Log(lengthOfTrack);
    }

    // Update is called once per frame
    void Update()
    {        
        if (!completedFinish)
        {
            DistanceToWaypointNodes();
            RaycastToFinish();
        }


        Debug.Log(completed);
    }

    void RaycastToFinish()
    {
        RaycastHit rayhit;
        Ray newRay = new Ray(playerCarBumper.transform.position, transform.forward);
       
        if (Physics.Raycast(newRay , out rayhit, raycastLength))
        {
            if (rayhit.collider.gameObject.name == "FinishLine")
            {
                distanceToWaypoint = Vector3.Distance(playerCarBumper.transform.position, rayhit.point);
                completed = 100 - (100 * distanceToWaypoint / lengthOfTrack);
                completed = Mathf.Clamp(completed, 0, 100);
            }
            Debug.DrawLine(playerCarBumper.transform.position, rayhit.point);
        }
        else
            Debug.DrawRay(newRay.origin, newRay.direction  * raycastLength, Color.red);
    }

    void DistanceToWaypointNodes()
    {
        //gets the distance between the first waypoint and the player
        distanceToWaypoint = Vector3.Distance(playerCarBumper.transform.position, currentWayPoint.transform.position);
        completed = 100 - (100 * distanceToWaypoint / lengthOfTrack);
        completed = Mathf.Clamp(completed, 0, 100);
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        TriggerWaypoints(other);
    }

    void TriggerWaypoints(Collider other)
    {
        if ((other.gameObject.tag == "Waypoint") /*&& (completed <= 30)*/)
        {
            Debug.Log("Triggered " +other.gameObject.name);
            other.gameObject.GetComponent<Collider>().enabled = false;
            currentWaypointIndex++;
            //currentWayPoint = waypoints[currentWaypointIndex];
        }

        if (other.gameObject.name == "FinishLine")
        {
            completedFinish = true;
            completed = 100f;
            Debug.Log("You win!");
        }
    }
}