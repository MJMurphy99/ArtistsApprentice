using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float speed, radius, haltApproachRange, onsetClaustrophobia;
    public StatusEffect[] moveList;
    public StatusEffect currentMove;

    private Vector3 originPoint, currentPos, movePoint;
    private bool moving = false;
    private GameObject[] player;
    private NavMeshAgent enemy;
    private RecoveryTimer timer;
    private float distToGround = 0;

    private void Start()
    {
        timer = GetComponent<RecoveryTimer>();
        originPoint = transform.position;
        currentPos = transform.position;
        player = GameObject.FindGameObjectsWithTag("Player");
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

            if (Vector3.Distance(movePoint, target) <= .5f)
            {
                enemy.isStopped = true;
                currentMove.User = gameObject;
                currentMove.Host = player[0];
                currentMove.Effect();
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
            Vector3 fromOriginToObject = movePoint - originPoint;
            fromOriginToObject *= radius / Vector3.Distance(originPoint, movePoint);
            transform.position = originPoint + fromOriginToObject;

            enemy.isStopped = true;
            timer.AddUpRecoveryTime(originPoint, movePoint, 0);
        }

        
    }

    private Vector3 CalculateStoppingPoint()
    {
        Vector3 dir = (-movePoint + player[0].transform.position).normalized;
        return movePoint + dir * (Vector3.Distance(movePoint, player[0].transform.position) - haltApproachRange);
    }
}





//MovementManager();
//movePoint = transform.position;
//Vector3 target = CalculateStoppingPoint();

//if (Vector3.Distance(originPoint, target) > .5f)
//{
//    enemy.SetDestination(target);
//    enemy.isStopped = false;
//}
//else
//{
//    enemy.isStopped = true;
//    currentMove.User = gameObject;
//    currentMove.Host = enemy.GetComponent<Host>();
//    currentMove.Effect();
//    timer.AddUpRecoveryTime(originPoint, movePoint, currentMove.cost);
//}


//Ray ray = new Ray(transform.position, Vector2.down);
//RaycastHit2D downHit = Physics2D.Raycast(ray);
//distToGround = downHit.distance;
//print(distToGround + " " + );
