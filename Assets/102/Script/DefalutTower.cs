using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class DefalutTower : Tower
{


    #region 
    [Header("레이저")]
    [SerializeField] private GameObject lazer;
    [SerializeField] private GameObject lazerend;
    [SerializeField] private GameObject BigLazer;
    [SerializeField] private Transform lazerPos;
    [SerializeField] private int raydistance;
    [SerializeField] private float chargeTime;
    [SerializeField] private float upgradeChargeTime;
    [SerializeField] private float bigValue;
    [SerializeField] private bool isBigLazer;
    [SerializeField] private float BigLazerDelay;
    [SerializeField] private float BigLazerChargeValue;
    public float Timer =0;


    #endregion

    #region 
    [Header("레이캐스트")]
    RaycastHit2D lrhit;
    public RaycastHit2D[] lrhits;
    public LineRenderer lr;
    [SerializeField] public LayerMask whatisEnemy;
    [SerializeField] public LayerMask whatisEnd;
    Vector2 pos;
    public float rayDistance = 200f;
    #endregion

    #region 
    [Header("애니메이션")]
    [SerializeField] private GameObject animationPrefab;
    #endregion
    #region 
    [Header("레이저관통")]
    [SerializeField] public float Atk;
    [SerializeField] public int maxHitsBeforeDisable;
    [SerializeField] public int penetratedCount;
    #endregion
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        SoundManager.instance.StartLooping();

    }
    void Update()
    {
        Move();
        SetRotation();
        Attack();
        ChargeDelayUpgrade();
        ChargeTimeUpgrade();
        if (M_GameManager.instance.domehp <= 0)
        { 
            gameObject.SetActive(false);
        }
        Timer += Time.deltaTime;
    }

    void ChargeDelayUpgrade()
    {
        if (SkillTreeManager.Instance.isChargeDelayLess == true)
        {
            BigLazerDelay = 0.25f;
        }

    }
    void ChargeTimeUpgrade()
    {
        if (SkillTreeManager.Instance.isChargeTimeLess == true)
        {
            BigLazerChargeValue = 0.005f;
        }

    }


    void Attack()
    {
        if (S_GameManager.instance.player.playerCheck == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
               
                LrDraw();
                BigLazerShotReady();
                if (Timer > 1.5f)
                {
                    SoundManager.instance.PlayLazer();
                    Timer = 0f;
                }

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                LrDisable();
           
            }
        }

    }

    void LrDisable()
    {
        lr.enabled = false;
    }

    void BigLazerShotReady()
    {
        if (SkillTreeManager.Instance.isCharge == true)
        {
            bigValue += BigLazerChargeValue;

            if (bigValue >= 1)
            {
                isBigLazer = true;
                BigLazerCreate();
            }
        }
    }
    void BigLazerCreate()
    {
        GameObject Big = Instantiate(BigLazer, lazerPos.transform.position, lazerPos.transform.rotation);

        Destroy(Big, BigLazerDelay);
        Invoke("BigLazerfalse", BigLazerDelay);
        bigValue = 0f;
    }
    void BigLazerfalse()
    {
        isBigLazer = false;
    }
    /* void LrDraw()
     {
         lr.SetPosition(0, lazerPos.transform.position);


         bool hitEnemy = false;

         if (lrhit = Physics2D.Raycast(transform.position, transform.up, raydistance, whatisEnd))
         {
             lr.SetPosition(1, lrhit.point);
             lr.enabled = true;


             if (lrhit.collider.CompareTag("Monster"))
             {
                 if (lrhit.collider != null)
                 {
                     GameObject hitObject = lrhit.collider.gameObject;

                     hitObject.GetComponent<M_Base>().Damage(Atk);


                     hitEnemy = true;
                 }
             }
         }


         if (!hitEnemy)
         {
             lrhits = Physics2D.RaycastAll(transform.position, transform.up, raydistance, whatisEnemy);

             foreach (RaycastHit2D hit in lrhits)
             {
                 if (hit.collider.CompareTag("Monster"))
                 {
                     if (hit.collider != null)
                     {
                         GameObject hitObject = hit.collider.gameObject;

                         hitObject.GetComponent<M_Base>().Damage(Atk);


                         lr.SetPosition(1, hit.point);
                         lr.enabled = true;

                         Instantiate(lazerend, hit.point, Quaternion.identity);
                         if(SkillTreeManager.Instance.isPenetrate != true) 
                         { 
                         break; //이것만 지우면 관통형 레이저 가능 
                         }
                     }
                 }
             }
         }


         lr.enabled = true;

     }*/
    void LrDraw()
    {
        lr.SetPosition(0, lazerPos.transform.position);

        bool hitEnemy = false;
        int penetratedEnemyCount = 0; // 관통한 몬스터 수를 세기 위한 변수

        if (lrhit = Physics2D.Raycast(transform.position, transform.up, raydistance, whatisEnd))
        {
            lr.SetPosition(1, lrhit.point);
            lr.enabled = true;

            if (lrhit.collider.CompareTag("Monster"))
            {
                if (lrhit.collider != null)
                {
                    GameObject hitObject = lrhit.collider.gameObject;
                    hitObject.GetComponent<M_Base>().Damage1(Atk);
                    hitEnemy = true;
                    penetratedEnemyCount++;
                    SoundManager.instance.PlayLazerHit();
                }
            }
        }

        if (!hitEnemy && penetratedEnemyCount < penetratedCount)
        {
            lrhits = Physics2D.RaycastAll(transform.position, transform.up, raydistance, whatisEnemy);

            foreach (RaycastHit2D hit in lrhits)
            {
                if (hit.collider.CompareTag("Monster"))
                {
                    if (hit.collider != null)
                    {
                        GameObject hitObject = hit.collider.gameObject;
                        hitObject.GetComponent<M_Base>().Damage1(Atk);

                        lr.SetPosition(1, hit.point);
                        lr.enabled = true;

                        SoundManager.instance.PlayLazerHit();
                        Instantiate(lazerend, hit.point, Quaternion.identity);
                        penetratedEnemyCount++;

                        if (penetratedEnemyCount >= penetratedCount) // 3마리 이상 관통했다면 중단
                        {
                            break;
                        }
                    }
                }
            }
        }

        lr.enabled = true;
    }

    void SetRotation()
    {
        if (angle > 1.5 && angle < 1.6)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (angle >= 0.8 && angle <= 0.9)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (angle >= 2.3 && angle <= 2.4)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius / 1.5f;


        transform.position = new Vector3(posX, posY);
    }


    void Move()
    {
        if (S_GameManager.instance.player.playerCheck == false)
        {

            if (isBigLazer != true)
            {
                if (angle < leftLockAngle)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {

                        angle = angle + Time.deltaTime * angularSpeed;
                        transform.Rotate(0, 0, rote);
                        if (angle >= leftLockAngle)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                    }
                }

                if (angle > rightLockAngle)
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {

                        angle = angle + Time.deltaTime * -angularSpeed;
                        transform.Rotate(0, 0, -rote);
                        if (angle <= rightLockAngle)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, -90);
                        }
                        if (transform.rotation.z < -90)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, -90);
                        }
                    }
                }
            }
        }
    }

}