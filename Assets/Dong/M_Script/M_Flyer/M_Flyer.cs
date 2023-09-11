using UnityEngine;

public class M_Flyer : M_Moving
{
    [Header("받아오는 값")]
    public Collider2D active; // attack,hit때의 collider
    public Collider2D inactive; // apper,hide때의 collider
    public Transform shootPosition;
    public GameObject bullet;


    public M_FlyerAppear appear { get; private set; }
    public M_FlyerAttack attack { get; private set; }
    public M_FlyerDead dead { get; private set; }
    public M_FlyerHide hide { get; private set; }
    public M_FlyerHit hit { get; private set; }
    public M_FlyerMove move { get; private set; }


    public int attackTimes { get; set; }
    public int currentAttackTimes { get; set; }
    public Vector2 moveLocation { get; set; }
    public Vector2 path{ get; set; }
    public Vector2 centerVec{ get; set; }
    public Vector2 normalVec{ get; set; }



    public float x;

    AudioSource ads;
    public AudioClip soundAppear;
    public AudioClip soundDead;
    public AudioClip soundHit;
    public AudioClip soundShot;
    public AudioClip soundShrink;


    protected override void Awake()
    {
        ads = GetComponent<AudioSource>();
        base.Awake();
        appear = new M_FlyerAppear(this, stateMachine, "Appear", this);
        attack = new M_FlyerAttack(this, stateMachine, "Attack", this);
        dead = new M_FlyerDead(this, stateMachine, "Dead", this);
        hide = new M_FlyerHide(this, stateMachine, "Hide", this);
        hit = new M_FlyerHit(this, stateMachine, "Hit", this);
        move = new M_FlyerMove(this, stateMachine, "Move", this);

        stateMachine.Initiate(move);


        active.enabled = false;
        inactive.enabled = false;

        int x = (int)((Random.Range(1, 3) - 1.5) * 2);
        moveLocation = new Vector3(Random.Range(2f, 15f) * x, Random.Range(-2f, 5f), 0);

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (M_GameManager.instance.killmonster)
        {
            Dead();
        }

        if (attacked) stateMachine.ChangeState(hit);

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

    public void EndHide()
    {
        stateMachine.ChangeState(move);
    }

    public void EndApper()
    {
        stateMachine.ChangeState(attack);
    }

    public void EndAttack()
    {
        currentAttackTimes++;
    }

    public void EndMove()
    {
        stateMachine.ChangeState(appear);
    }

    public Vector2 Curve(Vector2 start, Vector2 path, Vector2 destination, float timing)
    {

        Vector2 vec1 = Vector2.Lerp(start, path, timing);

        Vector2 vec2 = Vector2.Lerp(path, destination, timing);

        return Vector2.Lerp(vec1, vec2, timing);
    }

    public void Shoot()
    {
        GameObject bulletPrefab = Instantiate(bullet, shootPosition.position, transform.rotation);
        bulletPrefab.SetActive(true);
    }

    public float vectorLength(Vector2 vec1, Vector2 vec2)
    {
        float x;
        Vector2 wantLength;
        wantLength = new Vector2(vec1.x - vec2.x, vec1.y - vec2.y);
        x = wantLength.magnitude;

        return x;
    }

    public void SoundAppear() => ads.PlayOneShot(soundAppear);
    public void SoundDead() =>ads.PlayOneShot(soundDead); 
    public void SoundHit() => ads.PlayOneShot(soundHit); 
    public void SoundShot() => ads.PlayOneShot(soundShot);
    public void SoundShrink() => ads.PlayOneShot(soundShrink);

}

