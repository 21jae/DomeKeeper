using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class M_Beast : M_Moving
{
    #region State
    public M_BeastMove move { get; private set; } 
    public M_BeastAttack attack { get; private set; }
    public M_BeastGroundDead groundDead { get; private set; }
    public M_BeastHowling howling { get; private set; }
    public M_BeastJump jump { get; private set; }
    public M_BeastWallDead wallDead { get; private set; }
    public M_BeastWallHit wallHit { get; private set; }
    #endregion


    public bool howl { get; private set; }

    public Vector2 jumpPower;
    public bool onGround { get;  set; }

    public float howlX { get; private set; }
    public float JumpX { get; private set; }

    public float currnetHP;

    public AudioSource ads;
    public AudioClip death;
    public AudioClip[] hit;
    public AudioClip[] jumps;
    public AudioClip[] roar;
    public AudioClip[] land;

    protected override void Awake()
    {
        base.Awake();

        ads = GetComponent<AudioSource>();
        move = new M_BeastMove(this,stateMachine,"Move",this);
        attack = new M_BeastAttack(this, stateMachine, "Attack", this);
        groundDead = new M_BeastGroundDead(this, stateMachine, "GroundDead", this);
        howling = new M_BeastHowling(this, stateMachine, "Howling", this);
        jump = new M_BeastJump(this, stateMachine, "Jump", this);
        wallDead = new M_BeastWallDead(this, stateMachine, "WallDead", this);
        wallHit = new M_BeastWallHit(this, stateMachine, "WallHit", this);

        rb.gravityScale = 0;

        howlX = Random.Range(15f, 17f);
        JumpX = Random.Range(10f, 12f);
        howl = false;
        onGround = true;


        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initiate(move);
        currnetHP = hp1;
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
        stateMachine.ChangeState(howling);
    }

    public void HowlOn()
    {
        howl = true;
        stateMachine.ChangeState(move);
    }

    protected override void ChangeAniVelocity(float x)
    {
        base.ChangeAniVelocity(x);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Dome"))
        {
            stateMachine.ChangeState(attack);   
        }
    }

    protected override void Dead()
    {
        if (onGround) stateMachine.ChangeState(groundDead);  
        else stateMachine.ChangeState(wallDead);
    }

    public void HitSound()
    {
        ads.PlayOneShot(hit[Random.Range(0,3)]);
    }
    public void HoulSound()
    {
        ads.PlayOneShot(roar[Random.Range(0, 3)]);
    }

    public void DeadSound()
    {
        ads.PlayOneShot(death);
    }

    public void JumpSound()
    {
        ads.PlayOneShot(jumps[Random.Range(0, 3)]);
    }

    public void LandSound()
    {
        ads.PlayOneShot(land[0]);
    }
}
