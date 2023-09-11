using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PetEntity : MonoBehaviour
{
    [Header("펫 정보")]
    [Tooltip("펫의 현재 공격력")]
    [SerializeField] private float petSpeed = 2.0f;
    [SerializeField] private float petDamage;

    [Tooltip("펫 재사용 대기시간")]
    [SerializeField] protected float petCooldownTimer = 60.0f;
    private float petCooldownTimerUpgrade = 30.0f;

    [Tooltip("footPos : 아랫방향 채광")]
    [SerializeField] protected Transform footPos;
    [Tooltip("toothPos : 정방향 채굴")]
    [SerializeField] protected Transform toothPos;
    [SerializeField] GameObject plight;

    [Header("보유한 광물의 수")]
    public float redjemScore = 0f;
    public float greenjemScore = 0f;
    public float bluejemScore = 0f;
    [Tooltip("Pet이 가질 수 있는 최대 광물 갯수입니다.")]
    public float maxScore = 10.0f;

    [Header("충돌 체크")]
    [Tooltip("Pet이 닿고있는 지면을 확인합니다.")]
    [SerializeField] protected Transform groundCheck;
    private float groundCheckDistance = 0.18f;

    [Tooltip("아랫 방향의 미네랄을 감지합니다.")]
    [SerializeField] protected Transform mineralUnderCheck;
    private float mineralCheckDistance = 0.25f;

    [Tooltip("정면에 광물이 무엇인지 감지합니다.")]
    [SerializeField] protected Transform sideCheck;
    private float sideCheckDistance = 0.05f;

    [Tooltip("Pet 뒤에 미네랄이 있는지 감지합니다.")]
    [SerializeField] protected Transform backCheck;
    private float backCheckDistance = 1.0f;

    [Tooltip("벽을 체크하여 방향전환을 결정합니다.")]
    [SerializeField] protected Transform wallCheck;
    private float wallCheckDistance = 0.8f;

    [Tooltip("바라보는 방향에 미네랄을 임의의 값만큼 감지합니다.")]
    [SerializeField] protected Transform sideMineralCheck;
    [SerializeField] protected float sideMineralCheckDistance;

    [Header("레이어 체크")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected LayerMask WhatIsMineral;
    [SerializeField] protected LayerMask WhatIsSideTile;

    #region facing and Skill
    protected int facingDir = -1;
    protected bool facingRight = true;
    private int attackLv = 1;
    private int scanLv = 1;
    private int carryLv = 1;
    private int cooltimeLv = 1;
    #endregion

    #region Components
    protected Rigidbody2D rbody;
    protected Animator anim;
    protected SpriteRenderer spr;
    private S_Mineral mineral;
    NavigationController2D move_Astar;
    Light2D _light;
    #endregion

    #region anim bool
    protected bool isGrounded;
    protected bool isWallDetected;
    protected bool isMineraled;
    protected bool isSideDetected;
    protected bool isSideMineralDetected;
    protected bool isBackDetected;
    protected bool petMove;
    protected bool underMine;
    protected bool sideMine;
    protected bool petIdle;
    protected bool petFly;
    protected bool hasFlipped = false;
    protected bool isPetCooldown = false;
    private bool hardTileCheck;
    private bool repeatLabor =false;
    #endregion

    protected virtual void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        mineral = GetComponent<S_Mineral>();
        move_Astar = GetComponent<NavigationController2D>();
    }

    protected virtual void Update()
    {
        CollisionChecks();
        PetAnimatorControllers();
        FlipController();

        if (!repeatLabor) 
        { 
            maxComebackHome(); 
        }      
        RestartMining();
    }

    #region Mining
    protected void RestartMining()
    {
        if (isPetCooldown)
        {
            petCooldownTimer -= Time.deltaTime;

            if (petCooldownTimer <= 0.0f)
            {
                isPetCooldown = false;
                petCooldownTimer = 60.0f;
                move_Astar.Gmc();
            }
        }
    }

    protected void maxComebackHome()
    {
        if (redjemScore + greenjemScore + bluejemScore >= maxScore)
        {
            Debug.Log("Pet의 보유 광물의 갯수가 최대치가 도달해, Dome으로 복귀합니다.");
            Instantiate(plight, transform.position, Quaternion.identity);
            move_Astar.Bmc();
            repeatLabor = true;
        }

    }
    #endregion

    #region Flip 
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if (isWallDetected)
        {
            Debug.Log("Flip");
            Flip();
        }
        else if (isBackDetected)
        {
            if (!hasFlipped)
            {
                sideMine = true;
                underMine = false;
                petIdle = false;
                petMove = false;
                Flip();
                hasFlipped = true;
            }
        }
        else
        {
            hasFlipped = false;
        }
    }

    #endregion

    #region CollisionChecks
    protected virtual void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, new Vector2(0,0f -0.1f), groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance * -facingDir, whatIsWall);

        isMineraled = Physics2D.Raycast(mineralUnderCheck.position, Vector2.down, mineralCheckDistance, WhatIsMineral);
        isSideMineralDetected = Physics2D.Raycast(sideMineralCheck.position, new Vector2(-1.0f, 0.0f), sideMineralCheckDistance * -facingDir, WhatIsMineral);

        isBackDetected = Physics2D.Raycast(backCheck.position, Vector2.right,backCheckDistance * facingDir, WhatIsMineral);
        isSideDetected = Physics2D.Raycast(sideCheck.position, new Vector2(-0.5f, 0.0f), sideCheckDistance * -facingDir, WhatIsSideTile);

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(sideCheck.position, new Vector3(sideCheck.position.x + sideCheckDistance * facingDir, sideCheck.position.y));
        Gizmos.DrawLine(sideMineralCheck.position, new Vector3(sideMineralCheck.position.x + sideMineralCheckDistance * facingDir, sideMineralCheck.position.y));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mineralUnderCheck.position, new Vector3(mineralUnderCheck.position.x, mineralUnderCheck.position.y - mineralCheckDistance));

        Gizmos.color = Color.black;
        Gizmos.DrawLine(backCheck.position, new Vector3(backCheck.position.x + backCheckDistance * -facingDir, backCheck.position.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (redjemScore + bluejemScore + greenjemScore >= 1)
        {
            if (collision.gameObject.CompareTag("Stash"))
            {

                S_GameManager.instance.stash.redjemScore += redjemScore;
                S_GameManager.instance.stash.bluejemScore += bluejemScore;
                S_GameManager.instance.stash.greenjemScore += greenjemScore;

                redjemScore = 0;
                bluejemScore = 0;
                greenjemScore = 0;

                isPetCooldown = true;
                repeatLabor = false;
                Debug.Log("Stash에 광물 반납을 완료하였습니다.");

            }
        }
    }

    #endregion

    #region Velocity
    protected void ZeroVelocity() => rbody.velocity = Vector2.zero;
    protected void MoveVelocity() => rbody.velocity = new Vector2(petSpeed * facingDir, rbody.velocity.y);
    protected void boostVelocity()
    {
        Vector2 currentPos = transform.position;
        Vector2 startPos = new Vector2(currentPos.x + 5.0f, currentPos.y);
        Vector2 forceDir = (startPos - currentPos).normalized;
        rbody.velocity = forceDir * 3.0f;
    }

    #endregion

    #region Animaition
    private void PetAnimatorControllers()
    {
        anim.SetBool("Pet_Move", petMove);
        anim.SetBool("Pet_idle", petIdle);
        anim.SetBool("Pet_Fly", petFly);
        anim.SetBool("Under_Mine", underMine);
        anim.SetBool("Side_Mine", sideMine);
    }

    void PetUnderMine()
    {
        Collider2D groundCollider2d = Physics2D.OverlapCircle(footPos.position, 0.05f, whatIsGround);
        Collider2D mineralCollider2d = Physics2D.OverlapCircle(footPos.position, 0.05f, WhatIsMineral);

        if (mineralCollider2d != null)
        {
            mineralCollider2d.transform.GetComponent<S_Mineral>().SetDamage(petDamage);
        }
        else if (groundCollider2d != null && !hardTileCheck)
        {
            groundCollider2d.transform.GetComponent<S_MapGenerator>().MakeDot(footPos.position);
        }
    }

    void PetSideMine()
    {
        Collider2D groundCollider2d = Physics2D.OverlapCircle(toothPos.position, 0.01f, whatIsGround);
        Collider2D mineralCollider2d = Physics2D.OverlapCircle(toothPos.position, 0.01f, WhatIsMineral);

        if (mineralCollider2d != null)
        {
            mineralCollider2d.transform.GetComponent<S_Mineral>().SetDamage(petDamage);
        }
        else if (groundCollider2d != null && !hardTileCheck)
        {
            groundCollider2d.transform.GetComponent<S_MapGenerator>().MakeDot(toothPos.position);
        }
    }
    #endregion

    #region Pet Skill
    protected void PetDamageLv2()
    {
        if (attackLv == 1) 
        {
            petDamage += 5.0f;
       
            attackLv = 2;
        }
    }

    protected void PetDamageLv3()
    {
        if (attackLv == 2 ) 
        {
            petDamage += 5.0f;

            attackLv = 3;
        }
    }

    protected void PetCarryLv2()
    {
        if (carryLv == 1)
        {
            maxScore = 20;
            carryLv = 2;
        }
    }

    protected void PetCarryLv3()
    {
        if (carryLv == 2)
        {
            maxScore = 30;
            carryLv = 3;
        }
    }

    protected void PetScanLv2()
    {
        if (scanLv == 1)
        {
            sideMineralCheckDistance += 2.0f;
            scanLv = 2;
        }
    }

    protected void PetScanLv3()
    {
        if (scanLv == 2)
        {
            sideMineralCheckDistance += 3.0f;
            scanLv = 3;
        }
    }

    protected void PetCoolTimeUpgrade()
    {
        if (cooltimeLv == 1 &&( scanLv == 2 || carryLv == 2 || attackLv == 2))
        {
            cooltimeLv = 2;
            petCooldownTimer = petCooldownTimerUpgrade;
        }
    }

    protected void DoublePet()
    {
        if (cooltimeLv == 2)
        {
        }
    }

    #endregion

}
