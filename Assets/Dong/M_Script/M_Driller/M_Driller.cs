using Unity.VisualScripting;
using UnityEngine;

public class M_Driller : M_Moving
{
    public M_DrillerAttacking attacking { get; private set; }
    public M_DrillerAttackingDead attackingDead { get; private set; }
    public M_DrillerAttackStart attackStart { get; private set; }
    public M_DrillerMove move { get; private set; }
    public M_DrillerMovingDead movingDead { get; private set; }
    public M_DrillerSetting setting { get; private set; }

    public bool settingOn { get; set; }


    AudioSource ads;
    public AudioClip soundSet;
    public AudioClip[] soundDie;
    public AudioClip soundattackStart; 

    protected override void Awake()
    {
        ads = GetComponent<AudioSource>();
        base.Awake();
        transform.Rotate(0, 180, 0);
        attacking = new M_DrillerAttacking(this, stateMachine, "Attacking", this);
        attackingDead = new M_DrillerAttackingDead(this, stateMachine, "AttackingDead", this);
        attackStart = new M_DrillerAttackStart(this, stateMachine, "AttackStart", this);
        move = new M_DrillerMove(this, stateMachine, "Move", this);
        movingDead = new M_DrillerMovingDead(this, stateMachine, "MovingDead", this);
        setting = new M_DrillerSetting(this, stateMachine, "Setting", this);
        settingOn = false;
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
    }

    protected override void Dead()
    {
        if (settingOn) stateMachine.ChangeState(attackingDead);
        else stateMachine.ChangeState(movingDead);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Dome"))
        {
            stateMachine.ChangeState(setting);
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
    public void Attackstart()
    {
        stateMachine.ChangeState(attackStart);
    }

    public void Attacking()
    {
        stateMachine.ChangeState(attacking);
    }

    public void SoundSetting()
    {
        ads.PlayOneShot(soundSet);
    }
    public void SoundDie()
    {
        ads.PlayOneShot(soundDie[Random.Range(0,2)]);
    }
    public void SoundAtk()
    {
        ads.PlayOneShot(soundattackStart);
    }
}
