using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WAILA: What Am I Looking At
public class WAILA : MonoBehaviour
{
    public Vector3 cameraAnchor;
    private Party p;
    private GameObject highlight;

    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Party>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(r, out hit);

        if (hit.transform != null && hit.transform.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject g = hit.transform.gameObject;
                p.SetFocusedMember(g);

                Camera.main.transform.position = g.transform.position + cameraAnchor;
                highlight = null;
            }
            else
            {
                GameObject g = hit.transform.GetChild(1).gameObject;
                SpriteRenderer sr = g.GetComponent<SpriteRenderer>();

                if (!sr.enabled)
                {
                    highlight = g;
                    sr.enabled = true;
                }
            }
        }
        else
        {
            if(highlight != null)
            {
                SpriteRenderer sr = highlight.GetComponent<SpriteRenderer>();
                sr.enabled = false;
                highlight = null;
            }
        }
    }
}
