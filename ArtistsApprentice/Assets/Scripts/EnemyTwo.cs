using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTwo : MonoBehaviour
{
    public GameObject checkPosition;
    public float speed, radius, haltApproachRange, onsetClaustrophobia;
    public StatusEffect[] moveList;
    public StatusEffect currentMove;

    private Vector3 originPoint, targetDestination, movePoint;
    private bool running = false;
    private GameObject[] targetable;
    private GameObject targetObject;
    private NavMeshAgent enemy;
    private RecoveryTimer timer;

    private void Start()
    {
        timer = GetComponent<RecoveryTimer>();
        originPoint = transform.position;
        targetable = GameObject.FindGameObjectsWithTag("Player");
        targetObject = targetable[0];
        enemy = GetComponent<NavMeshAgent>();
        enemy.SetDestination(targetObject.transform.position);
        currentMove = moveList[0];
    }

    private void Update()
    {
        SetTargetDestination();
        MovementManager();


        if (timer.recoveryTime == 0)
        {
            movePoint = transform.position;
            enemy.isStopped = false;

            //if (Vector3.Distance(originPoint, targetDestination) > 0.5f)
            //    Movement();
            if (Vector3.Distance(originPoint, targetDestination) <= 0.5f)
            {
                originPoint = movePoint;
                currentMove.User = gameObject;
                currentMove.Host = targetObject;
                currentMove.Effect();
                timer.AddUpRecoveryTime(originPoint, movePoint, currentMove.cost);
            }
        }
    }

    private void MovementManager()
    {
        if (enemy.isStopped)
        {
            enemy.SetDestination(targetDestination);
            originPoint = movePoint;
        }

        if (Vector3.Distance(originPoint, movePoint) >= radius)
        {
            Vector3 fromOriginToObject = movePoint - originPoint;
            fromOriginToObject *= radius / Vector3.Distance(originPoint, movePoint);
            transform.position = originPoint + fromOriginToObject;

            enemy.isStopped = true;
            movePoint = transform.position;
            timer.AddUpRecoveryTime(originPoint, movePoint, 0);
        }
    }

    private Vector3 Disengage()
    {
        Vector3 dir = (movePoint + targetObject.transform.position).normalized;
        float opp = dir.x;
        float adj = dir.y;

        float incomingDir = Mathf.Atan(adj / opp) * Mathf.Rad2Deg;

        if (opp < 0 && adj > 0) incomingDir += 180;
        else if (opp < 0 && adj < 0) incomingDir += 180;
        else if (opp > 0 && adj < 0) incomingDir += 360;


        float s = 1.0f, r = haltApproachRange, theta;
        theta = s / r * Mathf.Rad2Deg;

        float j = targetObject.transform.position.x, k = targetObject.transform.position.z;
        float x = 0, z = 0;
        bool foundEscape = false;

        for (int i = 1; i <= 180 / theta; i++)
        {
            x = r * Mathf.Cos(AdjustAngle(incomingDir + 90 + (theta * i))) + j;
            z = r * Mathf.Sin(AdjustAngle(incomingDir + 90 + (theta * i))) + k;

            //Y val will need to be changed to account for slopes in the future
            checkPosition.transform.position = new Vector3(x, transform.position.y, z);

            if (!checkPosition.GetComponent<CheckPosForObstruction>().Occupied)
            {
                foundEscape = true;
                break;
            }
        }

        return foundEscape == true ? new Vector3(x, transform.position.y, z) : transform.position;
    }

    private void SetTargetDestination()
    {
        bool ifTooClose = Vector3.Distance(targetObject.transform.position, transform.position) <= haltApproachRange / 2;

        if (Vector3.Distance(targetObject.transform.position, transform.position) > haltApproachRange)
        {
            targetDestination = CalculateStoppingPoint();
        }
        else if (ifTooClose)
        {
            targetDestination = Disengage();
            running = true;
        }
    }

    private Vector3 CalculateStoppingPoint()
    {
        //Possibly change. Right now, this is what causes the enemy to go the extra distance to stop infront
        //of the player instead of the closest point it reaches
        Vector3 dir = (movePoint + targetObject.transform.position).normalized;
        return dir * (Vector3.Distance(movePoint, targetObject.transform.position) - haltApproachRange);
    }

    private float AdjustAngle(float theta)
    {
        if (theta >= 360) theta = theta - 360;
        else if (theta < 0) theta = 360 + theta;

        return theta;
    }
}
