using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public CheckRange cr;
    public float speed, radius, haltApproachRange, onsetClaustrophobia;
    public StatusEffect[] moveList;
    public StatusEffect currentMove;
    public Transform pivot;

    private Vector3 originPoint, currentPos, movePoint;
    private bool moving = false;
    private Vector3 targetPlayerPos;
    private NavMeshAgent enemy;
    private RecoveryTimer timer;
    private float distToGround = 0;

    private void Start()
    {
        timer = GetComponent<RecoveryTimer>();
        originPoint = transform.position;
        currentPos = transform.position;
        targetPlayerPos = ChooseTarget();
        enemy = GetComponent<NavMeshAgent>();
        currentMove = moveList[0];
    }

    private void Update()
    {
        movePoint = transform.position;

        if (timer.recoveryTime == 0)
        {
            MovementManager();
            Vector3 target = CalculateStoppingPoint();
            enemy.SetDestination(target);

            if (cr.targets.Count > 0)
            { 
                enemy.isStopped = true;
                Fire(cr.targets);
                timer.AddUpRecoveryTime(originPoint, movePoint, currentMove.cost);
            }
        }
    }

    private void MovementManager()
    {
        if (enemy.isStopped)
        {
            originPoint = movePoint;
            enemy.isStopped = false;
        }   

        if (Vector3.Distance(originPoint, movePoint) >= radius)
        {
            MouseScreenAngle();
            Vector3 fromOriginToObject = movePoint - originPoint;
            fromOriginToObject *= radius / Vector3.Distance(originPoint, movePoint);
            transform.position = originPoint + fromOriginToObject;

            enemy.isStopped = true;
            timer.AddUpRecoveryTime(originPoint, movePoint, 0);
        }        
    }

    private void Fire(List<GameObject> targets)
    {
        foreach(GameObject player in targets)
        {
            currentMove.User = gameObject;
            currentMove.Host = player;
            currentMove.Effect();
        } 
    }

    private Vector3 CalculateStoppingPoint()
    {
        Vector3 dir = (-movePoint + targetPlayerPos).normalized;
        return movePoint + dir * (Vector3.Distance(movePoint, targetPlayerPos) - haltApproachRange);
    }

    private Vector3 ChooseTarget()
    {
        float shortestDistance = 100;
        Vector3 closestPlayerPos = transform.position;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            float d = Vector3.Distance(players[i].transform.position, transform.position);
            if (d < shortestDistance)
            {
                shortestDistance = d;
                closestPlayerPos = players[i].transform.position;
            }
        }

        return closestPlayerPos;
    }

    private void MouseScreenAngle()
    {
        Vector2 playerAngle = 
            new Vector2(targetPlayerPos.x - transform.position.x, targetPlayerPos.z - transform.position.z);

        float opp = playerAngle.x;
        float adj = playerAngle.y;

        float theta = Mathf.Atan(adj / opp) * Mathf.Rad2Deg;

        if (opp < 0 && adj > 0) theta += 180;
        else if (opp < 0 && adj < 0) theta += 180;
        else if (opp > 0 && adj < 0) theta += 360;

        pivot.localRotation = Quaternion.Euler(new Vector3(0, -theta, 0));
    }
}
