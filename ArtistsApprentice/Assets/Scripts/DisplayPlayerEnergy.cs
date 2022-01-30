using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerEnergy : MonoBehaviour
{
    public Energy energy;

    private static TextMeshProUGUI remainingEnergy;

    // Start is called before the first frame update
    void Start()
    {
        remainingEnergy = GetComponent<TextMeshProUGUI>();
        remainingEnergy.text = "Energy: " + energy.EnergyVal;
    }

    // Update is called once per frame
    void Update()
    {
        remainingEnergy.text = "Energy: " + energy.EnergyVal;
    }
}
