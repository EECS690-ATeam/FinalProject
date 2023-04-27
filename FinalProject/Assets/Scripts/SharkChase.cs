using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkChase : MonoBehaviour
{
    /*******************
     * DECLARATIONS
     ******************/

    // Editor private members
    [SerializeField] private float noticeRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float speed;
    [SerializeField] private Transform castPoint;
    [SerializeField] private Transform player;

    // Private members
    private Rigidbody2D rb;
    //private Material material;

    // Public members
    public bool chasing;

    void Start()
    {
        /*******************
         * INITIALIZATIONS
         ******************/

        rb = GetComponent<Rigidbody2D>();
        chasing = false;
        //material = null;
    }

    void FixedUpdate()
    {
        
    }
}
