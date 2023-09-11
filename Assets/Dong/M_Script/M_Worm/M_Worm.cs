using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class M_Worm : M_Holding
{
    public M_WormAttack attack { get; private set; }
    public M_WormDead dead { get; private set; }
    public M_WormHide hide { get; private set; }
    public M_WormHit hit { get; private set; }
    public M_WormIdle idle { get; private set; }
    public M_WormInstantiate instantiate { get; private set; }
    public M_WormWakeUp wakeUp { get; private set; }

    public float maxHp;

    [Header("받아오는 값")]
    public GameObject bullet;
    public Transform bulletHole;

    [Header("공격주기")]
    public float attackTimer_Min;
    public float attackTimer_Max;
    public float wakeUpTimer_Min;
    public float wakeUpTimer_Max;

    public Collider2D myCollider { get; set; }

    public int attackCount { get; set; }
    public int attackCounter;

    AudioSource ads;
    public AudioClip soundBurrow;
    public AudioClip soundDie;
    public AudioClip soundEmerge;
    public AudioClip soundSpit;
    protected override void Awake()
    {
        ads = GetComponent<AudioSource>();
        base.Awake();

        attack = new M_WormAttack(this, stateMachine, "Attack", this);
        dead = new M_WormDead(this, stateMachine, "Dead", this);
        hide = new M_WormHide(this, stateMachine, "Hide", this);
        hit = new M_WormHit(this, stateMachine, "Hit", this);
        idle = new M_WormIdle(this, stateMachine, "Idle", this);
        instantiate = new M_WormInstantiate(this, stateMachine, "Instantiate", this);
        wakeUp = new M_WormWakeUp(this, stateMachine, "WakeUp", this);

        myCollider =gameObject.GetComponent<Collider2D>();
        myCollider.enabled = false;
        attackCount = 0;

        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initiate(instantiate);
        maxHp = hp;
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
    public void EndWakeUP()
    {
        stateMachine.ChangeState(idle);
    }

    public void EndAttack()
    {
        stateMachine.ChangeState(idle);
    }

    public void EndIdle()
    {
        if (attackCount == attackCounter)
        {
            stateMachine.ChangeState(hide);
            attackCount = 0;
        }
    }

    public void Attack()
    {
        GameObject bulletPrefab = Instantiate(bullet, bulletHole.position, Quaternion.identity);
        bulletPrefab.SetActive(true);
    }

    public void WakeUp()
    {
        myCollider.enabled = true;
    }


    public void SoundBurrow() => ads.PlayOneShot(soundBurrow);
    public void SoundDie() => ads.PlayOneShot(soundDie);
    public void SoundEmerge() => ads.PlayOneShot(soundEmerge);
    public void SoundSpit() => ads.PlayOneShot(soundSpit);
}
