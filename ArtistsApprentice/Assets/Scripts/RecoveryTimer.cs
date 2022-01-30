using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecoveryTimer : MonoBehaviour
{
    public float movementRadius;
    public TextMeshProUGUI display;
    public GameObject moveRangeHUD;
    public int recoveryTime = 0, movementCost;
    public bool playerCharacter = false;

    private MovementRangeHUD mrHUD;
    private Movement movement;
    private bool recovering = false;

    // Start is called before the first frame update
    void Start()
    {
        //movement = GetComponent<Movement>();
        if(playerCharacter)
            mrHUD = moveRangeHUD.GetComponent<MovementRangeHUD>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUpRecoveryTime(Vector3 originPoint, Vector3 movePoint, int attackCost)
    {
        if (attackCost > 0)
            recoveryTime += (int)(Vector3.Distance(originPoint, movePoint) / movementRadius * movementCost);
        else
            recoveryTime += movementCost;

        recoveryTime += attackCost;
        StartCoroutine("NextTurn");
    }

    public IEnumerator NextTurn()
    {
        display.gameObject.SetActive(true);
        display.text = "Recovery Time: " + recoveryTime;
        if (playerCharacter)
        {
            
            display.gameObject.SetActive(true);
            moveRangeHUD.SetActive(false);
        }

        recovering = true;
        while(recoveryTime > 0)
        {
            yield return new WaitForSeconds(GameManager.recoveryTimeModifier);
            recoveryTime -= 1;
            display.text = "Recovery Time: " + recoveryTime;
        }
        
        recovering = false;
        display.gameObject.SetActive(false);
        if(playerCharacter)
        {
            mrHUD.ResetPosition();
            moveRangeHUD.SetActive(true);
        }        
    }
}
