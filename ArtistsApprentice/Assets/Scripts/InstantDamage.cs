using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InstantDamage : StatusEffect
{
    public override void Effect()
    {
        Health h = Host.GetComponent<Health>();
        if (h.ChangeHealth(val)) ClearCheckRange();
    }

    private void ClearCheckRange()
    {
        if (User.CompareTag("Enemy"))
        {
            EnemyMovement em = User.GetComponent<EnemyMovement>();
            em.cr.Clear();
        }
        else
            User.GetComponent<Movement>().cr.Clear();
    }
}
