using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InstantDamage : StatusEffect
{
    public override void Effect()
    {
        Health h = Host.GetComponent<Health>();
        h.ChangeHealth(val);
    }
}
