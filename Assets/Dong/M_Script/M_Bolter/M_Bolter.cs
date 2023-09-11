using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bolter : M_Moving
{
    public M_BolterAttack1 attack1 { get; private set; }
    public M_BolterAttack2 attack2 { get; private set; }
    public M_BolterDead dead { get; private set; }
    public M_BolterMove move { get; private set; }
    public M_BolterIdle idle { get; private set; }
    public M_BolterHit hit { get; private set; }

    public float attackCounter;
    public float attackCount { get; set; }

    public int attackChange { get; set; }

    [Header("받아오는 값")]
    public GameObject bullet;
    public Transform shootPosition;

    public GameObject bullet21;
    public GameObject bullet22;
    public GameObject bullet23;
    public Transform shoot21Position;
    public Transform shoot22Position;
    public Transform shoot23Position;

    AudioSource ads;
    public AudioClip[] death;
    public AudioClip[] charge;

    protected override void Awake()
    {
        base.Awake();
        ads = GetComponent<AudioSource>();
        attack1 = new M_BolterAttack1(this, stateMachine, "Attack1", this);
        attack2 = new M_BolterAttack2(this, stateMachine, "Attack2", this);
        dead = new M_BolterDead(this, stateMachine, "Dead", this);
        move = new M_BolterMove(this, stateMachine, "Move", this);
        idle = new M_BolterIdle(this, stateMachine, "Idle", this);
        hit = new M_BolterHit(this, stateMachine, "Hit", this);
    }

    protected override void Start()
    {
        base.Start();
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
        stateMachine.ChangeState(idle);
    }

    protected override void Dead()
    {
        stateMachine.ChangeState(dead);
    }

    public void AttackChange()
    {
        if (stateMachine.currentState != attack2)
        {
            if (attackChange == 2)
            {
                stateMachine.ChangeState(attack2);
            }
        }
    }

    public void EndAttack()
    {
        stateMachine.ChangeState(idle);
    }

    public void Shoot()
    {
       GameObject bulletPrefab = Instantiate(bullet,shootPosition.position,Quaternion.identity);
        bulletPrefab.SetActive(true);
    }

    public void Shoot2()
    {
        GameObject bullet21Prefab = Instantiate(bullet21, shoot21Position.position, Quaternion.identity);
        GameObject bullet22Prefab = Instantiate(bullet22, shoot22Position.position, Quaternion.identity);
        GameObject bullet23Prefab = Instantiate(bullet23, shoot23Position.position, Quaternion.identity);

        bullet21Prefab.SetActive(true);
        bullet22Prefab.SetActive(true);
        bullet23Prefab.SetActive(true);
    }

    public void SoundCharge()
    {
        ads.PlayOneShot(charge[0]);
    }

    public void SoundDead()
    {
        ads.PlayOneShot(death[Random.Range(0,2)]);
    }
}
