using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    Animator animator;
    public GameObject Dome;
    public SpriteRenderer DomeSprite;
    public GameObject Damaged;
    public SpriteRenderer DamagedSprite;

    void Start()
    {
        Dome = GetComponent<GameObject>();
        DomeSprite = Dome.GetComponent<SpriteRenderer>();
        Damaged = GetComponent<GameObject>();
        DamagedSprite = Damaged.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("StartAnim", 4);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartAnim()
    {
        animator.SetBool("isDead", true);
        DomeSprite.enabled = false;
        DamagedSprite.enabled = false;
    }

    void StopAnim()
    {
        animator.SetBool("isDead", false);
    }
}
