using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject myDataManager;
    public DataPersistenceManager var;
    // Start is called before the first frame update
    void Start()
    {
        myDataManager = GameObject.Find("DataPersistenceManager");
        var = myDataManager.GetComponent<DataPersistenceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Keycard")
        {
            var.SaveGame();
            var.LoadGame();
        }
    }  
}
