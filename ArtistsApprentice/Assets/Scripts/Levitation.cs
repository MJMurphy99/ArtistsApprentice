using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Levitation : StatusEffect
{
    public float magnitude;
    public override void Effect()
    {
        //Host.body.transform.position += Vector3.up * magnitude;
    }
}
