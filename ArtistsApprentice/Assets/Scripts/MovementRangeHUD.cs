using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRangeHUD : MonoBehaviour
{
    public GameObject pointPrefab;

    private static GameObject range, point;
    private static GameObject[] player;

    public void Awake()
    {
        range = gameObject;
        player = GameObject.FindGameObjectsWithTag("Player");
        point = pointPrefab;
        ResetPosition();
    }

    public void ResetPosition()
    {
        range.transform.position = new Vector3(
            player[0].transform.position.x, range.transform.position.y, player[0].transform.position.z);
        //PlacePoints(range.transform.position);
    }

    private static void PlacePoints(Vector3 pos)
    {
        for(float x = -2; x < 2.5; x += .5f)
        {
            for (float z = -2; z < 2.5; z += .5f)
            {
                Vector3 pointPos = new Vector3(x + pos.x, pos.y, z + pos.z);

                if(Vector3.Distance(pointPos, pos) == 2)
                    Instantiate(point, pointPos, Quaternion.identity);
            }
        }
    }
}
