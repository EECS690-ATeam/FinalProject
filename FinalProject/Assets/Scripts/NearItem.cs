using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearItem : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = null;
    }

    // Update is called once per frame
    void Update()
    {
        material = gameObject.GetComponent<SpriteRenderer>().material;

        if (Vector3.Distance(playerPos.position, transform.position) < 4f)
        {
            if (material != null)
            {
                material.SetColor("_Color", new Color(0f, 12f, 24.5f, 1f));
            }
        }
        else
        {
            if (material != null)
            {
                material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
            }
        }
    }
}
