using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<GameObject> party;
    private GameObject focusedPartyMember;

    private void Start()
    {
        SetFocusedMember(party[0]);
    }

    public void SetFocusedMember(GameObject member)
    {
        Movement m1 = member.transform.GetChild(0).GetComponent<Movement>();
        m1.SetFocused();

        if(focusedPartyMember != null)
        {
            Movement m2 = focusedPartyMember.transform.GetChild(0).GetComponent<Movement>();
            m2.SetFocused();
        } 

        focusedPartyMember = member;
    }

    public void EndTurn()
    {
        print(1);
        Movement m = focusedPartyMember.transform.GetChild(0).GetComponent<Movement>();
        m.EndTurn();
    }
}
