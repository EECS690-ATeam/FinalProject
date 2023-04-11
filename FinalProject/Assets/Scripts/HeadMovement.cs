using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public float MovementSpeed = 15;
    public Transform GameObject;

    public Animator animator;
    public Animator headAnimator;

    public Transform headWrapper;
    public float topAngleLimit = 35;
    public float bottomAngleLimit = -20f;
    public Vector2 mouseWorldPos;
    bool facingRight = true;

    void Update() {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FlipX(horizontalMovement);
        if(horizontalMovement != 0) animator.SetBool("IsIdle", false);
        else animator.SetBool("IsIdle", true);
        if(horizontalMovement != 0) headAnimator.SetBool("IsIdle", false);
        else headAnimator.SetBool("IsIdle", true);
        transform.position += new Vector3(horizontalMovement, verticalMovement, 0) * Time.deltaTime * MovementSpeed;
        LookAtMouse();     
        // if(horizontalMovement !=0 && verticalMovement<0) {
        //     if((GameObject.eulerAngles.z < 360 && GameObject.eulerAngles.z > 315)) {
        //         transform.Rotate(new Vector3(0,0,-horizontalMovement));
        //     }
        // } 
        // if(facingRight) {
        //     if(horizontalMovement != 0 && verticalMovement<0) animator.SetBool("IsDive", true);
        //     else animator.SetBool("IsDive", false);
        //     if(horizontalMovement != 0 && verticalMovement>0) animator.SetBool("IsSurface", true);
        //     else animator.SetBool("IsSurface", false);
        // }  
        // if(!facingRight) {
        //     if(horizontalMovement != 0 && verticalMovement<0 && !facingRight) animator.SetBool("IsSurface", true);
        //     else animator.SetBool("IsSurface", false);
        //     if(horizontalMovement != 0 && verticalMovement>0 && !facingRight) animator.SetBool("IsDive", true);
        //     else animator.SetBool("IsDive", false);
        // }
    }

    private void LookAtMouse() 
    {
        var dir = (mouseWorldPos - (Vector2) headWrapper.position).normalized;
        headWrapper.right = (dir * Mathf.Sign(transform.localScale.x));
        var eulerDir = headWrapper.localEulerAngles;
        eulerDir.z = Mathf.Clamp(eulerDir.z - (eulerDir.z > 180 ? 360 : 0), bottomAngleLimit, topAngleLimit);
        headWrapper.localEulerAngles = eulerDir; 
    }

    private void FlipX(float x) {
        if(facingRight && x<0) {
            animator.SetBool("SetTurnL", true);
            animator.SetBool("Flip", true);
            facingRight = !facingRight;
        }
        if(!facingRight && x>0) {
            animator.SetBool("SetTurnR", true);
            animator.SetBool("Flip", true);
            facingRight = !facingRight;
        }
         if (x!=0) {
            if(animator.GetBool("SetTurnL") == false) transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
            if(animator.GetBool("SetTurnR") == false) transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
         }
    }
}
