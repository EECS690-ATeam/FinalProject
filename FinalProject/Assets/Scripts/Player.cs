using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject myDataManager;
    private DataPersistenceManager var;
    // Start is called before the first frame update
    void Start()
    {
        myDataManager = GameObject.FindWithTag("DPM");
        Debug.Log(myDataManager);
        var = myDataManager.GetComponent<DataPersistenceManager>();
        Debug.Log(var);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Keycard")
        {
            Debug.Log("KEYCARD COLLIDED WITH PLAYER");
            var.SaveGame();
            var.LoadGame();
        }
    }  
}
