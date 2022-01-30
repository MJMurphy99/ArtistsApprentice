using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AppliedVelocity : StatusEffect
{
    public float speed, magnitude;

    // Start is called before the first frame update
    public override void Effect()
    {
        EnemyMovement m = Host.GetComponent<EnemyMovement>();

        //Vector3 currentPos = Host.body.transform.position;
        //Vector3 movePoint = currentPos + (currentPos - User.transform.position).normalized * magnitude;

        //m.StartCoroutine(m.MovingBetweenPoints(currentPos, movePoint, speed));
    }
}
