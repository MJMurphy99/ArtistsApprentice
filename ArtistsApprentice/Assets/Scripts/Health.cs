using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image img;
    public int totalHealth;

    private float currentHealth;

    private void Start()
    {
        currentHealth = totalHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth -= amount;
        img.fillAmount = currentHealth / totalHealth;
    }
}
