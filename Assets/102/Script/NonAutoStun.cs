using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAutoStun : MonoBehaviour
{
    public float Atk;
    public float Speed = 5f;
    private void Start()
    {
      
    }
    private void Update()
    {
       
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<M_Base>().Damage2(Atk);
          
            Destroy(gameObject);
        }
    }
}
