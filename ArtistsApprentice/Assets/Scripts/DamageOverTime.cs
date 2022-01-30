using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DamageOverTime : StatusEffect
{
    public float frequency;

    public override void Effect()
    {
        //Host.StartCoroutine(Host.PersistentDamage(duration, frequency, val));
    }

}
