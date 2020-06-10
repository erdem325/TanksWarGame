using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    public Transform[] wayPoints;
    public Transform rayOrigin;
    int index = 0;
    Animator fsm; 
    Vector3[] wayPointsPos = new Vector3[3];
    float shootFreq = 5f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPointsPos[i] = wayPoints[i].position;
        }
        fsm = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPointsPos[index]);

        StartCoroutine("CheckPlayer");
    }
    IEnumerator CheckPlayer()
    {
        CheckVisibility();
        CheckDistance();
        CheckDistanceFromCurrentWaypoint();
        yield return new WaitForSeconds(0.1f);
        yield return CheckPlayer();
    }

    private void CheckDistanceFromCurrentWaypoint()
    {
        float distance = Vector3.Distance(wayPointsPos[index], rayOrigin.position);
        

        fsm.SetFloat("distanceFromCurrentWaypoint", distance);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(player.position, rayOrigin.position);
        fsm.SetFloat("distance", distance);
    }

    private void CheckVisibility()
    {
        float maxDistance = 25;
        Vector3 direction = (player.position - rayOrigin.position).normalized;
        Debug.DrawRay(rayOrigin.position, direction * maxDistance, Color.red);

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit info, maxDistance))
        {
            if (info.transform.tag == "Player")
                fsm.SetBool("isVisible", true);

            else
                fsm.SetBool("isVisible", false);
        }
        else
            fsm.SetBool("isVisible", false);
    }
    public void SetLoookRotation()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,0.2f);

    }

    public void Shoot()
    {
        GetComponent<ShootBehavior>().Shoot(shootFreq);
    }

    public void Patrol()
    {
        Debug.Log("patrolling...");
    }

    public void Chase()
    {
        agent.SetDestination(player.position);
    }

    public void SetNewWayPoint()
    {
        switch (index)
        {
            case 0:
                index = 1;
                break;
            case 1:
                index = 2;
                break;
            case 2:
                index = 0;
                break;
        }
        agent.SetDestination(wayPointsPos[index]);
    }
}
