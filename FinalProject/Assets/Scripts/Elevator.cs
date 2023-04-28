using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform playerTransform;
    public SpriteRenderer playerSR;
    public Camera mainCamera;
    public Camera secondCamera;
    public ElevatorCam secondCamScript;
    public float panSpeed;
    public float zoomFactor;
    public float zoomSpeed;
    public float zoomSpeedDown;


    // Start is called before the first frame update
    void Start()
    {
        panSpeed = 8f;
        zoomFactor = 1f;
        zoomSpeed = 0.35f;
        zoomSpeedDown = 0.45f;
}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (Vector3.Distance(transform.position, playerTransform.position) < 4f)
            {
                StartCoroutine(ElevatorRideUp());
            }
            else if(playerTransform.position.y > -5)
            {
                StartCoroutine(ElevatorRideDown());
            }
        }
    }

    private IEnumerator ElevatorRideUp()
    {
        // Set second camera in place
        secondCamera.transform.position = playerTransform.position + new Vector3(0, 0, -10);
        secondCamera.orthographicSize = 10f;

        // Set player invisible
        playerSR.color = new Color(255, 255, 255, 0);

        // Swap cameras
        mainCamera.enabled = false;
        secondCamera.enabled = true;

        // Pan towards top
        var targetCamPos = new Vector3(21.9f, 10.6f, -10f);
        var targetZoom = 10f;

        while (Vector3.Distance(secondCamera.transform.position, targetCamPos) > 0.2)
        {
            secondCamScript.SetPosition(Vector3.MoveTowards(secondCamera.transform.position, targetCamPos, Time.deltaTime * panSpeed));

            // Zoom out
            targetZoom += zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom, 10f, 50f);
            secondCamera.orthographicSize = Mathf.Lerp(secondCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);


            yield return new WaitForEndOfFrame();
        }

        // Move player
        playerTransform.position = new Vector3(23.8f, -3.91f, -0.1f);

        // Make player visible once done
        playerSR.color = new Color(255, 255, 255, 255);


        yield return null;
    }

    private IEnumerator ElevatorRideDown()
    {
        // Set player invisible
        playerSR.color = new Color(255, 255, 255, 0);

        // Move player
        playerTransform.position = new Vector3(23.8f, -18.1f, -0.1f);
        var targetCamPos = mainCamera.transform.position;
        var targetZoom = secondCamera.orthographicSize;

        // Pan towards bottom
        while (Vector3.Distance(secondCamera.transform.position, targetCamPos) > 0.01)
        {
            Debug.Log("Update: " + secondCamera.transform.position + secondCamera.orthographicSize);

            secondCamScript.SetPosition(Vector3.MoveTowards(secondCamera.transform.position, targetCamPos, Time.deltaTime * panSpeed));

            // Zoom in
            targetZoom -= zoomFactor;
            targetZoom = Mathf.Clamp(targetZoom, 0f, 50f);
            secondCamera.orthographicSize = Mathf.Clamp(Mathf.Lerp(secondCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeedDown), 10, 40);


            yield return new WaitForEndOfFrame();
        }


        // Make player visible once done
        playerSR.color = new Color(255, 255, 255, 255);


        secondCamera.enabled = false;
        mainCamera.enabled = true;

        yield return null;
    }
}
