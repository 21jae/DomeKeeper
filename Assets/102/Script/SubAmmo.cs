using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAmmo : MonoBehaviour
{
    
    [SerializeField] public float Atk;
    [SerializeField] public float Speed;

    private void Start()
    {
        SoundManager.instance.PlaySubTower();
    }
    private void Update()
    {
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<M_Base>().Damage1(Atk);
            Destroy(gameObject);
        }
    }
}
