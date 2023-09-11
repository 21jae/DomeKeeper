using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
 
    public GameObject LazerEnd;
    void Awake()
    {
     
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dome"))
        {
         
            Debug.Log("대충데미지입는중");
            
           
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Instantiate(LazerEnd);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
      
    }
}
