using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Buttoninteractive : MonoBehaviour
{
    [Header("스킬 정보")]
    public string skillName;
    public Sprite skillSprite;

    [TextArea(1, 3)]
    public string skillDes;
    //[SerializeField] protected bool isUpgrade;

    [Header("Price")]
    [SerializeField] float redjemPrice;
    [SerializeField] float bluejemPrice;
    [SerializeField] float greenjemPrice;

    [SerializeField] Button parentbutton;
    [SerializeField] Button[] otherTech;
    public bool parentCheck = false;
    public bool otherCheck = true;

    [SerializeField] GameObject[] Attackimage;
    [SerializeField] GameObject[] Defimage;

    int count = 0;

    enum Name
    {
        None,
        Attack,
        Heal,
        DefUp,
    }

    [SerializeField] Name buttonname;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void Update()
    {

        if (parentbutton != null && parentbutton.interactable == false)
            parentCheck = true;
        else
        {
            parentCheck = false;
            otherCheck = false;
        }

        if (otherTech.Length > 0)
        {
            for (int i = 0; i < otherTech.Length; i++)
            {
                if (otherTech[i].interactable == false)
                {
                    otherCheck = false;
                    break;
                }

                if (otherTech[i].interactable == true)
                    otherCheck = true;
            }
        }
        else
            otherCheck = true;



    }

    public void OnClick()
    {
        if (parentCheck && otherCheck)
        {
            if (S_GameManager.instance.stash.redjemScore >= redjemPrice
            && S_GameManager.instance.stash.bluejemScore >= bluejemPrice
            && S_GameManager.instance.stash.greenjemScore >= greenjemPrice)
            {
                GetComponent<Button>().interactable = false;
                SoundManager.instance.PlaySkillUp();
                S_GameManager.instance.stash.redjemScore -= redjemPrice;
                S_GameManager.instance.stash.bluejemScore -= bluejemPrice;
                S_GameManager.instance.stash.greenjemScore -= greenjemPrice;
            }
        }
    }

    public void OnClick2()
    {
        if (buttonname == Name.Attack)  //  Name.Attack
        {
            if (S_GameManager.instance.stash.redjemScore >= redjemPrice
        && S_GameManager.instance.stash.bluejemScore >= bluejemPrice
        && S_GameManager.instance.stash.greenjemScore >= greenjemPrice
        && count < 5)
            {
                SoundManager.instance.PlaySkillUp();
                S_GameManager.instance.stash.redjemScore -= redjemPrice;
                S_GameManager.instance.stash.bluejemScore -= bluejemPrice;
                S_GameManager.instance.stash.greenjemScore -= greenjemPrice;
                SkillTreeManager.Instance.AttackUp();

                Attackimage[count].SetActive(true);

                count++;
            }
        }
        if (buttonname == Name.DefUp)  //  Name.Attack
        {
            if (S_GameManager.instance.stash.redjemScore >= redjemPrice
        && S_GameManager.instance.stash.bluejemScore >= bluejemPrice
        && S_GameManager.instance.stash.greenjemScore >= greenjemPrice
        && count < 5)
            {
                SoundManager.instance.PlaySkillUp();
                S_GameManager.instance.stash.redjemScore -= redjemPrice;
                S_GameManager.instance.stash.bluejemScore -= bluejemPrice;
                S_GameManager.instance.stash.greenjemScore -= greenjemPrice;
                SkillTreeManager.Instance.DefUp();

                Defimage[count].SetActive(true);

                count++;
            }
        }

        if (buttonname == Name.Heal)  //  Name.Attack
        {
            if (S_GameManager.instance.stash.redjemScore >= redjemPrice
        && S_GameManager.instance.stash.bluejemScore >= bluejemPrice
        && S_GameManager.instance.stash.greenjemScore >= greenjemPrice
        )
            {
                SoundManager.instance.PlaySkillUp();
                S_GameManager.instance.stash.redjemScore -= redjemPrice;
                S_GameManager.instance.stash.bluejemScore -= bluejemPrice;
                S_GameManager.instance.stash.greenjemScore -= greenjemPrice;
                SkillTreeManager.Instance.DomeHeal();


            }
        }

    }
}

