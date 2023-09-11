using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnd : MonoBehaviour
{
    Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Hit", true);
    }

    void End()
    {
        anim.SetBool("Hit", false);
        Destroy(gameObject,0.5f);
    }
}
