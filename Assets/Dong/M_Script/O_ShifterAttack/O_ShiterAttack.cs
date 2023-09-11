using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ShiterAttack : M_Moving
{
    public O_ShiterAttackAttack attack { get; private set; }
    public O_ShiterAttackHit hit { get; private set; }

    AudioSource ads;
    public AudioClip soundHit;

    protected override void Awake()
    {
        ads = GetComponent<AudioSource>();
        base.Awake();
        attack = new O_ShiterAttackAttack(this, stateMachine, "Attack", this);
        hit = new O_ShiterAttackHit(this, stateMachine, "Hit", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initiate(attack);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Dome"))
        {
            stateMachine.ChangeState(hit);
        }
    }

    public void SoundHit() => ads.PlayOneShot(soundHit);
}
