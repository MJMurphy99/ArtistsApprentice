using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject mainBody;
    public float speed, radius;
    public CheckRange cr;

    private static Vector3 movePoint, originPoint;
    private RecoveryTimer timer;
    private AimAttack aa;

    // Start is called before the first frame update
    void Start()
    {
        aa = GetComponent<AimAttack>();
        timer = GetComponent<RecoveryTimer>();
        originPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (timer.recoveryTime == 0)
        {
            transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
            transform.position += Vector3.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            movePoint = transform.position;

            if (Vector3.Distance(originPoint, movePoint) >= radius)
            {
                Vector3 fromOriginToObject = movePoint - originPoint; 
                fromOriginToObject *= radius / Vector3.Distance(originPoint, movePoint); 
                transform.position = originPoint + fromOriginToObject; 
                
                movePoint = transform.position;
            }
        }
    }

    public void EndTurn()
    {
        mainBody.transform.position = transform.position;
        transform.localPosition = Vector3.zero;
        int cost;

        if (aa.targetLocked)
            cost = cr.currentMove.cost;
        else cost = 0;

        timer.AddUpRecoveryTime(originPoint, movePoint, cost);
        originPoint = movePoint;
    }
}
