using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Entity Attributes
    [Header("[Relevant]")]
    public int totalHealth;
    private float currentHealth;

    //Miscellaneous Variables
    [Header("[Extra]")]
    public Image img;
    private Combat_EntityManager cem;

    private void Start()
    {
        currentHealth = totalHealth;
        cem = FindObjectOfType<Combat_EntityManager>();
    }

    public bool ChangeHealth(int amount)
    {
        currentHealth -= amount;
        img.fillAmount = currentHealth / totalHealth;
        bool d = DeathState();
        return d;
    }

    private bool DeathState()
    {
        if (currentHealth <= 0)
        {
            cem.UpdatePartyCount(gameObject, 0);
            return true;
        }
        return false;
    }
}
