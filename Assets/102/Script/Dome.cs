using UnityEngine;

public class Dome : MonoBehaviour
{
    #region
    [Header("µ¼")]

    [SerializeField] public float MaxHp;
    [SerializeField] public float CurHp;
    [SerializeField] public float Def;
    [SerializeField] float trueatk;

    public GameObject CrackBg;
    public GameObject Damaged;
    public GameObject DestroyDome;
    [Header("µ¼ ½¯µå")]
    [SerializeField] bool isShield = false;
    SpriteRenderer SI;
    SpriteRenderer Dd;
    public GameObject ShieldIg;
    [SerializeField] public float Shield;
    [SerializeField] public float MaxShield;
    [SerializeField] float RespawnTime;
    [SerializeField] float CoolTimer;
    
    [Header("°Ë")]
    [SerializeField] float Atk;


    float currentHP;
    #endregion

    private void Start()
    {
        SI = ShieldIg.GetComponent<SpriteRenderer>();
        Dd = Damaged.GetComponent<SpriteRenderer>();
        CurHp = MaxHp ;
        CoolTimer = 29f;
    }

    private void Update()
    {
        M_GameManager.instance.domehp = CurHp;

        if (M_GameManager.instance.healDome)
        {
            CurHp += MaxHp * 0.3f;
            if(CurHp >= MaxHp)
            {
                CurHp = MaxHp;
            }
            M_GameManager.instance.healDome = !M_GameManager.instance.healDome;
        }
        
        if (M_GameManager.instance.immortableDome)
        {
            CurHp = currentHP;
        }
        else
        {
            currentHP = CurHp;
        }

        if(M_GameManager.instance.destroyDome)
        {
            CurHp = 0;
            M_GameManager.instance.destroyDome = !M_GameManager.instance.destroyDome;
        }



        if (SkillTreeManager.Instance.isShield == true)
        {
            if (!isShield)
            {
                CoolTimer += Time.deltaTime;
            }
            if (CoolTimer > RespawnTime)
            {
               
                Shield = MaxShield;
                isShield = true;
                SI.enabled = true;
                CoolTimer = 0f;

            }

        }

        if(CurHp <= 0)
        {
            Invoke("LoadLoseScene", 3f);
        }
        else if (M_GameManager.instance.wave<=10)
        {
            M_GameManager.instance.playtime += Time.deltaTime;
        }
    }


    public void LoadLoseScene()
    {
        M_GameManager.instance.EndingScene();
    }

    public void SetHeal(float heal)
    {
        CurHp += heal;
        if (CurHp > MaxHp)
        {
            CurHp = MaxHp;
        }

    }
    public void SetDamage(float atk)
    {
        if (!isShield)
        {
            trueatk = atk - Def;
            if (trueatk <= 0)
            {
                trueatk = 0;
            }

            CurHp -= trueatk;

            if (CurHp < MaxHp / 2)
            {
                Dd.enabled = true;
            }
            else

                Dd.enabled = false;
            if (CurHp < 0)
            {
                CrackBg.SetActive(true);
                DestroyDome.SetActive(true);
                Dd.enabled = false;
            }
        }
        else if (isShield)
        {

            trueatk = atk - Def;
            if (trueatk <= 0)
            {
                trueatk = 0;
            }
            Shield -= trueatk;
            if (Shield < 0)
            {
                Shield = 0f;
                SI.enabled = false;
                isShield = false;
            }
        }
    }


}
