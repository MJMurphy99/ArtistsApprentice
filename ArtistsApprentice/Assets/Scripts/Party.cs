using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<GameObject> party;
    public GameObject focusedPartyMember;

    private void Awake()
    {
        SetFocusedMember(party[0]);
    }

    private void Update()
    {
        PartyNumberListener();    
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

    private void PartyNumberListener()
    {
        int num = -1;
        if (Input.GetKeyUp(KeyCode.Alpha1)) num = 0;
        else if (Input.GetKeyUp(KeyCode.Alpha2)) num = 1;
        else if (Input.GetKeyUp(KeyCode.Alpha3)) num = 2;
        else if (Input.GetKeyUp(KeyCode.Alpha4)) num = 3;
        else if (Input.GetKeyUp(KeyCode.Alpha5)) num = 4;
        else if (Input.GetKeyUp(KeyCode.Alpha6)) num = 5;
        else if (Input.GetKeyUp(KeyCode.Alpha7)) num = 6;
        else if (Input.GetKeyUp(KeyCode.Alpha8)) num = 7;
        else if (Input.GetKeyUp(KeyCode.Alpha9)) num = 8;
        else if (Input.GetKeyUp(KeyCode.Alpha0)) num = 9;

        if (num > -1 && num < party.Count) SetFocusedMember(party[num]);
        
    }

    public void Remove(GameObject member)
    {
        int index = party.FindIndex((GameObject g) => { return g == member; });

        if (party.Count > 1)
        {
            if (index == 0) SetFocusedMember(party[1]);
            else SetFocusedMember(party[index - 1]);
        }
        party.Remove(member);
        Destroy(member);
    }

    public void EndTurn()
    {
        if (focusedPartyMember != null)
        {
            Movement m = focusedPartyMember.transform.GetChild(0).GetComponent<Movement>();
            m.EndTurn();
        }
        else print("All party members defeated");
    }
}
