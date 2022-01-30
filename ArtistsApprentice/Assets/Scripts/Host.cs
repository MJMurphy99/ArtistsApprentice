using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Host : MonoBehaviour
{
    //public TextMeshProUGUI damageDealt;
    public GameObject body;

    private float totalDamage = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        //damageDealt.text = "Damage Dealt: " + totalDamage;
        body = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float val)
    {
        totalDamage += val;
        //damageDealt.text = "Damage Dealt: " + totalDamage;
    }

    public IEnumerator PersistentDamage(float durration, float frequency, float val)
    {
        float currentTime = 0;

        while(currentTime < durration)
        {
            currentTime += frequency;
            totalDamage += val;
            //damageDealt.text = "Damage Dealt: " + totalDamage;
            yield return new WaitForSeconds(frequency);
        }
    }
}
