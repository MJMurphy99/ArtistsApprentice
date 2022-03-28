using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color occupied, empty;
    public List<GameObject> targets = new List<GameObject>();

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        occupied = new Color(0, 1, 0, .6f);
        empty = new Color(1, 0, 0, .6f);
        sr.color = empty;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.transform.CompareTag("Untargetable"))
        {
            if(sr.color == empty) sr.color = occupied;
            targets.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (!col.transform.CompareTag("Untargetable"))
        {
            targets.Remove(col.gameObject);
            if (targets.Count == 0) sr.color = empty;
        };
    }
}
