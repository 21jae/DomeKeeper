using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SkillUnLock : MonoBehaviour
{
    public Button button;
    [SerializeField] GameObject[] upgradeNode;
    [SerializeField] Dome dome;
    [SerializeField] GameObject UnlockImage;

    bool useSkill = false;

    int upgradeCount = 0;
    enum SkillName
    {
        None,
        light,
        teleport,
        domesheild,
        domeHP,
    }

    [SerializeField] SkillName skillName;

    private void Update()
    {
        if (skillName == SkillName.domeHP)
        {
            gameObject.GetComponentInChildren<Slider>().value = dome.GetComponent<Dome>().CurHp / dome.GetComponent<Dome>().MaxHp;
        }

        if (skillName == SkillName.domesheild && useSkill)
        {
            gameObject.GetComponentInChildren<Slider>().value = dome.GetComponent<Dome>().Shield / dome.GetComponent<Dome>().MaxShield;
        }
        if (S_GameManager.instance.playerSkillUp.lightuseCheck)
        {
            if (skillName == SkillName.light)
                gameObject.GetComponentInChildren<Slider>().value += (1 / S_GameManager.instance.player.lightCoolTime) * Time.deltaTime;
        }

        if (S_GameManager.instance.playerSkillUp.teleportuseCheck)
        {
            if (skillName == SkillName.teleport)
                gameObject.GetComponentInChildren<Slider>().value += (1 / S_GameManager.instance.player.teleportCoolTime) * Time.deltaTime;
        }


    }

    public void Unlock()
    {
        //UnlockImage.SetActive(false);
        useSkill = true;
    }

    public void UpgradeNode()
    {
        upgradeNode[upgradeCount].SetActive(true);
        upgradeCount++;

    }
}
