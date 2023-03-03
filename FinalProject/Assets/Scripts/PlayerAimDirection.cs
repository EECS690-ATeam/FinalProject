using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimDirection : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set aim direction
        Vector3 targetPosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);
    }
}
