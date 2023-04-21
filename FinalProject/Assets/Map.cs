using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform l1Loc;
    public Transform l2Loc;
    public Transform l3Loc;
    public Lab1 l1;
    public Lab2 l2;
    public Lab3 l3;
    public Transform pLoc;
    public Transform p;
    public SpriteRenderer boundsRenderer;
    public SpriteRenderer temp_grid;
    public Transform area;
    public Transform map;


    //multiplying this number by a worldspace point to get to a mapspace point
    private float horizontalScaling;
    private float verticalScaling;

    Vector3 ScaleToMap(Vector3 point) {
        Vector3 diff = point - area.position;
        float scaledX = diff[0] * horizontalScaling * map.localScale[0];
        float scaledY = diff[1] * verticalScaling * map.localScale[1];
        Vector3 mapDiff = new Vector3(scaledX, scaledY, 0);
        return map.position + mapDiff;
    }

    // Start is called before the first frame update
    void Start()
    {

        float mapVertical = boundsRenderer.size.y;
        float mapHorizontal = boundsRenderer.size.x;

        float areaVertical = temp_grid.size.y;
        float areaHorizontal = temp_grid.size.x;
 
        horizontalScaling = mapHorizontal/areaHorizontal;
        verticalScaling = mapVertical/areaVertical;
        Debug.Log(horizontalScaling);
        Debug.Log(verticalScaling);
        //setting lab and player positions on the minimap
        l1Loc.position = ScaleToMap(l1.transform.position);
        l2Loc.position = ScaleToMap(l2.transform.position);
        l3Loc.position = ScaleToMap(l3.transform.position);
        pLoc.position = ScaleToMap(p.position);


    }

    // Update is called once per frame
    void Update()
    {
       pLoc.transform.position = ScaleToMap(p.position); 
    }
}
