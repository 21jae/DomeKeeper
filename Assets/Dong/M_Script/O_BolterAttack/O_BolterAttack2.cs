using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_BolterAttack2 : M_Holding
{
    public O_BolterAttack2Attack attack2 { get; private set; }

    AudioSource ads;
    public AudioClip ht;
    protected override void Awake()
    {
        ads = GetComponent<AudioSource>();
        base.Awake();
        attack2 = new O_BolterAttack2Attack(this, stateMachine, "Attack2");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initiate(attack2);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void hit1()
    {
        ads.PlayOneShot(ht);
    }
}
