using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Entity Attributes
    [Header("[Relevant]")]
    public int attackMod, recoveryMod;
    public float speed, radius;

    [Header("[Extra]")]
    public StatusEffect[] moveList;
    public StatusEffect currentMove;
    public GameObject body;
    public GameObject mainBody;

    //Referenced Scripts
    public CheckRange cr;
    private RecoveryTimer timer;
    private AimAttack aa;

    //UI Management
    public GameObject[] visualIndicators;
    public Image img;
    public bool isFocused = false;

    //Miscellaneous Variables
    private Vector3 movePoint, originPoint;
    private int scroll = 0, delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        aa = GetComponent<AimAttack>();
        timer = GetComponent<RecoveryTimer>();

        currentMove = Instantiate(moveList[0]);
        currentMove.val += attackMod;
        currentMove.cost += recoveryMod;

        img.sprite = currentMove.effectIcon;
        originPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFocused)
        {
            Move();

            delta = (int)Input.mouseScrollDelta.y;
            if (delta != 0)
                ChangeMove();
        }
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

    public void Fire(List<GameObject> targets)
    {
        foreach (GameObject enemy in targets)
        {
            currentMove.User = body;
            currentMove.Host = enemy;
            currentMove.Effect();
        }
    }

    private void ChangeMove()
    {
        scroll = (scroll + delta) % moveList.Length;
        if (scroll < 0) scroll = moveList.Length - 1;

        currentMove = moveList[scroll];
        img.sprite = currentMove.effectIcon;
    }

    public void EndTurn()
    {
        if (timer.recoveryTime == 0)
        {
            mainBody.transform.position = transform.position;
            transform.localPosition = Vector3.zero;
            int cost;

            if (aa.targetLocked)
                cost = currentMove.cost;
            else cost = 0;

            timer.AddUpRecoveryTime(originPoint, movePoint, cost);
            originPoint = movePoint;

            Fire(cr.targets);
        }
    }

    public void SetFocused()
    {
        isFocused = !isFocused;

        foreach(GameObject g in visualIndicators)
        {
            if(g.name.CompareTo("InnerThreshold") == 0)
                g.SetActive(isFocused);
            else
            {
                SpriteRenderer sr = g.GetComponent<SpriteRenderer>();
                sr.enabled = isFocused;
            }
        }    
    }
}
