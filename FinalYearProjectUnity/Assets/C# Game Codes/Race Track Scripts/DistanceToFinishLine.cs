﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceToFinishLine : MonoBehaviour
{
    private PlayerScoreManager psm;
    public Text progressionText;
    public GameObject playerCarBumper;
    public GameObject[] waypoints;
    public GameObject currentWayPoint;

    public float distanceToWaypoint;
    private float lengthOfTrack;
    private float completed = 0; // how much have you completed of the track
    private float raycastLength = 20f;
    private int currentWaypointIndex = 0;
    public bool completedFinish = false;

    // Use this for initialization
    void Start()
    {
        GetLengthOfTrack();
    }

    // Update is called once per frame
    void Update()
    {        
        if (!completedFinish)
        {
            DistanceToWaypointNodes();
            RaycastToFinish();
        }
        DisplayProgressionUI();
    }

    void GetLengthOfTrack()
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
                lengthOfTrack += Vector3.Distance(previousWaypoint.transform.position, WP.transform.position);
            }

            previousWaypoint = WP;
        }

        //initialize the currentwaypoint to the array index of the last item in the array
        currentWayPoint = waypoints[waypoints.Length - 1];

        //Debug.Log(lengthOfTrack);
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

    void DisplayProgressionUI()
    {
        progressionText.text = "Progress: " + (int)completed + " %";
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        TriggerWaypoints(other);
    }

    void TriggerWaypoints(Collider other)
    {
        // if the player touches the start line 
        // they can start incrementing their score by driving forward
        // start line bool is responsible for incrementing score till the finish line reached...

        if (other.gameObject.tag == "StartLine")
        {
            Debug.Log("Triggered " +other.gameObject.name);
            other.gameObject.GetComponent<Collider>().enabled = false; // disable collider
            currentWaypointIndex++; // go and find the finish line 
            psm = GetComponent<PlayerScoreManager>();
            psm.hasTriggeredStartLine = true; // set the bool to be true to start incrementing score
        }

        // if the player touches the finish line
        // disable the start line bool which stops incrementing score
        // set the race completion bool to true
        // as long as the race has been 100% complete 

        if (other.gameObject.name == "FinishLine")
        {
            psm = GetComponent<PlayerScoreManager>();
            Debug.Log("You win!");
            other.gameObject.GetComponent<Collider>().enabled = false;
            completedFinish = true; // the race is now finished
            psm.hasTriggeredStartLine = false; // we are no longer incrementing score from when we hit the start line
            completed = 100f;
        }
    }
}