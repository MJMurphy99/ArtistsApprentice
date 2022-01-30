using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CheckRange : MonoBehaviour
{
    public StatusEffect[] moveList;
    public Image img;
    public GameObject body;
    public StatusEffect currentMove;

    private SpriteRenderer sr;
    private Color occupied, empty;
    private GameObject enemy;
    private int scroll = 0, delta = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        occupied = new Color(0, 1, 0, .6f);
        empty = new Color(1, 0, 0, .6f);
        currentMove = moveList[0];
        img.sprite = currentMove.effectIcon;
        sr.color = empty;
    }

    private void Update()
    {
        //delta = (int)Input.mouseScrollDelta.y;
        //if (delta != 0)
        //    ChangeMove(); 

        Fire();
    }

    private void OnTriggerStay(Collider col)
    {
        if (!col.transform.CompareTag("Untargetable"))
        {
            sr.color = occupied;
            enemy = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //TODO: make it check if empty rather than if something exited
        if (!col.transform.CompareTag("Untargetable"))
        {
            sr.color = empty;
            enemy = null;
        };
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(enemy != null)
            {
                currentMove.User = body;
                currentMove.Host = enemy;
                currentMove.Effect();
            }
        }
    }

    private void ChangeMove()
    {
        scroll = (scroll + delta) % moveList.Length;
        if (scroll < 0) scroll = moveList.Length - 1;

        currentMove = moveList[scroll];
        img.sprite = currentMove.effectIcon;
    }
}
