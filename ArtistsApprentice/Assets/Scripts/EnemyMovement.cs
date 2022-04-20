using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //Entity Attributes
    [Header("[Relevant]")]
    public float speed, radius;
    public int attackMod, recoveryMod;

    [Header("[Extra]")]
    public float haltApproachRange;
    public StatusEffect[] moveList;
    public StatusEffect currentMove;
    public Transform pivot;

    //Referenced Scripts
    public CheckRange cr;
    public Party p;
    private NavMeshAgent enemy;
    private RecoveryTimer timer;

    //Movement Management
    private GameObject targetPlayer;
    private Vector3 targetPlayerPos;
    private Vector3 originPoint, movePoint;

    private void Start()
    {
        timer = GetComponent<RecoveryTimer>();
        originPoint = transform.position;
        targetPlayer = ChooseTarget();
        targetPlayerPos = targetPlayer.transform.position;
        enemy = GetComponent<NavMeshAgent>();
        currentMove = Instantiate(moveList[0]);
        currentMove.val += attackMod;
        currentMove.cost += recoveryMod;
    }

    private void Update()
    {
        movePoint = transform.position;

        if (targetPlayer == null) targetPlayer = ChooseTarget();

        if (timer.recoveryTime == 0)
        {
            MovementManager();
            Vector3 target = CalculateStoppingPoint();
            enemy.SetDestination(target);

            if (cr.targets.Count > 0 && CheckForPlayers())
            { 
                enemy.isStopped = true;
                Fire(cr.targets);
                timer.AddUpRecoveryTime(originPoint, movePoint, currentMove.cost);
            }
        }
    }

    private bool CheckForPlayers()
    {
        foreach(GameObject g in cr.targets)
        {
            if (g != null)
            {
                if (g.CompareTag("Player")) return true;
            }
            else cr.Clear();
        }

        return false;
    }

    private void MovementManager()
    {
        if (enemy.isStopped)
        {
            originPoint = movePoint;
            enemy.isStopped = false;
        }   

        //Bind entity to radius and end turn
        if (Vector3.Distance(originPoint, movePoint) >= radius)
        {
            RotateAttackRadius();
            Vector3 fromOriginToObject = movePoint - originPoint;
            fromOriginToObject *= radius / Vector3.Distance(originPoint, movePoint);
            transform.position = originPoint + fromOriginToObject;

            enemy.isStopped = true;
            timer.AddUpRecoveryTime(originPoint, movePoint, 0);
        }        
    }

    private void Fire(List<GameObject> targets)
    {
        StartCoroutine("FadeAttackHUD");
        foreach(GameObject player in targets)
        {
            currentMove.User = gameObject;
            currentMove.Host = player;
            currentMove.Effect();
        } 
    }

    private Vector3 CalculateStoppingPoint()
    {
        targetPlayerPos = targetPlayer != null ? targetPlayer.transform.position : movePoint;
        Vector3 dir = (-movePoint + targetPlayerPos).normalized;
        return movePoint + dir * (Vector3.Distance(movePoint, targetPlayerPos) - haltApproachRange);
    }

    public GameObject ChooseTarget()
    {
        float shortestDistance = 100;
        GameObject closestPlayer = null;

        GameObject[] players = p.party.ToArray();
        for (int i = 0; i < players.Length; i++)
        {
            float d = Vector3.Distance(players[i].transform.position, transform.position);
            if (d < shortestDistance)
            {
                shortestDistance = d;
                closestPlayer = players[i];
            }
        }

        return closestPlayer;
    }

    private void RotateAttackRadius()
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

    private IEnumerator FadeAttackHUD()
    {
        SpriteRenderer sr = cr.gameObject.GetComponent<SpriteRenderer>();
        sr.color += new Color(0, 0, 0, 160 / 255f);

        while(sr.color.a > 0)
        {
            sr.color -= new Color(0, 0, 0, 20 / 255f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
