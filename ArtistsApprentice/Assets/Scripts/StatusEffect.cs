using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    //public int activationPeriod;
    public int duration;
    public int val;
    public int cost;
    private GameObject user, host;
    //private Host host; public Entity host;
    //public Color effectColor;
    public Sprite effectIcon;// nullIcon;

    public GameObject Host
    {
        get { return host; }
        set { host = value; }
    }

    public GameObject User
    {
        get { return user; }
        set { user = value; }
    }


    public abstract void Effect();

    //public IEnumerator ColorBlink()
    //{
    //    GameObject body = host.currentBody;
    //    //PlaySFX();
    //    SpriteRenderer bodyHue = body.GetComponent<SpriteRenderer>();
    //    bodyHue.color = effectColor;
    //    yield return new WaitForSeconds(1);
    //    bodyHue.color = Color.white;
    //}

    //public void SetIcon(bool active)
    //{
    //    Transform t = host.currentBody.transform.parent.GetChild(0).GetChild(0);
    //    Image i = t.GetChild(t.childCount - (2 - activationPeriod)).GetComponent<Image>();
    //    i.sprite = active ? effectIcon : nullIcon;
    //}

    public void PrintAllStats()
    {
        Debug.Log("Durration: " + duration + " Cost: " + cost + " Value: " + val);
    }
}
