using UnityEngine;

public class O_WormAttack : M_Base
{
    public Collider2D myCollider;

    public O_WormAttackAttack attack { get; private set; }
    public O_WormAttackDead dead { get; private set; }
    public O_WormAttackHit hit { get; private set; }


    float distance;

    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;

    int x = 0;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        myCollider = rb.GetComponent<Collider2D>();
        attack = new O_WormAttackAttack(this, stateMachine, "Attack", this);
        dead = new O_WormAttackDead(this, stateMachine, "Dead", this);
        hit = new O_WormAttackHit(this, stateMachine, "Hit", this);

        stateMachine.Initiate(attack);
    }

    protected override void Start()
    {
        base.Start();
        distance = domeCenter.position.x - transform.position.x;
        rb.AddForce(new Vector2(distance / 2, Mathf.Abs(distance) * 3 / 5), ForceMode2D.Impulse);
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

    protected override void Dead()
    {
        if (x == 0)
        {
            stateMachine.ChangeState(dead);
            x++;
        }
    }




}
