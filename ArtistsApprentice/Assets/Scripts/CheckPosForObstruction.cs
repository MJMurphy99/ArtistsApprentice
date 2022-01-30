using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosForObstruction : MonoBehaviour
{
    private bool occupied = false;

    public bool Occupied
    {
        get { return occupied; }
    }

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("AllyEnemy")) occupied = true;
    }
}
