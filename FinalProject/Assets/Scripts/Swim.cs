using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public Transform GameObject;
    public Animator animator;
    public float MovementSpeed = 1;
    bool facingRight = true;
    private SpriteRenderer sr;
    public float verticalRot = 30;
    public float horizontalRot = 180;


    bool dec = false;
    bool inc = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        animator.SetFloat("Speed1", Mathf.Abs(verticalMovement));
        transform.position += new Vector3(horizontalMovement, verticalMovement, 0) * Time.deltaTime * MovementSpeed;

        if(!(GameObject.eulerAngles.z >90 && GameObject.eulerAngles.z < 270)) {
            transform.Rotate(new Vector3(0,0,verticalMovement) * Time.deltaTime * verticalRot);
            if(GameObject.eulerAngles.z > 90 && GameObject.eulerAngles.z < 92) dec = true;
            if(GameObject.eulerAngles.z  < 270 && GameObject.eulerAngles.z>268) inc = true;
        }

         if(inc) 
         {
            transform.Rotate(new Vector3(0,0,2f));
            inc = false;
         }
         if(dec) {
            transform.Rotate(new Vector3(0,0,-2f));
            dec = false;
         }

        if(transform.rotation.y >0 && transform.rotation.y < 180 && horizontalMovement < 0) {
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot*2);
            //if(GameObject.eulerAngles.z<180) transform.Rotate(new Vector3(0,0,180-GameObject.eulerAngles.z));
            //if(GameObject.eulerAngles.z>180) transform.Rotate(new Vector3(0,0,360-GameObject.eulerAngles.z));
        }
        if(horizontalMovement<0 && facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot);
        }
        if(transform.rotation.y > -180 && transform.rotation.y < 0 && horizontalMovement >0) {
            transform.Rotate(new Vector3(0,Time.deltaTime,0)*horizontalRot*2);
            //if(GameObject.eulerAngles.z<180) transform.Rotate(new Vector3(0,0,180-GameObject.eulerAngles.z));
            //if(GameObject.eulerAngles.z>180) transform.Rotate(new Vector3(0,0,360-GameObject.eulerAngles.z));
        }
        if(horizontalMovement>0 && !facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot);
        }
    }
}
