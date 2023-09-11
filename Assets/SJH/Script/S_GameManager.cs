using UnityEngine;

public class S_GameManager : MonoBehaviour
{
    public static S_GameManager instance;

    public WJ_Player player;
    public PetController pet;
    public PetController pet2;

    public S_JemstoneStash stash;
    public S_PlayerSkillUpManager playerSkillUp;
    public float GameTime = 0;

    [Header("스킬창")]
    public Buttoninteractive[] skills;
    public WJ_SkillButton[] skillButtons;
    [Tooltip("현재 보여지는 스킬의 정보입니다.")]
    public Buttoninteractive activateSkill;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        GameTime = Time.time;
    }
}
