using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform playerTransform;
    public Camera mainCamera;
    public Camera secondCamera;
    public SpriteRenderer foregroundSR;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (Vector3.Distance(transform.position, playerTransform.position) < 4f)
            {
                playerTransform.position = new Vector3(23.8f, -3.91f, -0.1f);
                mainCamera.enabled = false;
                secondCamera.enabled = true;
                foregroundSR.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
            }
            else if(playerTransform.position.y > -5)
            {
                playerTransform.position = new Vector3(23.8f, -18.97f, -0.1f);
                secondCamera.enabled = false;
                mainCamera.enabled = true;
                foregroundSR.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
            }
        }
    }
}
