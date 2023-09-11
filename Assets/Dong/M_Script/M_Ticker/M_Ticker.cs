using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M_Ticker : M_Moving
{
    public M_TickerDead dead { get; private set; }
    public M_TickerExplosion explosion { get; private set; }
    public M_TickerJump jump { get; private set; }
    public M_TickerMove move { get; private set; }

    public Vector2 jumpPower;

    AudioSource ads;
    public AudioClip[] soundDead;
    public AudioClip[] soundJump;
    public AudioClip soundExplode;

    protected override void Awake()
    {
        base.Awake();
        dead = new M_TickerDead(this, stateMachine, "Dead", this);
        explosion = new M_TickerExplosion(this, stateMachine, "Explosion", this);
        jump = new M_TickerJump(this, stateMachine, "Jump", this);
        move = new M_TickerMove(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0;
        stateMachine.Initiate(move);
    }

    protected override void Update()
    {
        base.Update();
        if (M_GameManager.instance.killmonster)
        {
            Dead();
        }
        if (M_GameManager.instance.domehp <= 0)
        {
            ChangeIdle();
        }
    }
    protected override void ChangeIdle()
    {
        stateMachine.ChangeState(move);
    }
    protected override void Dead()
    {
        stateMachine.ChangeState(dead);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Dome"))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            SetVelocity(zero);
            stateMachine.ChangeState(explosion);
        }
    }

    public void SoundExplode() => ads.PlayOneShot(soundExplode);
    public void SoundJump() => ads.PlayOneShot(soundJump[Random.Range(0, soundJump.Length)]);
    public void SoundDeath() => ads.PlayOneShot(soundDead[Random.Range(0, soundDead.Length)]);
}
