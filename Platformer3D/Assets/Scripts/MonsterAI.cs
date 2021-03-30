using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Range(0.5f, 50)]
    public float detectDistance = 5;
    public Transform[] points;
    int destinationIndex = 0;

    NavMeshAgent agent;
    Transform player;

    float walkSpeed;
    float runSpeed = 2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.destination = points[destinationIndex].position;
            walkSpeed = agent.speed;
        }
    }

    private void Update()
    {
        SetMobSize();
        SearchPlayer();
        Walk();
    }

    public void SetMobSize()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectDistance + 2)
        {
            iTween.ScaleTo(gameObject, Vector3.one, 0.5f);
        }
    }

    public void SearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectDistance)
        {
            // Le joueur est détecté
            agent.destination = player.position;
            agent.speed = runSpeed;
            detectDistance = 10;
        }
        else
        {
            agent.destination = points[destinationIndex].position;
            agent.speed = walkSpeed;
            detectDistance = 5;
        }
    }

    public void Walk()
    {
        float dist = agent.remainingDistance;
        if (dist <= 0.05f)
        {
            destinationIndex++;
            if (destinationIndex > points.Length - 1)
            {
                destinationIndex = 0;
            }
            agent.destination = points[destinationIndex].position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
