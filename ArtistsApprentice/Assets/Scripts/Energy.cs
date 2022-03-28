using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public int fullEnergyVal;
    public GameObject moveRangeHUD;

    private int energyVal;
    private bool repairing = false;

    public int EnergyVal
    {
        get { return energyVal; }
        set 
        { energyVal = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        EnergyVal = fullEnergyVal;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnergyVal <= 0 && !repairing)
            StartCoroutine("NextTurn");
    }

    public IEnumerator NextTurn()
    {
        repairing = true;
        yield return new WaitForSeconds(1.5f);
        EnergyVal = fullEnergyVal;
        repairing = false;
    }

    public void EnergyThreshold(Vector3 originPoint, Vector3 movePoint)
    {
        EnergyVal -= Vector3.Distance(originPoint, movePoint) > 1 ? 2 : 1;
    }
}
