using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAttack : MonoBehaviour
{
    public Transform pivot, body ;
    public bool targetLocked = false;

    private Movement m;
    private float xRadius, yRadius;   

    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<Movement>();

        xRadius = Screen.width / 2;
        yRadius = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(m.isFocused)
        {
            Select();

            if (!targetLocked)
                MouseScreenAngle();
        }
    }

    private void MouseScreenAngle()
    {
        Vector2 rawMousePos = Input.mousePosition;
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(body.position);
        xRadius += screenPos.x - xRadius;
        yRadius += screenPos.y - yRadius;

        Vector2 adjMousePos = new Vector2(rawMousePos.x - xRadius, rawMousePos.y - yRadius);

        float opp = adjMousePos.x;
        float adj = adjMousePos.y;

        float theta = Mathf.Atan(adj / opp) * Mathf.Rad2Deg;

        if (opp < 0 && adj > 0) theta += 180;
        else if (opp < 0 && adj < 0) theta += 180;
        else if (opp > 0 && adj < 0) theta += 360;

        pivot.localRotation = Quaternion.Euler(new Vector3(0, -theta, 0));
    }

    private void Select()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            targetLocked = !targetLocked;
    }
}
