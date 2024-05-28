using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() 
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}
