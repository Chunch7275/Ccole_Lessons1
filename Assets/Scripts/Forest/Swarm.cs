using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swarm : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waypoints;
    private int waypointindex;

    [SerializeField]
    private int waypoint_threshold = 1;

    private NavMeshAgent agent;

    private Bot bot;

    private bool HiveNotPickedUp = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        bot = GetComponent<Bot>();

        HivePickUp.HivePickedUp += OnHivePickedUp;

        agent.SetDestination(waypoints[0].transform.position);
    }

    private void OnHivePickedUp()
    {
        HiveNotPickedUp = false;
    }

    // Update is called once per frame
    public void Patrol()
    {
        if (Vector3.Distance(transform.position, waypoints[waypointindex].transform.position) < waypoint_threshold)
        {
            waypointindex++;

            if (waypointindex == waypoints.Count)
            {
                waypointindex = 0;
            }
        }

        agent.SetDestination(waypoints[waypointindex].transform.position);
    }
    void Update()
    {
        if (HiveNotPickedUp)
        {
            Patrol();
        } else
        {
            bot.Pursue();
        }
    }
}
