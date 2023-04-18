using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform patrolRout;
    public List<Transform> locations;
    public Transform player;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int _lives = 3;
    public int Enemylives
    {
        get { return _lives; }
        private set 
        {
            _lives = value;

            if(_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRout();
        MoveToNextPatrolLocation();
    }

    private void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    private void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        {
            return;
        }

        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    private void InitializePatrolRout()
    {
        foreach(Transform shild in patrolRout)
        {
            locations.Add(shild);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            agent.destination = player.position;
        Debug.Log("Player detected. Attack!");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            Debug.Log("Player out of range, resume patrol"); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            Enemylives -= 1;
            Debug.Log("Critical hit.");
        }
    }
}
