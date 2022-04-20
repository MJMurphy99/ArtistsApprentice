using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRange : MonoBehaviour
{
    public GameObject user;
    private SpriteRenderer sr;
    private Color occupied, empty;
    public List<GameObject> targets = new List<GameObject>();

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        occupied = new Color(0, 1, 0, .6f);
        empty = new Color(1, 0, 0, .6f);
        if (user.CompareTag("Player")) sr.color = empty;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.transform.CompareTag("Untargetable"))
        {
            if(sr.color == empty && user.CompareTag("Player")) sr.color = occupied;
            if(targets.Find((GameObject g) => { return g == col.gameObject; }) == null)
                targets.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (!col.transform.CompareTag("Untargetable"))
        {
            targets.Remove(col.gameObject);
            if (targets.Count == 0 && user.CompareTag("Player")) sr.color = empty;
        };
    }

    public void Clear()
    {
        targets = new List<GameObject>();
    }
}
