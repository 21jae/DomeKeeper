using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WJ_Player : MonoBehaviour
{
    [SerializeField] GameObject petSpawnPos;
    [SerializeField] GameObject pet;
    [SerializeField] GameObject pet2;

    public GameObject drill;
    public bool playerCheck = true;


    public int facingDir { get; private set; } = 1;
    bool facingRight = true;
    public float layerChangeTime;
    public float layerChangeDelay = 1;
    public bool isDomeCheck;

    [Header("Move Info")]
    public float Speed = 3f;
    public float jumpForce = 5f;

    [Header("Collision Info")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask whatIsGround;

    [Header("Score Info")]
    public float redjemScore = 0;
    public float bluejemScore = 0;
    public float greenjemScore = 0;


    [Header("LightSkill Info")]
    [SerializeField] GameObject light;
    [SerializeField] GameObject lightSkillbar;
    public float lightCoolTime;
    public float lightCoolDown;
    public bool useLightSkill = false;

    [Header("TeleportSkill Info")]
    //[SerializeField] GameObject teleport;
    [SerializeField] GameObject teleportSkillbar;
    [SerializeField] GameObject teleportPos;
    public float teleportCoolTime;
    public float teleportCoolDown;
    public bool useteleportSkill = false;


    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rbody { get; private set; }
    //public SpriteRenderer spriteRender { get; private set; }
    #endregion

    #region States
    public WJ_PlayerStateMachine stateMachine { get; private set; }
    public WJ_PlayerIdleState idleState { get; private set; }
    public WJ_PlayerMoveState moveState { get; private set; }
    public WJ_PlayerUpState upState { get; private set; }
    public WJ_PlayerDownState downState { get; private set; }
    public WJ_PlayerDigState digState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new WJ_PlayerStateMachine();
        idleState = new WJ_PlayerIdleState(this, stateMachine, "Idle");
        moveState = new WJ_PlayerMoveState(this, stateMachine, "Move");
        upState = new WJ_PlayerUpState(this, stateMachine, "Up");
        downState = new WJ_PlayerDownState(this, stateMachine, "Down");
        digState = new WJ_PlayerDigState(this, stateMachine, "Dig");
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.Update();

        LayerChangeControll();

        if (useLightSkill)
        {
            LightSkill();
        }
        if (useteleportSkill)
        {
            TeleportSkill();
        }

        if (Input.GetMouseButton(0))
        {
            drill.SetActive(true);
            anim.SetBool("Dig", true);
        }

        if (!Input.GetMouseButton(0))
        {
            drill.SetActive(false);
            anim.SetBool("Dig", false);
        }

    }

    void LightSkill()
    {
        lightCoolDown -= Time.deltaTime;

        if (lightCoolDown <= 0 && Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(light, transform.position, Quaternion.identity);
            lightCoolDown = lightCoolTime;
            lightSkillbar.GetComponent<Slider>().value = 0;
        }
    }
    void TeleportSkill()
    {
        teleportCoolDown -= Time.deltaTime;

        if (teleportCoolDown <= 0 && Input.GetKeyDown(KeyCode.T))
        {
            SoundManager.instance.PlayUseTeleport();
            teleportCoolDown = teleportCoolTime;
            teleportSkillbar.GetComponent<Slider>().value = 0;
            gameObject.transform.position = teleportPos.transform.position;
        }
    }

    public void PetInstantiate()
    {
        pet.SetActive(true);
    }
    public void PetInstantiate2()
    {
        pet2.SetActive(true);
    }
    void LayerChangeControll() => layerChangeTime -= Time.deltaTime;

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rbody.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,
            groundCheck.position.y - groundCheckDistance));

    }
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && facingRight)
        {
            Flip();
        }
        else if (_x < 0 && !facingRight)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stash"))
        {
            if (redjemScore > 0 || bluejemScore > 0 || greenjemScore > 0)
                SoundManager.instance.PlayJemSave();

            S_GameManager.instance.stash.redjemScore += redjemScore;
            S_GameManager.instance.stash.bluejemScore += bluejemScore;
            S_GameManager.instance.stash.greenjemScore += greenjemScore;

            redjemScore = 0;
            bluejemScore = 0;
            greenjemScore = 0;

            isDomeCheck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stash"))
        {
            isDomeCheck = false;
        }
    }
}
