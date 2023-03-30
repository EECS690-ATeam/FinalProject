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
    bool inc = false;
    bool dec = false;

    bool orientation = false; //false = vertical -> true = horizontal

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator.SetBool("IsIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(orientation);
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        if(horizontalMovement != 0) animator.SetBool("IsIdle", false);
        else animator.SetBool("IsIdle", true);
        transform.position += new Vector3(horizontalMovement, verticalMovement, 0) * Time.deltaTime * MovementSpeed;


        if(!orientation && horizontalMovement!=0) { 
            if((GameObject.eulerAngles.z>270 && GameObject.eulerAngles.z<=360)) {
                if(facingRight) transform.Rotate(new Vector3(0,0,-horizontalMovement)); //Right angle
                else transform.Rotate(new Vector3(0,0,-1));
            }
            if(GameObject.eulerAngles.z<270 && GameObject.eulerAngles.z > 90) {
                orientation = true;
            }
        }
        else if(horizontalMovement==0) {
            if(GameObject.eulerAngles.z>220 && GameObject.eulerAngles.z<359) {
                transform.Rotate(new Vector3(0,0,.45f));
            }
        }

        if(orientation) {
            if(GameObject.eulerAngles.z>225 && GameObject.eulerAngles.z<320) {
                transform.Rotate(new Vector3(0,0,verticalMovement));
            }
            if(GameObject.eulerAngles.z<225) inc = true;
            if(GameObject.eulerAngles.z>320) {
                if(horizontalMovement==0) orientation = false;
                animator.SetBool("IsIdle", true);
            }
        }

        if(inc) {
            transform.Rotate(new Vector3(0,0,2));
            inc = false;
        }
        if(dec) {
            transform.Rotate(new Vector3(0,0,2));
            dec = false;
        }

        //Y Axis 180 Rotation when switching sides
        if(transform.rotation.y >0 && transform.rotation.y < 180 && horizontalMovement < 0) {
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot*2, Space.World);
        }
        if(horizontalMovement<0 && facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot);
        }
        if(transform.rotation.y > -180 && transform.rotation.y < 0 && horizontalMovement >0) {
            transform.Rotate(new Vector3(0,Time.deltaTime,0)*horizontalRot*2, Space.World);
        }
        if(horizontalMovement>0 && !facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0,Time.deltaTime,0) * horizontalRot);
        }
    }
}
