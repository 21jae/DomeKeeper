using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Shifter : M_Holding
{
    public M_ShifterAppear appear { get; private set; }
    public M_ShifterAttack attack { get; private set; }
    public M_ShifterDead dead { get; private set; }
    public M_ShifterHit hit { get; private set; }
    public M_ShifterIdle idle { get; private set; }
    public M_ShifterShift shift { get; private set; }

    public int shiftCounter;
    public int shiftCount;

    [Header("받아오는 값")]
    public GameObject bullet;
    public Transform shootPosition;

    AudioSource ads;
    public AudioClip soundCharge;
    public AudioClip soundDead;
    public AudioClip soundShoot;
    public AudioClip soundShift;
    protected override void Awake()
    {
        ads = GetComponent<AudioSource>();
        base.Awake();
        appear = new M_ShifterAppear(this, stateMachine, "Appear", this);
        attack = new M_ShifterAttack(this, stateMachine, "Attack", this);
        dead = new M_ShifterDead(this, stateMachine, "Dead", this);
        hit = new M_ShifterHit(this, stateMachine, "Hit", this);
        idle = new M_ShifterIdle(this, stateMachine, "Idle", this);
        shift = new M_ShifterShift(this, stateMachine, "Shift", this);

    }

    protected override void Start()
    {
        base.Start();

        int x = (int)((Random.Range(1, 3) - 1.5) * 2);
        transform.position = new Vector2(Random.Range(2f, 15f) * x, Random.Range(-2f, 5f));
        shiftCounter = Random.Range(2, 4);

        stateMachine.Initiate(appear);

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

        if(transform.position.x > domeCenter.position.x)
        {
           
        }
    }
    protected override void ChangeIdle()
    {
        stateMachine.ChangeState(idle);
    }
    protected override void Dead()
    {
        stateMachine.ChangeState(dead);
    }

    public void EndAppear()
    {
        stateMachine.ChangeState(idle);
    }

    public void EndIdle()
    {
        if (shiftCounter <= shiftCount)
        {
            stateMachine.ChangeState(attack);
        }
        else
            stateMachine.ChangeState(shift);
    }

    public void EndShift()
    {
        stateMachine.ChangeState(appear);
    }

    public void EndAttack()
    {
        stateMachine.ChangeState(shift);
    }

    public void Shoot()
    {
        GameObject bulletPrefab = Instantiate(bullet,shootPosition.position,Quaternion.identity);
        bulletPrefab.SetActive(true);
    }

    public void SoundCharge() => ads.PlayOneShot(soundCharge);
    public void SoundShift() => ads.PlayOneShot(soundShift);
    public void SoundDead() => ads.PlayOneShot(soundDead);
    public void SoundShoot() => ads.PlayOneShot(soundShoot);
}
